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
		public const int LaneWidth = 26;

		/// <summary>
		/// Width of the vehicles (in pixels).
		/// </summary>
		public const int VehicleWidth = 14;
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
		#endregion

		private int safetyDistance;
		/// <summary>
		/// Gets or sets the safety distance between vehicles.
		/// </summary>
		public int SafetyDistance
		{
			get { return safetyDistance; }
			set { safetyDistance = value; }
		}
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
			this.safetyDistance = 15;
			this.connectionEdges = new List<MapEdge>();
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
		}
		
		/// <summary>
		/// Stops the simulation.
		/// </summary>
		public void Stop()
		{
			if (!isSimulating)
				return;

			isSimulating = false;

			// TO-DO: Encapsular em métodos de Clear.
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
		}

		/// <summary>
		/// Pauses the simulation.
		/// </summary>
		public void Pause()
		{
			if (!isSimulating)
				return;

			isSimulating = false;
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
		}
		#endregion
	}
}
