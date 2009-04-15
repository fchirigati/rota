using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TEN.Structures
{
	/// <summary>
	/// Node that generates a flow of vehicles.
	/// </summary>
	public class FlowNode : MapNode
	{
		#region Fields
		private Random random;

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
		public FlowNode(int positionX, int positionY, int flowValue)
			: base(positionX, positionY)
		{
			this.random = new Random();
			this.counter = 0;
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
			if (counter > random.Next(10 * flow))
			{
				int lane = random.Next(OutEdges[0].Lanes.Count);
				List<Vehicle> vehicles = OutEdges[0].Lanes[lane].Vehicles;

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
						vehicles.Add(new Vehicle(30, 1, 1, 60, Color.White, OutEdges[0].Lanes[lane]));
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
