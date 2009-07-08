using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;
using TEN;
using TEN.Structures;
using TEN.Util;

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
		private readonly Color colorFlowNode;

		private readonly Pen penRoad;
		private readonly Pen penRoadContour;
		private readonly Pen penVehicleContour;
		private readonly Pen penLaneSeparator;
		private readonly Pen penRoadSketch;
		private readonly Pen penNode;
		private readonly Pen penHoveredNode;
		private readonly Pen penSelectedNode;
		private readonly Pen penFlowNode;
		private readonly Pen penLightBorder;

		private readonly Brush brushRoad;
		private readonly Brush brushNode;
		private readonly Brush brushHoveredNode;
		private readonly Brush brushSelectedNode;
		private readonly Brush brushHoveredEdge;
		private readonly Brush brushFlowNode;
		private readonly Brush brushSelectedEdge;
		private readonly Brush brushRedLight;
		private readonly Brush brushYellowLight;
		private readonly Brush brushGreenLight;
		private readonly Brush brushRedLightOff;
		private readonly Brush brushYellowLightOff;
		private readonly Brush brushGreenLightOff;
		private readonly Brush brushSemaphore;
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

		private MapNode selectedNode;
		/// <summary>
		/// Reference to the current selected map node. null if there are no hovered nodes.
		/// </summary>
		public MapNode SelectedNode
		{
			get { return selectedNode; }
		}

		/// <summary>
		/// Reference to the current hovered edge. null if there are no hovered edges.
		/// </summary>
		private MapEdge hoveredEdge;

		private MapEdge selectedEdge;
		/// <summary>
		/// Reference to the current selected edge. null if there are no hovered edges.
		/// </summary>
		public MapEdge SelectedEdge
		{
			set { selectedEdge = value; }
			get { return selectedEdge; }
		}

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
			this.colorHoveredNode = Color.MidnightBlue;
			this.colorSelectedNode = Color.Red;
			this.colorFlowNode = Color.Green;

			this.penRoad = new Pen(colorRoad, Simulator.LaneWidth);
			this.penRoadContour = new Pen(Color.Black, 3);
			this.penVehicleContour = new Pen(Color.Black, 2);
			this.penLaneSeparator = new Pen(Color.White, 2);
			this.penLaneSeparator.DashPattern = new float[]{5.0F, 10.0F};
			this.penRoadSketch = new Pen(Color.FromArgb(128, colorRoad), Simulator.LaneWidth);
			this.penNode = new Pen(colorNode, 1);
			this.penSelectedNode = new Pen(colorSelectedNode, 1);
			this.penHoveredNode = new Pen(colorHoveredNode, 1);
			this.penFlowNode = new Pen(colorFlowNode, 1);
			this.penLightBorder = new Pen(Color.White, 1);

			this.brushRoad = new SolidBrush(colorRoad);
			this.brushNode = new SolidBrush(Color.FromArgb(32, colorNode));
			this.brushHoveredNode = new SolidBrush(Color.FromArgb(128, colorHoveredNode));
			this.brushSelectedNode = new SolidBrush(Color.FromArgb(32, colorSelectedNode));
			this.brushFlowNode = new SolidBrush(Color.FromArgb(32, colorFlowNode));
			this.brushHoveredEdge = new SolidBrush(Color.FromArgb(64, Color.Blue));
			this.brushSelectedEdge = new SolidBrush(Color.FromArgb(64, Color.Red));
			this.brushRedLight = new SolidBrush(Color.Red);
			this.brushYellowLight = new SolidBrush(Color.Yellow);
			this.brushGreenLight = new SolidBrush(Color.Lime);
			this.brushRedLightOff = new SolidBrush(Color.FromArgb(64, Color.Red));
			this.brushYellowLightOff = new SolidBrush(Color.FromArgb(64, Color.Yellow));
			this.brushGreenLightOff = new SolidBrush(Color.FromArgb(64, Color.Lime));
			this.brushSemaphore = new SolidBrush(Color.FromArgb(144, Color.Black));
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
			this.hoveredEdge = null;
			this.selectedEdge = null;
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
		/// Clears the clicked points list and the selections.
		/// </summary>
		public void ResetSelections()
		{
			clickedNodes.Clear();
			selectedNode = MapNode.NoNode;
			selectedEdge = null;
		}

		/// <summary>
		/// Method called to configure the selected item or the general settings if no items are selected.
		/// </summary>
		public void Configure()
		{
			if (selectedNode != null)
			{
				TEN.Structures.Semaphore semaphore = selectedNode.Semaphore;
				if (semaphore == TEN.Structures.Semaphore.NoSemaphore)
					return;

				SemaphoreDialog dialog = new SemaphoreDialog(semaphore.CycleInterval,
					semaphore.Temporization);
				DialogResult result = dialog.ShowDialog();

				if (result == DialogResult.OK)
				{
					semaphore.CycleInterval = dialog.CycleInterval;
					semaphore.Temporization.Clear();
					foreach (KeyValuePair<MapEdge, int> kvp in dialog.TemporizationCopy)
						semaphore.Temporization.Add(kvp);
				}
			}
			else if (selectedEdge != null)
			{
				MaxSpeedDialog dialog = new MaxSpeedDialog(selectedEdge.MaximumSpeed);
				DialogResult result = dialog.ShowDialog();

				if (result == DialogResult.OK)
				{
					selectedEdge.MaximumSpeed = dialog.MaxSpeed;
				}
			}
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
			DrawSemaphores(graphics);

			// Mode-exclusive drawings.
			switch (TENApp.state)
			{
				case AppState.EditingPointer:
					DrawNodes(graphics);

					if (selectedEdge != null)
					{
						foreach (PointF[] quadrangle in selectedEdge.Boundings)
							graphics.FillPolygon(brushSelectedEdge, quadrangle);
					}

					if (hoveredNode == MapNode.NoNode)
					{
						if (hoveredEdge != null && !hoveredEdge.PointInsideEdge(mouseX, mouseY))
							hoveredEdge = null;

						if (hoveredEdge == null)
						{
							foreach (MapEdge edge in TENApp.simulator.Edges)
							{
								if (edge.PointInsideEdge(mouseX, mouseY))
								{
									hoveredEdge = edge;
									break;
								}
							}
						}

						if (hoveredEdge != null && hoveredEdge != selectedEdge)
						{
							foreach (PointF[] quadrangle in hoveredEdge.Boundings)
								graphics.FillPolygon(brushHoveredEdge, quadrangle);
						}
					}
					break;

				case AppState.EditingNewRoad:
					DrawNodes(graphics);
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
					DrawNodes(graphics);

					if (selectedEdge != null)
					{
						foreach (PointF[] quadrangle in selectedEdge.Boundings)
							graphics.FillPolygon(brushSelectedEdge, quadrangle);
					}
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
			else if (node.GetType() == typeof(FlowNode))
			{
				nodePen = penFlowNode;
				nodeBrush = brushFlowNode;
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
		/// Draws the semaphores that are in the map.
		/// </summary>
		/// <param name="graphics">Graphics object that will be used to draw the semaphores.</param>
		private void DrawSemaphores(Graphics graphics)
		{
			foreach (TEN.Structures.Semaphore semaphore in TENApp.simulator.Semaphores)
				DrawSemaphore(graphics, semaphore);
		}

		/// <summary>
		/// Draws a semaphore in the map based on a given Semaphore object.
		/// </summary>
		/// <param name="graphics">The Graphics object that will be used to draw the semaphore.</param>
		/// <param name="semaphore">The Semaphore object to be drawed.</param>
		private void DrawSemaphore(Graphics graphics, TEN.Structures.Semaphore semaphore)
		{
			foreach (KeyValuePair<MapEdge, int> kvp in semaphore.Temporization)
			{
				MapEdge edge = kvp.Key;
				Brush brushRed = brushRedLight;
				Brush brushYellow = brushYellowLightOff;
				Brush brushGreen = brushGreenLightOff;
				PointF position = edge.Lanes[0].DestinationPoint.ToPointF();

				if (semaphore.CurrentEdge == edge)
				{
					switch (semaphore.CurrentEdgeState)
					{
						case TEN.Structures.Semaphore.State.Red:
							brushRed = brushRedLight;
							brushYellow = brushYellowLightOff;
							brushGreen = brushGreenLightOff;
							break;

						case TEN.Structures.Semaphore.State.Yellow:
							brushRed = brushRedLightOff;
							brushYellow = brushYellowLight;
							brushGreen = brushGreenLightOff;
							break;

						case TEN.Structures.Semaphore.State.Green:
							brushRed = brushRedLightOff;
							brushYellow = brushYellowLightOff;
							brushGreen = brushGreenLight;
							break;
					}
				}

				//graphics.DrawRectangle(penLightBorder, position.X - 6, position.Y - 6, 12, 34);
				graphics.FillRectangle(brushSemaphore, position.X - 5, position.Y - 5, 10, 32);
				graphics.DrawEllipse(penLightBorder, position.X - 4, position.Y - 4, 8, 8);
				graphics.FillEllipse(brushGreen, position.X - 3, position.Y - 3, 6, 6);
				graphics.DrawEllipse(penLightBorder, position.X - 4, position.Y + 7, 8, 8);
				graphics.FillEllipse(brushYellow, position.X - 3, position.Y + 8, 6, 6);
				graphics.DrawEllipse(penLightBorder, position.X - 4, position.Y + 17, 8, 8);
				graphics.FillEllipse(brushRed, position.X - 3, position.Y + 18, 6, 6);
			}
			
		}

		/// <summary>
		/// Handles an OnClick event with the Pointer mode set.
		/// </summary>
		private void HandlePointerClick(EventArgs e)
		{
			selectedNode = hoveredNode;
			if (selectedNode == MapNode.NoNode)
				selectedEdge = hoveredEdge;
			else
				selectedEdge = null;
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
					FlowNode node = new FlowNode(adjustedMouseX, adjustedMouseY, TENApp.simulator.FlowValue);
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
				NewRoadDialog numberDialog = new NewRoadDialog();
				DialogResult result = numberDialog.ShowDialog();

				// TO-DO: Verificar se usuário entrou int > 0.
				if (result == DialogResult.OK)
				{
					MapNode from = clickedNodes[0], to = clickedNodes[1];

					// Verifies if the destination node is a flow node.
					if (to.GetType() == typeof(FlowNode))
					{
						// Removes it from the flow nodes and general nodes list.
						TENApp.simulator.FlowNodes.Remove((FlowNode)to);
						TENApp.simulator.Nodes.Remove(to);
						MapNode newNode = new MapNode(to);
						
						// Remake all references of the flow node to a new equivalent non-flow node.
						foreach (MapEdge edge in to.OutEdges)
							edge.FromNode = to;

						to = newNode;
					}

					#region Update map's size
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

					MapEdge mapEdge = new MapEdge(numberDialog.LanesNumber, from, to, numberDialog.MaxSpeed);

					from.OutEdges.Add(mapEdge);
					to.InEdges.Add(mapEdge);
					if (!TENApp.simulator.Nodes.Contains(from))
						TENApp.simulator.Nodes.Add(from);
					if (!TENApp.simulator.Nodes.Contains(to))
						TENApp.simulator.Nodes.Add(to);
					TENApp.simulator.Edges.Add(mapEdge);

					#region Trim edges as necessary
					foreach (MapEdge edge in to.OutEdges)
						edge.TrimFromEdge();

					foreach (MapEdge edge in from.InEdges)
						edge.TrimToEdge();
					#endregion

					#region Set ToLanes lists
					foreach (MapEdge edge in to.OutEdges)
					{
						MapEdge cEdge = new MapEdge(Math.Min(edge.Lanes.Count, mapEdge.Lanes.Count), to, to, mapEdge.MaximumSpeed);
						for (int i = 0; i < edge.Lanes.Count && i < mapEdge.Lanes.Count; i++)
						{
							ConnectionLane cLane = new ConnectionLane(mapEdge.Lanes[i], edge.Lanes[i]);
							cLane.ToLanes.Add(edge.Lanes[i]);
							cEdge.Lanes[i] = cLane;
							mapEdge.Lanes[i].ToLanes.Add(cLane);
						}
						edge.InConnections.Add(cEdge);
						mapEdge.OutConnections.Add(cEdge);
						edge.SetBoundings();
						TENApp.simulator.ConnectionEdges.Add(cEdge);
					}
					foreach (MapEdge edge in from.InEdges)
					{
						MapEdge cEdge = new MapEdge(Math.Min(edge.Lanes.Count, mapEdge.Lanes.Count), to, to, edge.MaximumSpeed);
						for (int i = 0; i < edge.Lanes.Count && i < mapEdge.Lanes.Count; i++)
						{
							ConnectionLane cLane = new ConnectionLane(edge.Lanes[i], mapEdge.Lanes[i]);
							cLane.ToLanes.Add(mapEdge.Lanes[i]);
							cEdge.Lanes[i] = cLane;
							edge.Lanes[i].ToLanes.Add(cLane);
						}
						edge.OutConnections.Add(cEdge);
						mapEdge.InConnections.Add(cEdge);
						TENApp.simulator.ConnectionEdges.Add(cEdge);
					}
					mapEdge.SetBoundings();
					#endregion

					#region Verify if a semaphore refresh is needed
					if (to.Semaphore != TEN.Structures.Semaphore.NoSemaphore)
						to.Semaphore.RefreshNode();
					#endregion
				}
				else
				{
					// User cancelled.
					if (clickedNodes[0].GetType() == typeof(FlowNode))
					{
						TENApp.simulator.FlowNodes.Remove((FlowNode)clickedNodes[0]);
					}
				}
			}

			if (clickedNodes.Count >= 2)
				ResetSelections();

			Refresh();
		}

		/// <summary>
		/// Handles an OnClick event with the NewTrafficLight mode set.
		/// </summary>
		private void HandleNewSemaphoreClick(EventArgs e)
		{
			MapNode node = hoveredNode;

			if (node == MapNode.NoNode || node.Semaphore != TEN.Structures.Semaphore.NoSemaphore || node.InEdges.Count == 0)
				return;

			TEN.Structures.Semaphore semaphore = new TEN.Structures.Semaphore(node);
			TENApp.simulator.Semaphores.Add(semaphore);
			node.Semaphore = semaphore;

			SemaphoreDialog dialog = new SemaphoreDialog(semaphore.CycleInterval,
				semaphore.Temporization);
			DialogResult result = dialog.ShowDialog();

			if (result == DialogResult.OK)
			{
				semaphore.CycleInterval = dialog.CycleInterval;
				semaphore.Temporization.Clear();
				foreach (KeyValuePair<MapEdge, int> kvp in dialog.TemporizationCopy)
					semaphore.Temporization.Add(kvp);
			}
			else
			{
				node.Semaphore = null;
				TENApp.simulator.Semaphores.Remove(semaphore);
			}

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
			pe.Graphics.TranslateTransform(AutoScrollPosition.X, AutoScrollPosition.Y);
			pe.Graphics.PageScale = zoom;

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
					HandleNewSemaphoreClick(e);
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
