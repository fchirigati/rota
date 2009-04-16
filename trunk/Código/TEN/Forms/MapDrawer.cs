using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using TEN.Structures;
using TEN;
using System.Threading;

namespace TEN.Forms
{
	/// <summary>
	/// TO-DO
	/// </summary>
	public partial class MapDrawer : ScrollableControl
	{
		#region Constants
		/// <summary>
		/// Number of extra pixels that bounds the map.
		/// </summary>
		private const int mapExtraBoundings = 150;
		#endregion

		#region Readonly Fields
		private readonly Color colorRoad;
		private readonly Color colorNode;
		private readonly Color colorSelectedNode;
		private readonly Color colorHoveredNode;

		private readonly Pen penRoad;
		private readonly Pen penRoadContour;
		private readonly Pen penVehicleContour;
		private readonly Pen penLaneSeparator;
		private readonly Pen penRoadSketch;
		private readonly Pen penNode;
		private readonly Pen penHoveredNode;
		private readonly Pen penSelectedNode;

		private readonly Brush brushRoad;
		private readonly Brush brushNode;
		private readonly Brush brushHoveredNode;
		private readonly Brush brushSelectedNode;
		#endregion

		#region Fields
		/// <summary>
		/// Current mouse X coordinate.
		/// </summary>
		private int mouseX;

		/// <summary>
		/// Current moues Y coordinate.
		/// </summary>
		private int mouseY;

		private int mapWidth;
		/// <summary>
		/// Width of the map in pixels. Given by the right-most node.
		/// </summary>
		public int MapWidth
		{
			get { return mapWidth; }
		}

		private int mapHeight;
		/// <summary>
		/// Width of the map in pixels. Given by the bottom-most node.
		/// </summary>
		public int MapHeight
		{
			get { return mapHeight; }
		}

		private float zoom;
		/// <summary>
		/// How many times the map is zoomed in.
		/// </summary>
		public float Zoom
		{
			get { return zoom; }
			set { zoom = value; }
		}

		/// <summary>
		/// Reference to the current hovered map node. null if there are no hovered nodes.
		/// </summary>
		MapNode hoveredNode;

		/// <summary>
		/// Reference to the current selected map node. null if there are no hovered nodes.
		/// </summary>
		MapNode selectedNode;

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
			this.colorHoveredNode = Color.Blue;
			this.colorSelectedNode = Color.Red;

			this.penRoad = new Pen(colorRoad, Simulator.LaneWidth);
			this.penRoadContour = new Pen(Color.Black, 2);
			this.penVehicleContour = new Pen(Color.Black, 2);
			this.penLaneSeparator = new Pen(Color.Yellow, 2);
			this.penLaneSeparator.DashStyle = DashStyle.Dash;
			this.penRoadSketch = new Pen(Color.FromArgb(128, colorRoad), Simulator.LaneWidth);
			this.penNode = new Pen(colorNode, 1);
			this.penSelectedNode = new Pen(colorSelectedNode, 1);
			this.penHoveredNode = new Pen(colorHoveredNode, 1);

			this.brushRoad = new SolidBrush(colorRoad);
			this.brushNode = new SolidBrush(Color.FromArgb(32, colorNode));
			this.brushHoveredNode = new SolidBrush(Color.FromArgb(128, colorHoveredNode));
			this.brushSelectedNode = new SolidBrush(colorSelectedNode);
			#endregion

