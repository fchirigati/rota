using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TEN.Structures;
using TrafficSimulator;
using TrafficSimulator.Structures;
using System.Threading;

namespace TEN.Forms
{
	/// <summary>
	/// TO-DO
	/// </summary>
	public partial class MapDrawer : Control
	{
		#region Readonly Fields
		private readonly Color colorRoad;
		private readonly Color colorNode;
		private readonly Color colorSelectedNode;
		private readonly Color colorHoverNode;

		private readonly Pen penRoad;
		private readonly Pen penRoadContour;
		private readonly Pen penLaneSeparator;
		private readonly Pen penRoadSketch;
		private readonly Pen penNode;
		private readonly Pen penSelectedNode;
		private readonly Pen penHoverNode;

		private readonly Brush brushRoad;
		#endregion

		#region Fields
		/// <summary>
		/// Current mouse X coordinate.
		/// </summary>
		private int mouseX = 0;

		/// <summary>
		/// Current moues Y coordinate.
		/// </summary>
		private int mouseY = 0;

		/// <summary>
		/// Reference to the current hovered map node. null if there are no hovered nodes.
		/// </summary>
		MapNode hoveredNode;

		/// <summary>
		/// List of temporary clicked points.
		/// </summary>
		private List<MapNode> clickedNodes;
		#endregion

