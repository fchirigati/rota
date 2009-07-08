using System;
using System.Drawing;
using System.Collections.Generic;
using System.Threading;
using TEN.Structures;

namespace TEN
{
	/// <summary>
	/// TO-DO
	/// </summary>
	public class Simulator
	{
		#region Constants
		/// <summary>
		/// Width of each lane (in pixels).
		/// </summary>
		public const int LaneWidth = 28;

		/// <summary>
		/// Width of the vehicles (in pixels).
		/// </summary>
		public const int VehicleWidth = 14;

		/// <summary>
		/// Default semaphore temporization (in seconds).
		/// </summary>
		public const int DefaultSemaphorePause = 10;

		/// <summary>
		/// Default safety distance (in pixels).
		/// </summary>
		public const int DefaultSafetyDistance = 15;

		/// <summary>
		/// Default warning speed.
		/// </summary>
		public const int DefaultWarningSpeed = 40;

		/// <summary>
		/// Default flow value.
		/// </summary>
		public const int DefaultFlowValue = 15;
		#endregion

		#region Fields
		/// <summary>
		/// States if the thread should be running or not.
		/// </summary>
		private bool running;

		private Random random;
		/// <summary>
		/// Random object used when necessary.
		/// </summary>
		public Random Random
		{
			get { return random; }
		}

		private bool isSimulating;
		/// <summary>
		/// States if this simulator is running.
		/// </summary>
		public bool IsSimulating
		{
			get { return isSimulating; }
		}

		private bool simulationStepDone;
		/// <summary>
		/// States if a simulation step has been done since the last time it was checked.
		/// </summary>
		public bool SimulationStepDone
		{
			get { return simulationStepDone; }
			set { simulationStepDone = value; }
		}

		private int simulationStepTime;
		/// <summary>
		/// How much time there is between simulation steps.
		/// </summary>
		public int SimulationStepTime
		{
			get { return simulationStepTime; }
			set { simulationStepTime = value; }
		}

		private int simulationDelay;
		/// <summary>
		/// Real time delay between simulation steps (in ms).
		/// </summary>
		public int SimulationDelay
		{
			get { return simulationDelay; }
			set { simulationDelay = value; }
		}

		#region Report Data
		private int carsOut;
		/// <summary>
		/// Number of cars that were removed from the simulation by reaching an end node.
		/// </summary>
		public int CarsOut
		{
			get { return carsOut; }
			set { carsOut = value; }
		}

		private float averageSpeedSum;
		/// <summary>
		/// Sum of average speed of the cars that were removed from the simulation.
		/// </summary>
		public float AverageSpeedSum
		{
			get { return averageSpeedSum; }
			set { averageSpeedSum = value; }
		}

		private DateTime simulationStartTime;
		/// <summary>
		/// Simulation start time.
		/// </summary>
		public DateTime SimulationStartTime
		{
			get { return simulationStartTime; }
			set { simulationStartTime = value; }
		}

		private DateTime simulationEndTime;
		/// <summary>
		/// Simulation end time.
		/// </summary>
		public DateTime SimulationEndTime
		{
			get { return simulationEndTime; }
			set { simulationEndTime = value; }
		}
		#endregion

		#region Map Data
		private List<MapEdge> edges;
		/// <summary>
		/// MapEdge objects contained in the map.
		/// </summary>
		public List<MapEdge> Edges
		{
			get { return edges; }
		}

		private List<MapNode> nodes;
		/// <summary>
		/// MapNode objects contained in the map.
		/// </summary>
		public List<MapNode> Nodes
		{
			get { return nodes; }
		}

		private List<FlowNode> flowNodes;
		/// <summary>
		/// List of flow nodes.
		/// </summary>
		public List<FlowNode> FlowNodes
		{
			get { return flowNodes; }
		}

		private List<MapEdge> connectionEdges;
		/// <summary>
		/// Connection lanes that are generated automatically by the simulator.
		/// </summary>
		public List<MapEdge> ConnectionEdges
		{
			get { return connectionEdges; }
		}

		private List<TEN.Structures.Semaphore> semaphores;
		/// <summary>
		/// Semaphores of the map.
		/// </summary>
		public List<TEN.Structures.Semaphore> Semaphores
		{
			get { return semaphores; }
		}
		#endregion

