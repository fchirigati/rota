using TEN.ThreadManagers;
using System.Collections.Generic;

namespace TEN.Structures
{
	/// <summary>
	/// Class that encapsulates all the information needed by a semaphore [group].
	/// </summary>
	public class Semaphore
	{
		#region Constants
		/// <summary>
		/// Value that represents a non-existant semaphore.
		/// </summary>
		public const Semaphore NoSemaphore = null;

		/// <summary>
		/// Possible states for a traffic light.
		/// </summary>
		public enum State : short
		{
			/// <summary>
			/// State that corresponds to the traffic light being red.
			/// </summary>
			Red,

			/// <summary>
			/// State that corresponds to the traffic light being yellow.
			/// </summary>
			Yellow,

			/// <summary>
			/// State that corresponds to the traffic light being green.
			/// </summary>
			Green
		}
		#endregion

		#region Fields
		private MapNode node;
		/// <summary>
		/// MapNode that contains the semaphore.
		/// </summary>
		public MapNode Node
		{
			get { return node; }
		}

		private List<KeyValuePair<MapEdge, int>> temporization;
		/// <summary>
		/// Temporization values - time in seconds that each edge will have on green light.
		/// </summary>
		public List<KeyValuePair<MapEdge, int>> Temporization
		{
			get { return temporization; }
		}

		/// <summary>
		/// Current index of the edge which it's sempahore is not red (either green or yellow).
		/// </summary>
		private int currentEdgeIndex;

		/// <summary>
		/// Current edge which it's sempahore is not red (either green or yellow).
		/// </summary>
		public MapEdge CurrentEdge
		{
			get
			{
				if (currentEdgeIndex == temporization.Count)
					return null;
				else
					return temporization[currentEdgeIndex].Key;
			}
		}

		private float counter;
		/// <summary>
		/// Internal counter of the current edge's timer.
		/// </summary>
		public float Counter
		{
			get { return counter; }
		}

		private State currentEdgeState;
		/// <summary>
		/// State of the current edge's traffic light.
		/// </summary>
		public State CurrentEdgeState
		{
			get { return currentEdgeState; }
		}

		private int cycleInterval;
		/// <summary>
		/// Interval in seconds in which all the lights become red between the cycles.
		/// </summary>
		public int CycleInterval
		{
			get { return cycleInterval; }
			set { cycleInterval = value; }
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Creates a Semaphore object.
		/// </summary>
		/// <param name="mapNode">Map node that contains the semaphore.</param>
		public Semaphore(MapNode mapNode)
		{
			this.node = mapNode;
			this.temporization = new List<KeyValuePair<MapEdge, int>>();
			foreach (MapEdge edge in this.node.InEdges)
				this.temporization.Add(new KeyValuePair<MapEdge,int>(edge, Simulator.DefaultSemaphorePause));
			this.currentEdgeIndex = 0;
			this.counter = 0;
			this.currentEdgeState = State.Green;
			if (this.temporization.Count > 1)
				this.cycleInterval = 0;
			else
				this.cycleInterval = Simulator.DefaultSemaphorePause;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Resets the semaphore to the initial simulation state.
		/// </summary>
		public void Reset()
		{
			counter = 0;
			currentEdgeIndex = 0;
			currentEdgeState = State.Green;
		}

		/// <summary>
		/// Reloads the node's data. Used case any data is altered.
		/// </summary>
		public void RefreshNode()
		{
			List<KeyValuePair<MapEdge,int>> toRemove = new List<KeyValuePair<MapEdge,int>>();

			foreach (KeyValuePair<MapEdge, int> kvp in temporization)
			{
				if (!node.InEdges.Contains(kvp.Key))
					toRemove.Add(kvp);
			}

			foreach (KeyValuePair<MapEdge, int> kvp in toRemove)
				temporization.Remove(kvp);

			foreach (MapEdge edge in node.InEdges)
			{
				bool alredyContains = false;
				foreach (KeyValuePair<MapEdge, int> kvp in temporization)
				{
					if (kvp.Key == edge)
					{
						alredyContains = true;
						break;
					}
				}

				if (!alredyContains)
					temporization.Add(new KeyValuePair<MapEdge, int>(edge, Simulator.DefaultSemaphorePause));
			}
		}

		/// <summary>
		/// Runs a simulation step.
		/// </summary>
		/// <param name="simulationStep">Simulation step value.</param>
		public void SimulationStep(int simulationStep)
		{
			if (temporization.Count == 0)
				return;

			counter += simulationStep * 0.001F;
			if (currentEdgeIndex == temporization.Count)
			{
				if (counter >= cycleInterval)
				{
					counter = 0;
					currentEdgeIndex = 0;
					//currentEdgeState = State.Green;
				}

				return;
			}

			if (counter >= temporization[currentEdgeIndex].Value)
			{
				counter = 0;
				currentEdgeIndex++;
				currentEdgeState = State.Green;
				return;
			}

			if (currentEdgeState == State.Yellow)
				return;

			if (temporization[currentEdgeIndex].Value - counter <= 3)
				currentEdgeState = State.Yellow;
		}
		#endregion
	}
}
