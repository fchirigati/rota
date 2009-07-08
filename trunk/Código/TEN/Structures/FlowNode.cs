using System.Collections.Generic;
using System.Drawing;
using TEN;

namespace TEN.Structures
{
	/// <summary>
	/// Node that generates a flow of vehicles.
	/// </summary>
	public class FlowNode : MapNode
	{
		#region Fields
		private int flow;
		/// <summary>
		/// Number of vehicles that this node generates per a period of time.
		/// </summary>
		public int Flow
		{
			get { return flow; }
			set { flow = value; }
		}

		private float counter;
		/// <summary>
		/// Counts how many steps the simulation has done since the last vehicle was generated.
		/// </summary>
		public float Counter
		{
			get { return counter; }
			set { counter = value; }
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Creates a new FlowNode object.
		/// </summary>
		/// <param name="positionX">The X coordinate of the node.</param>
		/// <param name="positionY">The Y coordinate of the node.</param>
		/// <param name="flowValue">Flow of vehicles that this nodes generates.</param>
		public FlowNode(float positionX, float positionY, int flowValue)
			: base(positionX, positionY)
		{	
			this.counter = 0;
			this.flow = flowValue;
		}

		/// <summary>
		/// Clones a MapNode object.
		/// </summary>
		/// <param name="mapNode">MapNode object to be cloned.</param>
		/// <param name="flowValue">Flow of vehicles that this nodes generates.</param>
		public FlowNode(MapNode mapNode, int flowValue)
			: base(mapNode)
		{
			this.flow = flowValue;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Runs a simulation step.
		/// </summary>
		/// <param name="simulationStep">Simulation step value.</param>
		public void SimulationStep(int simulationStep)
		{
			if (counter * flow > TENApp.simulator.Random.Next(1000))
			{
				int edge = TENApp.simulator.Random.Next(OutEdges.Count);
				int lane = TENApp.simulator.Random.Next(OutEdges[edge].Lanes.Count);
				LinkedList<Vehicle> vehicles = OutEdges[edge].Lanes[lane].Vehicles;

				lock (vehicles)
				{
					bool occupiedEntrance = false;
					foreach (Vehicle vehicle in vehicles)
					{
						if (vehicle.Position < TENApp.simulator.SafetyDistance + vehicle.Length)
						{
							occupiedEntrance = true;
							break;
						}
					}

					if (!occupiedEntrance)
					{
						vehicles.AddFirst(new Vehicle(30, 20, 100, TENApp.simulator.Random.Next(40) + 40,
							Color.White, OutEdges[edge].Lanes[lane]));
						counter = 0;
					}
				}
			}
			else
				counter += (float)simulationStep / 1000;
		}
		#endregion
	}
}