		#region Parameters
		private int safetyDistance;
		/// <summary>
		/// Gets or sets the safety distance between vehicles.
		/// </summary>
		public int SafetyDistance
		{
			get { return safetyDistance; }
			set { safetyDistance = value; }
		}

		private int warningSpeed;
		/// <summary>
		/// Maximum speed of a vehicle when entering a road intersection.
		/// </summary>
		public int WarningSpeed
		{
			get { return warningSpeed; }
			set { warningSpeed = value; }
		}

		private int flowValue;
		/// <summary>
		/// Value of the flow of vehicles that enters the map.
		/// </summary>
		public int FlowValue
		{
			get { return flowValue; }
			set { flowValue = value; }
		}
		#endregion
		#endregion

		#region Constructors
		/// <summary>
		/// Constructor of the Simulator object.
		/// </summary>
		public Simulator()
		{
			this.running = true;
			this.random = new Random();
			this.isSimulating = false;
			this.simulationStepDone = false;
			this.edges = new List<MapEdge>();
			this.nodes = new List<MapNode>();
			this.flowNodes = new List<FlowNode>();
			this.simulationStepTime = 60;
			this.simulationDelay = 30;
			this.connectionEdges = new List<MapEdge>();
			this.semaphores = new List<TEN.Structures.Semaphore>();
			this.safetyDistance = DefaultSafetyDistance;
			this.warningSpeed = DefaultWarningSpeed;
			this.flowValue = DefaultFlowValue;
			this.carsOut = 0;
			this.averageSpeedSum = 0;
			this.simulationStartTime = new DateTime();
			this.simulationEndTime = new DateTime();
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Method that a thread should run in order to use this object.
		/// </summary>
		public void ThreadRun()
		{
			while (running)
			{
				while (running && isSimulating)
				{
					SimulationStep();
					simulationStepDone = true;
					Thread.Sleep(simulationDelay);
				}

				Thread.Sleep(1000);
			}
		}

		/// <summary>
		/// Method called to end the thread running this object's ThreadRun method gracefully.
		/// </summary>
		public void ThreadStop()
		{
			running = false;
		}

		/// <summary>
		/// Starts the simulation.
		/// </summary>
		public void Start()
		{
			if (isSimulating)
				return;

			isSimulating = true;
			simulationStartTime = DateTime.Now;
			carsOut = 0;
			averageSpeedSum = 0;
		}
		
		/// <summary>
		/// Stops the simulation.
		/// </summary>
		public void Stop()
		{
			if (!isSimulating)
				return;

			isSimulating = false;

			simulationEndTime = DateTime.Now;
			List<MapEdge> allEdges = new List<MapEdge>(edges);
			allEdges.AddRange(connectionEdges);

			foreach (MapEdge edge in allEdges)
			{
				foreach (Lane lane in edge.Lanes)
				{
					lock (lane.Vehicles)
					{
						carsOut += lane.Vehicles.Count;

						foreach (Vehicle vehicle in lane.Vehicles)
							averageSpeedSum += 1000 * vehicle.TotalDistance / (vehicle.TotalSteps * simulationStepTime);
					}
				}
			}

			Reset();
		}

		/// <summary>
		/// Pauses the simulation.
		/// </summary>
		public void Pause()
		{
			if (!isSimulating)
				return;

			isSimulating = false;
			simulationEndTime = DateTime.Now;
		}

		/// <summary>
		/// Deletes the current selected edge.
		/// </summary>
		public void DeleteEdge()
		{
			MapEdge edge = TENApp.frmMain.Drawer.SelectedEdge;
			if (edge == null) 
				return;

			edges.Remove(edge);
			edge.ToNode.InEdges.Remove(edge);
			edge.FromNode.OutEdges.Remove(edge);
			foreach (MapEdge connEdge in edge.InConnections)
			{
				connectionEdges.Remove(connEdge);
				foreach (MapEdge inEdge in edge.FromNode.InEdges)
				{
					foreach (Lane lane in inEdge.Lanes)
					{
						List<Lane> toRemove = new List<Lane>();
						foreach (Lane connLane in lane.ToLanes)
						{
							if (connEdge.Lanes.Contains(connLane))
								toRemove.Add(connLane);
						}

						foreach (Lane remLane in toRemove)
							lane.ToLanes.Remove(remLane);
					}

					inEdge.OutConnections.Remove(connEdge);
				}
			}
			foreach (MapEdge connEdge in edge.OutConnections)
			{
				connectionEdges.Remove(connEdge);
				foreach (MapEdge outEdge in edge.ToNode.OutEdges)
				{
					outEdge.InConnections.Remove(connEdge);
				}
			}

			foreach (MapEdge trimmedEdge in edge.FromNode.InEdges)
			{
				trimmedEdge.TrimToEdge();
			}
			foreach (MapEdge trimmedEdge in edge.ToNode.OutEdges)
			{
				trimmedEdge.TrimFromEdge();
			}

			if (edge.ToNode.Semaphore != TEN.Structures.Semaphore.NoSemaphore)
			{
				if (edge.ToNode.InEdges.Count > 0)
					edge.ToNode.Semaphore.RefreshNode();
				else
				{
					semaphores.Remove(edge.ToNode.Semaphore);
					edge.ToNode.Semaphore = null;
				}
			}
			if (edge.ToNode.InEdges.Count == 0)
			{
				if (edge.ToNode.OutEdges.Count > 0)
				{
					FlowNode newNode = new FlowNode(edge.ToNode, flowValue);
					foreach (MapEdge outEdge in edge.ToNode.OutEdges)
						outEdge.FromNode = newNode;

					nodes.Add(newNode);
					flowNodes.Add(newNode);
				}

				nodes.Remove(edge.ToNode);
				semaphores.Remove(edge.ToNode.Semaphore);
			}
			if (edge.FromNode.InEdges.Count == 0 && edge.FromNode.OutEdges.Count == 0)
			{
				nodes.Remove(edge.FromNode);
				semaphores.Remove(edge.FromNode.Semaphore);
				if (edge.FromNode.GetType() == typeof(FlowNode))
					flowNodes.Remove((FlowNode)edge.FromNode);
			}

			TENApp.frmMain.Drawer.SelectedEdge = null;
			TENApp.frmMain.Drawer.HoveredEdge = null;
		}

		/// <summary>
		/// Deletes the current selected node's semaphore.
		/// </summary>
		public void DeleteSemaphore()
		{
			MapNode node = TENApp.frmMain.Drawer.SelectedNode;
			if (node == MapNode.NoNode)
				return;

			semaphores.Remove(node.Semaphore);
			node.Semaphore = null;
		}

		/// <summary>
		/// Resets the simulation to the initial state.
		/// </summary>
		public void Reset()
		{
			foreach (MapEdge edge in edges)
				foreach (Lane lane in edge.Lanes)
					lock (lane.Vehicles)
						lane.Vehicles.Clear();

			foreach (MapEdge edge in connectionEdges)
				foreach (Lane lane in edge.Lanes)
					lock (lane.Vehicles)
						lane.Vehicles.Clear();

			foreach (FlowNode node in flowNodes)
				node.Counter = 0;

			foreach (TEN.Structures.Semaphore semaphore in semaphores)
				semaphore.Reset();
		}

		/// <summary>
		/// Clears all map elements.
		/// </summary>
		public void Clear()
		{
			edges.Clear();
			nodes.Clear();
			flowNodes.Clear();
			connectionEdges.Clear();
			semaphores.Clear();
			safetyDistance = DefaultSafetyDistance;
			warningSpeed = DefaultWarningSpeed;
			flowValue = DefaultFlowValue;
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Runs a simulation step.
		/// </summary>
		private void SimulationStep()
		{
			foreach (MapEdge edge in edges)
				edge.SimulationStep(simulationStepTime);

			foreach (MapEdge edge in connectionEdges)
				edge.SimulationStep(simulationStepTime);

			foreach (FlowNode node in flowNodes)
				node.SimulationStep(simulationStepTime);

			foreach (TEN.Structures.Semaphore semaphore in semaphores)
				semaphore.SimulationStep(simulationStepTime);
		}
		#endregion
	}
}