			// Initialize fields.
			this.mapWidth = 0;
			this.mapHeight = 0;
			this.zoom = 1;
			this.hoveredNode = null;
			this.selectedNode = null;
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
		/// Renders the entire map.
		/// </summary>
		/// <param name="graphics">Graphics object in which the map will be drawed.</param>
		private void RenderMap(Graphics graphics)
		{
			if (!TENApp.frmMain.Running)
				return;

			// TO-DO: Usuário deve poder definir modo.
			graphics.SmoothingMode = SmoothingMode.AntiAlias;

			// Mode-exclusive drawings.
			switch (TENApp.frmMain.EditionMode)
			{
				case EditorMode.Pointer:
					break;

				case EditorMode.NewRoad:
					if (clickedNodes.Count > 0)
					{
						graphics.DrawEllipse(penNode, clickedNodes[0].Point.X - 5,
							clickedNodes[0].Point.Y - 5, 10, 10);
						graphics.DrawLine(penRoadSketch, clickedNodes[0].Point,
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
				DrawRoad(graphics, edge);
			}

			// DrawVehicles.
			DrawVehicles(graphics);

			// Draw nodes.
			foreach (MapNode node in TENApp.simulator.Nodes)
			{
				if (hoveredNode == node)
				{
					graphics.DrawEllipse(penHoveredNode, node.Point.X - 5, node.Point.Y - 5, 10, 10);
					graphics.FillEllipse(brushHoveredNode, node.Point.X - 5, node.Point.Y - 5, 10, 10);
				}
				else
				{
					graphics.DrawEllipse(penNode, node.Point.X - 5, node.Point.Y - 5, 10, 10);
					graphics.FillEllipse(brushNode, node.Point.X - 5, node.Point.Y - 5, 10, 10);
				}
			}
		}

		/// <summary>
		/// Draws a road in the map based on a given edge.
		/// </summary>
		/// <param name="graphics">Graphics object that will be used to draw the road.</param>
		/// <param name="edge">The edge that represents the road to be drawed.</param>
		private void DrawRoad(Graphics graphics, MapEdge edge)
		{
			for (int i = 0; i < edge.Lanes.Count; i++)
			{
				graphics.DrawLine(penRoad, edge.Lanes[i].SourcePoint.ToPoint(),
					edge.Lanes[i].DestinationPoint.ToPoint());

				if (i == 0)
					// Draws roads contours.
					graphics.DrawLine(penRoadContour,
						edge.Lanes[i].UpperSourcePoint.ToPoint(),
						edge.Lanes[i].UpperDestinationPoint.ToPoint());
				else
					// Draws lanes separators.
					graphics.DrawLine(penLaneSeparator,
						edge.Lanes[i].UpperSourcePoint.ToPoint(),
						edge.Lanes[i].UpperDestinationPoint.ToPoint());
			}
			graphics.DrawLine(penRoadContour,
				edge.Lanes[edge.Lanes.Count - 1].LowerSourcePoint.ToPoint(),
				edge.Lanes[edge.Lanes.Count - 1].LowerDestinationPoint.ToPoint());

			if (edge.FromNode.OutEdges.Count > 1)
			{
				// TO-DO: curvas
			}
		}

		/// <summary>
		/// Draws the vehicles that are in the map.
		/// </summary>
		/// <param name="graphics">Graphics object that will be used to draw the vehicles.</param>
		private void DrawVehicles(Graphics graphics)
		{
			foreach (MapEdge edge in TENApp.simulator.Edges)
				foreach (Lane lane in edge.Lanes)
					lock (lane.Vehicles)
					{
						foreach (Vehicle vehicle in lane.Vehicles)
							DrawVehicle(graphics, vehicle);
					}
		}

		/// <summary>
		/// Draws a vehicle in the map based on a given vehicle.
		/// </summary>
		/// <param name="graphics">The Graphics object that will be used to draw the road.</param>
		/// <param name="vehicle">The vehicle object to be drawed.</param>
		private void DrawVehicle(Graphics graphics, Vehicle vehicle)
		{
			graphics.DrawPolygon(penVehicleContour, vehicle.GetPoints());
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

			if (clickedNodes.Count >= 2 && clickedNodes[0] != clickedNodes[1])
			{
				NumberDialog numberDialog = new NumberDialog();
				DialogResult result = numberDialog.ShowDialog("New Road", "How many lanes will this road have?", "2");

				if (result == DialogResult.OK)
				{
					MapNode from = clickedNodes[0], to = clickedNodes[1];

					// Verifies if the destination node was a flow node.
					if (to.GetType() == typeof(FlowNode))
					{
						// Removes it from the flow nodes list.
						TENApp.simulator.FlowNodes.Remove((FlowNode)to);
						to = new MapNode(to);
					}

					// Updates the map's size.
					if (from.Point.X > mapWidth)
						mapWidth = from.Point.X;
					if (to.Point.X > mapWidth)
						mapWidth = to.Point.X;
					if (from.Point.Y > mapHeight)
						mapHeight = from.Point.Y;
					if (to.Point.Y > mapHeight)
						mapHeight = to.Point.Y;

					AutoScrollMinSize = new Size(mapWidth + mapExtraBoundings, mapHeight + mapExtraBoundings);

					MapEdge mapEdge = new MapEdge(numberDialog.Response, from, to);

					from.OutEdges.Add(mapEdge);
					to.InEdges.Add(mapEdge);
					TENApp.simulator.Nodes.Add(from);
					TENApp.simulator.Nodes.Add(to);
					TENApp.simulator.Edges.Add(mapEdge);
				}
				else
				{
					// TO-DO
					// User cancelled.
				}
			}

			if (clickedNodes.Count >= 2)
				ClearClickedPoints();
		}
		#endregion

		#region Event Handlers
		/// <summary>
		/// Handles the OnPaint event.
		/// </summary>
		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);

			// Map transformations.
			pe.Graphics.PageUnit = GraphicsUnit.Pixel;
			pe.Graphics.PageScale = zoom;
			pe.Graphics.TranslateTransform(AutoScrollPosition.X, AutoScrollPosition.Y);

			RenderMap(pe.Graphics);
		}

		protected override void OnScroll(ScrollEventArgs se)
		{
			base.OnScroll(se);
			Refresh();
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

			mouseX = (int)((e.X) / zoom) - AutoScrollPosition.X;
			mouseY = (int)((e.Y) / zoom) - AutoScrollPosition.Y;

			if (hoveredNode == null || hoveredNode.Distance(mouseX, mouseY) > 12)
			{
				hoveredNode = null;
				foreach (MapNode node in TENApp.simulator.Nodes)
				{
					if (node.Distance(mouseX, mouseY) < 12)
					{
						hoveredNode = node;
						break;
					}
				}
			}

			// TO-DO: Refresh só se estiver em modo de edição.
			Refresh();
		}
		#endregion
	}
}