		#region Constructors
		/// <summary>
		/// TO-DO
		/// </summary>
		public MapDrawer()
		{
			#region Readonly Fields
			this.colorRoad = Color.LightGray;
			this.colorNode = Color.Blue;
			this.colorHoverNode = Color.Blue;
			this.colorSelectedNode = Color.Red;

			this.penRoad = new Pen(colorRoad, Simulator.LaneWidth);
			this.penRoadContour = new Pen(Color.Black, 2);
			this.penLaneSeparator = new Pen(Color.Yellow, 2);
			this.penLaneSeparator.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
			this.penRoadSketch = new Pen(Color.FromArgb(128, colorRoad), Simulator.LaneWidth);
			this.penNode = new Pen(colorNode, 1);
			this.penHoverNode = new Pen(colorHoverNode, 2);
			this.penSelectedNode = new Pen(colorSelectedNode, 2);

			this.brushRoad = new SolidBrush(colorRoad);
			#endregion

			// Initialize fields.
			this.hoveredNode = null;
			this.clickedNodes = new List<MapNode>();

			// Design method.
			InitializeComponent();

			// Enable double buffer.
			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Clears the clicked points list.
		/// </summary>
		public void ClearClickedPoints()
		{
			this.clickedNodes.Clear();
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Draws a road in the map based on a given edge.
		/// </summary>
		/// <param name="graphics">The Graphics object that will be used to draw the road.</param>
		/// <param name="edge">The edge that represents the road to be drawed.</param>
		private void DrawRoad(Graphics graphics, MapEdge edge)
		{
			for (int i = 0; i < edge.Lanes.Count; i++)
			{
				graphics.DrawLine(penRoad, edge.Lanes[i].SourcePoint.ToPoint(),
					edge.Lanes[i].DestinationPoint.ToPoint());

				// Draws lanes separators.
				if (i == 0)
					graphics.DrawLine(penRoadContour,
						edge.Lanes[i].UpperSourcePoint.ToPoint(),
						edge.Lanes[i].UpperDestinationPoint.ToPoint());
				else
					graphics.DrawLine(penLaneSeparator,
						edge.Lanes[i].UpperSourcePoint.ToPoint(),
						edge.Lanes[i].UpperDestinationPoint.ToPoint());

				// Draw vehicles.
				lock (edge.Lanes[i].Vehicles)
				{
					foreach (Vehicle vehicle in edge.Lanes[i].Vehicles)
						DrawVehicle(graphics, vehicle);
				}
			}
			graphics.DrawLine(penRoadContour,
				edge.Lanes[edge.Lanes.Count - 1].LowerSourcePoint.ToPoint(),
				edge.Lanes[edge.Lanes.Count - 1].LowerDestinationPoint.ToPoint());

			// TO-DO: curvas
			if (edge.FromNode.InEdges.Count > 1)
			{
				//graphics.DrawLine(penRoadContour, );
				graphics.FillEllipse(brushRoad, edge.FromNode.Point.X - Simulator.LaneWidth / 2,
					edge.FromNode.Point.Y - Simulator.LaneWidth / 2,
					Simulator.LaneWidth, Simulator.LaneWidth);
			}

			if (edge.ToNode.InEdges.Count > 1)
			{
				graphics.FillEllipse(brushRoad, edge.ToNode.Point.X - Simulator.LaneWidth / 2,
					edge.ToNode.Point.Y - Simulator.LaneWidth / 2,
					Simulator.LaneWidth, Simulator.LaneWidth);
			}
		}

		/// <summary>
		/// Draws a vehicle in the map based on a given vehicle.
		/// </summary>
		/// <param name="graphics">The Graphics object that will be used to draw the road.</param>
		/// <param name="vehicle">The vehicle object to be drawed.</param>
		private void DrawVehicle(Graphics graphics, Vehicle vehicle)
		{
			graphics.DrawPolygon(penRoadContour, vehicle.GetPoints());
			graphics.FillPolygon(new SolidBrush(vehicle.Color), vehicle.GetPoints());
		}

		/// <summary>
		/// Handles an OnClick event with the NewRoad moad set.
		/// </summary>
		private void HandleNewRoadClick(EventArgs e)
		{
			if (hoveredNode != null)
				clickedNodes.Add(hoveredNode);
			else
				if (clickedNodes.Count == 1)
					clickedNodes.Add(new MapNode(mouseX, mouseY));
				else
				{
					FlowNode node = new FlowNode(mouseX, mouseY, 10);
					TENApp.simulator.FlowNodes.Add(node);
					clickedNodes.Add(node);
				}

			if (clickedNodes.Count >= 2)
			{
				MapNode from = clickedNodes[0], to = clickedNodes[1];

				if (from != to)
				{
					NumberDialog numberDialog = new NumberDialog();
					DialogResult result =
						numberDialog.ShowDialog("New Road", "How many lanes will this road have?", "2");
					if (result == DialogResult.OK)
					{
						// User clicked on Ok.
						if (to.GetType() == typeof(FlowNode))
						{
							// Removes the node from the flow nodes list.
							TENApp.simulator.FlowNodes.Remove((FlowNode)to);
							to = new MapNode(to);
						}

						MapEdge mapEdge = new MapEdge(numberDialog.Response, from, to);

						from.InEdges.Add(mapEdge);
						to.InEdges.Add(mapEdge);
						TENApp.simulator.Nodes.Add(from);
						TENApp.simulator.Nodes.Add(to);
						TENApp.simulator.Edges.Add(mapEdge);
					}
					else
					{
						// User cancelled (TO-DO).
					}
				}

				ClearClickedPoints();
			}
		}
		#endregion

		#region Event Handlers
		/// <summary>
		/// Handles the OnPaint event.
		/// </summary>
		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);
			if (!TENApp.frmMain.Running)
				return;

			// TO-DO: Usuário deve poder definir.
			pe.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

			// Mode-exclusive drawings.
			switch (TENApp.frmMain.EditionMode)
			{
				case EditorMode.Pointer:
					break;

				case EditorMode.NewRoad:
					if (clickedNodes.Count > 0)
					{
						pe.Graphics.DrawEllipse(penNode, clickedNodes[0].Point.X - 5,
							clickedNodes[0].Point.Y - 5, 10, 10);
						pe.Graphics.DrawLine(penRoadSketch, clickedNodes[0].Point,
							new Point(mouseX, mouseY));
					}
					break;

				case EditorMode.NewTrafficLight:
					break;
			}

			// Map drawing.
			// Draw edges.
			foreach (MapEdge edge in TENApp.simulator.Edges)
			{
				DrawRoad(pe.Graphics, edge);
			}

			// Draw nodes.
			if (hoveredNode != null && hoveredNode.Distance(mouseX, mouseY) > 10)
				hoveredNode = null;

			foreach (MapNode node in TENApp.simulator.Nodes)
			{
				if (node == hoveredNode ||
					(hoveredNode == null && node.Distance(mouseX, mouseY) < 10))
				{
					hoveredNode = node;
					pe.Graphics.DrawEllipse(penHoverNode,
						node.Point.X - 5, node.Point.Y - 5, 10, 10);
				}
				else
					pe.Graphics.DrawEllipse(penNode,
						node.Point.X - 5, node.Point.Y - 5, 10, 10);
			}
		}

		/// <summary>
		/// Handles the OnClick event.
		/// </summary>
		protected override void OnClick(EventArgs e)
		{
			base.OnClick(e);
			if (!TENApp.frmMain.Running)
				return;

			switch (TENApp.frmMain.EditionMode)
			{
				case EditorMode.Pointer:
					break;

				case EditorMode.NewRoad:
					HandleNewRoadClick(e);
					break;

				case EditorMode.NewTrafficLight:
					break;
			}
		}

		/// <summary>
		/// Handles OnMouseMove event.
		/// </summary>
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			mouseX = e.X;
			mouseY = e.Y;

			Refresh();
		}
		#endregion
	}
}
