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

		/// <summary>
		/// Radius of the nodes.
		/// </summary>
		private const int nodeRadius = 5;
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
		private float mouseX;

		/// <summary>
		/// Current mouse Y coordinate.
		/// </summary>
		private float mouseY;

		/// <summary>
		/// Adjusted mouse X coordinate.
		/// </summary>
		private float adjustedMouseX;

		/// <summary>
		/// Adjusted mouse Y coordinate.
		/// </summary>
		private float adjustedMouseY;

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

		private int snapOffset;
		/// <summary>
		/// Minimum distance from the mouse position to a near node to set it as hovered.
		/// </summary>
		public int SnapOffset
		{
			get { return snapOffset; }
			set { snapOffset = value; }
		}

		/// <summary>
		/// Reference to the current hovered map node. null if there are no hovered nodes.
		/// </summary>
		private MapNode hoveredNode;

		/// <summary>
		/// Reference to the current selected map node. null if there are no hovered nodes.
		/// </summary>
		private MapNode selectedNode;

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
			this.penRoadContour = new Pen(Color.Black, 3);
			this.penVehicleContour = new Pen(Color.Black, 2);
			this.penLaneSeparator = new Pen(Color.White, 2);
			//this.penLaneSeparator.DashStyle = DashStyle.Custom;
			this.penLaneSeparator.DashPattern = new float[]{5.0F, 10.0F};
			this.penRoadSketch = new Pen(Color.FromArgb(128, colorRoad), Simulator.LaneWidth);
			this.penNode = new Pen(colorNode, 1);
			this.penSelectedNode = new Pen(colorSelectedNode, 1);
			this.penHoveredNode = new Pen(colorHoveredNode, 1);

			this.brushRoad = new SolidBrush(colorRoad);
			this.brushNode = new SolidBrush(Color.FromArgb(32, colorNode));
			this.brushHoveredNode = new SolidBrush(Color.FromArgb(128, colorHoveredNode));
			this.brushSelectedNode = new SolidBrush(Color.FromArgb(32, colorSelectedNode));
			#endregion

			// Initialize fields.
			this.adjustedMouseX = -1;
			this.adjustedMouseY = -1;
			this.mapWidth = 10000;
			this.mapHeight = 10000;
			this.zoom = 1;
			this.snapOffset = 12;
			this.hoveredNode = MapNode.NoNode;
			this.selectedNode = MapNode.NoNode;
			this.clickedNodes = new List<MapNode>();

			// Design methods.
			InitializeComponent();
			this.AutoScrollMinSize = new Size(this.mapWidth, this.mapHeight);

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
			clickedNodes.Clear();
			selectedNode = MapNode.NoNode;
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

			DrawRoadContours(graphics);
			DrawRoads(graphics);
			DrawRoadSeparators(graphics);
			DrawVehicles(graphics);
			DrawNodes(graphics);

			// Mode-exclusive drawings.
			switch (TENApp.state)
			{
				case AppState.EditingPointer:
					break;

				case AppState.EditingNewRoad:
					if (clickedNodes.Count > 0)
					{
						DrawNode(graphics, clickedNodes[0]);
						if (hoveredNode == MapNode.NoNode)
							graphics.DrawLine(penRoadSketch, clickedNodes[0].Point,
								new Point((int)adjustedMouseX, (int)adjustedMouseY));
						else
							graphics.DrawLine(penRoadSketch, clickedNodes[0].Point, hoveredNode.Point);
					}

					if (hoveredNode == MapNode.NoNode)
						DrawNode(graphics, new MapNode(adjustedMouseX, adjustedMouseY));
					break;

				case AppState.EditingNewTrafficLight:
					break;
			}
		}

		/// <summary>
		/// Draws all the roads (without contours and lane separators).
		/// </summary>
		/// <param name="graphics">Graphics object that will be used to draw the roads.</param>
		private void DrawRoads(Graphics graphics)
		{
			foreach (MapEdge edge in TENApp.simulator.Edges)
			{
				foreach (Lane lane in edge.Lanes)
					graphics.DrawLine(penRoad, lane.SourcePoint.ToPointF(), lane.DestinationPoint.ToPointF());
			}

			foreach (MapEdge edge in TENApp.simulator.ConnectionEdges)
			{
				foreach (Lane lane in edge.Lanes)
				{
					ConnectionLane connLane = (ConnectionLane)lane;
					graphics.DrawBeziers(penRoad, connLane.BezierPoints);
				}
			}
		}

		/// <summary>
		/// Draws all the roads' contours.
		/// </summary>
		/// <param name="graphics">Graphics object that will be used to draw the contours.</param>
		private void DrawRoadContours(Graphics graphics)
		{
			foreach (MapEdge edge in TENApp.simulator.Edges)
			{
				graphics.DrawLine(penRoadContour,
					edge.Lanes[0].UpperSourcePoint.ToPointF(),
					edge.Lanes[0].UpperDestinationPoint.ToPointF());

				graphics.DrawLine(penRoadContour,
					edge.Lanes[edge.Lanes.Count - 1].LowerSourcePoint.ToPointF(),
					edge.Lanes[edge.Lanes.Count - 1].LowerDestinationPoint.ToPointF());
			}

			foreach (MapEdge edge in TENApp.simulator.ConnectionEdges)
			{
				ConnectionLane lowerLane = (ConnectionLane)edge.Lanes[0];
				ConnectionLane upperLane = (ConnectionLane)edge.Lanes[edge.Lanes.Count - 1];

				graphics.DrawBeziers(penRoadContour, lowerLane.UpperBezierPoints);
				graphics.DrawBeziers(penRoadContour, upperLane.LowerBezierPoints);
			}
		}

		/// <summary>
		/// Draws all the road separators in the map.
		/// </summary>
		/// <param name="graphics">Graphics object that will be used to draw the road separators.</param>
		private void DrawRoadSeparators(Graphics graphics)
		{
			foreach (MapEdge edge in TENApp.simulator.Edges)
			{
				for (int i = 1; i < edge.Lanes.Count; i++)
				{
					graphics.DrawLine(penLaneSeparator,
						edge.Lanes[i].UpperSourcePoint.ToPoint(),
						edge.Lanes[i].UpperDestinationPoint.ToPoint());
				}
			}

			foreach (MapEdge edge in TENApp.simulator.ConnectionEdges)
			{
				for (int i = 1; i < edge.Lanes.Count; i++)
				{
					graphics.DrawBeziers(penLaneSeparator, ((ConnectionLane)edge.Lanes[i]).UpperBezierPoints);
				}
			}
		}

		/// <summary>
		/// Draw the map's nodes.
		/// </summary>
		/// <param name="graphics">Graphics object that will be used to draw the nodes.</param>
		private void DrawNodes(Graphics graphics)
		{
			foreach (MapNode node in TENApp.simulator.Nodes)
				DrawNode(graphics, node);
		}

		/// <summary>
		/// Draws a node.
		/// </summary>
		/// <param name="graphics">Graphics object that will be used to draw the nodes.</param>
		/// <param name="node">Node object to be drawed</param>
		private void DrawNode(Graphics graphics, MapNode node)
		{
			Pen nodePen;
			Brush nodeBrush;
			if (selectedNode == node)
			{
				nodePen = penSelectedNode;
				nodeBrush = brushSelectedNode;
			}
			else if (hoveredNode == node)
			{
				nodePen = penHoveredNode;
				nodeBrush = brushHoveredNode;
			}
			else
			{
				nodePen = penNode;
				nodeBrush = brushNode;
			}

			graphics.DrawEllipse(nodePen, node.Point.X - nodeRadius, node.Point.Y - nodeRadius,
				2 * nodeRadius, 2 * nodeRadius);
			graphics.FillEllipse(nodeBrush, node.Point.X - nodeRadius, node.Point.Y - nodeRadius,
				2 * nodeRadius, 2 * nodeRadius);
		}

		/// <summary>
		/// Draws the vehicles that are in the map.
		/// </summary>
		/// <param name="graphics">Graphics object that will be used to draw the vehicles.</param>
		private void DrawVehicles(Graphics graphics)
		{
			foreach (MapEdge edge in TENApp.simulator.Edges)
			{
				foreach (Lane lane in edge.Lanes)
					lock (lane.Vehicles)
					{
						foreach (Vehicle vehicle in lane.Vehicles)
							DrawVehicle(graphics, vehicle);
					}
			}

			foreach (MapEdge edge in TENApp.simulator.ConnectionEdges)
			{
				foreach (Lane lane in edge.Lanes)
					lock (lane.Vehicles)
					{
						foreach (Vehicle vehicle in lane.Vehicles)
							DrawVehicle(graphics, vehicle);
					}
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
		/// Handles an OnClick event with the Pointer mode set.
		/// </summary>
		private void HandlePointerClick(EventArgs e)
		{
			selectedNode = hoveredNode;
			Refresh();
		}

		/// <summary>
		/// Handles an OnClick event with the NewRoad mode set.
		/// </summary>
		private void HandleNewRoadClick(EventArgs e)
		{
			if (hoveredNode != MapNode.NoNode)
			{
				clickedNodes.Add(hoveredNode);
			}
			else
			{
				if (clickedNodes.Count == 0)
				{
					// TO-DO: Setar fluxo.
					FlowNode node = new FlowNode(adjustedMouseX, adjustedMouseY, 15);
					TENApp.simulator.FlowNodes.Add(node);
					clickedNodes.Add(node);
				}
				else
				{
					clickedNodes.Add(new MapNode(adjustedMouseX, adjustedMouseY));
				}
			}

			if (clickedNodes.Count >= 2 && clickedNodes[0] != clickedNodes[1])
			{
				NumberDialog numberDialog = new NumberDialog();
				DialogResult result = numberDialog.ShowDialog("New Road", "How many lanes will this road have?", "2");

				// TO-DO: Verificar se usuário entrou int > 0.
				if (result == DialogResult.OK)
				{
					MapNode from = clickedNodes[0], to = clickedNodes[1];

					// Verifies if the destination node is a flow node.
					if (to.GetType() == typeof(FlowNode))
					{
						// Removes it from the flow nodes list.
						TENApp.simulator.FlowNodes.Remove((FlowNode)to);
						to = new MapNode(to);
					}

					#region Update map's size.
					if (from.Point.X > mapWidth)
						mapWidth = from.Point.X;
					if (to.Point.X > mapWidth)
						mapWidth = to.Point.X;
					if (from.Point.Y > mapHeight)
						mapHeight = from.Point.Y;
					if (to.Point.Y > mapHeight)
						mapHeight = to.Point.Y;

					// TO-DO: Verificar como vai ser isso.
					// AutoScrollMinSize = new Size(mapWidth + mapExtraBoundings, mapHeight + mapExtraBoundings); 
					#endregion

					MapEdge mapEdge = new MapEdge(numberDialog.Response, from, to);

					#region Trim edges as necessary
					if (to.InEdges.Count == 0)
					{
						foreach (MapEdge edge in to.OutEdges)
							edge.TrimFromEdge();
					}
					if (from.OutEdges.Count == 0)
					{
						foreach (MapEdge edge in from.InEdges)
							edge.TrimToEdge();
					} 
					#endregion

					#region Set ToLanes lists
					foreach (MapEdge edge in to.OutEdges)
					{
						MapEdge cEdge = new MapEdge(Math.Min(edge.Lanes.Count, mapEdge.Lanes.Count), to, to);
						for (int i = 0; i < edge.Lanes.Count && i < mapEdge.Lanes.Count; i++)
						{
							ConnectionLane cLane = new ConnectionLane(mapEdge.Lanes[i], edge.Lanes[i]);
							cLane.ToLanes.Add(edge.Lanes[i]);
							cEdge.Lanes[i] = cLane;
							mapEdge.Lanes[i].ToLanes.Add(cLane);
						}
						TENApp.simulator.ConnectionEdges.Add(cEdge);
					}
					foreach (MapEdge edge in from.InEdges)
					{
						MapEdge cEdge = new MapEdge(Math.Min(edge.Lanes.Count, mapEdge.Lanes.Count), to, to);
						for (int i = 0; i < edge.Lanes.Count && i < mapEdge.Lanes.Count; i++)
						{
							ConnectionLane cLane = new ConnectionLane(edge.Lanes[i], mapEdge.Lanes[i]);
							cLane.ToLanes.Add(mapEdge.Lanes[i]);
							cEdge.Lanes[i] = cLane;
							edge.Lanes[i].ToLanes.Add(cLane);
						}
						TENApp.simulator.ConnectionEdges.Add(cEdge);
					}
					#endregion

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

			Refresh();
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

		/// <summary>
		/// Handles OnMouseDown event.
		/// </summary>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
		}

		/// <summary>
		/// Handles OnMouseUp event.
		/// </summary>
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			if (!TENApp.frmMain.Running)
				return;

			switch (TENApp.state)
			{
				case AppState.EditingPointer:
					HandlePointerClick(e);
					break;

				case AppState.EditingNewRoad:
					HandleNewRoadClick(e);
					break;

				case AppState.EditingNewTrafficLight:
					break;
			}
		}

		/// <summary>
		/// Handles OnMouseMove event.
		/// </summary>
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			adjustedMouseX = mouseX = ((e.X) / zoom) - AutoScrollPosition.X;
			adjustedMouseY = mouseY = ((e.Y) / zoom) - AutoScrollPosition.Y;

			// Adjust mouse coordinates, if needed.
			if (TENApp.state == AppState.EditingNewRoad && clickedNodes.Count > 0 && clickedNodes[0].InEdges.Count > 0)
			{
				Vector newRoadSense = new Vector(clickedNodes[0].Point, new Point((int)mouseX, (int)mouseY));

				if (clickedNodes[0].InEdges[0].SenseVector * newRoadSense < 0)
				{
					Vector orthogonal = clickedNodes[0].InEdges[0].OrthogonalVector;
					adjustedMouseX = clickedNodes[0].Point.X + (orthogonal * newRoadSense) * orthogonal.X;
					adjustedMouseY = clickedNodes[0].Point.Y + (orthogonal * newRoadSense) * orthogonal.Y;
				}
			}

			if (hoveredNode == MapNode.NoNode || hoveredNode.Distance(mouseX, mouseY) > snapOffset)
			{
				hoveredNode = MapNode.NoNode;
				foreach (MapNode node in TENApp.simulator.Nodes)
				{
					if (node.Distance(mouseX, mouseY) < snapOffset)
					{
						hoveredNode = node;
						break;
					}
				}
			}

			switch (TENApp.state)
			{
				case AppState.EditingPointer:
				case AppState.EditingNewRoad:
				case AppState.EditingNewTrafficLight:
					Refresh();
					break;
			}
		}

		/// <summary>
		/// Handles OnMouseWheel event.
		/// </summary>
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			base.OnMouseWheel(e);
			Refresh();
		}

		/// <summary>
		/// Handles OnRegionChanged event.
		/// </summary>
		protected override void OnRegionChanged(EventArgs e)
		{
			base.OnRegionChanged(e);
			Refresh();
		}
		#endregion
	}
}
