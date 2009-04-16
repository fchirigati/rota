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
		public const int LaneWidth = 25;

		/// <summary>
		/// Width of the vehicles (in pixels).
		/// </summary>
		public const int VehicleWidth = 14;
		#endregion

		#region Fields
		/// <summary>
		/// States if this simulator is running.
		/// </summary>
		private bool running;

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
			this.simulationStepDone = false;
			this.edges = new List<MapEdge>();
			this.nodes = new List<MapNode>();
			this.flowNodes = new List<FlowNode>();
			this.simulationStepTime = 60;
			this.simulationDelay = 30;
			this.safetyDistance = 30;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Runs the simulation.
		/// </summary>
		public void Run()
		{
			while (running)
			{
				SimulationStep();
				simulationStepDone = true;
				Thread.Sleep(simulationDelay);
			}
		}
		
		/// <summary>
		/// Stops the simulation.
		/// </summary>
		public void Stop()
		{
			running = false;
		}

		/// <summary>
		/// Pauses the simulation.
		/// </summary>
		public void Pause()
		{
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

			foreach (FlowNode node in flowNodes)
				node.SimulationStep(simulationStepTime);
		}
		#endregion
	}
}
