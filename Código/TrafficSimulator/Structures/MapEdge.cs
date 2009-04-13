using System;
using System.Collections.Generic;
using TrafficSimulator.Structures;
using System.Linq;
using System.Text;

namespace TrafficSimulator.Structures
{
	/// <summary>
	/// This class represets a edge in the map.
	/// </summary>
	public class MapEdge
	{
		#region Constants
		private const float defaultMaximumSpeed = 80;
		#endregion

		#region Fields
		private MapNode fromNode;
		/// <summary>
		/// A reference to the node that this edge is coming from.
		/// </summary>
		public MapNode FromNode
		{
			get { return fromNode; }
			set { fromNode = value; }
		}

		private MapNode toNode;
		/// <summary>
		/// A reference to the node that this edge is going to.
		/// </summary>
		public MapNode ToNode
		{
			get { return toNode; }
			set { toNode = value; }
		}

		private List<Lane> lanes;
		/// <summary>
		/// Number of lanes in the edge.
		/// </summary>
		public List<Lane> Lanes
		{
			get { return lanes; }
		}

		private float maximumSpeed;
		/// <summary>
		/// Maximum allowed speed in the edge.
		/// </summary>
		public float MaximumSpeed
		{
			get { return maximumSpeed; }
			set { maximumSpeed = value; }
		}

		private float length;
		/// <summary>
		/// Length of this edge.
		/// </summary>
		public float Length
		{
			get { return length; }
		}

		private Vector orthonalVector;
		/// <summary>
		/// Unitary orthogonal vector related to the edge.
		/// </summary>
		public Vector OrthonalVector
		{
			get { return orthonalVector; }
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Creates a new MapEdge object.
		/// </summary>
		/// <param name="lanesNumber">Number of lanes that this edge contains.</param>
		/// <param name="from">The node that this edge is coming from.</param>
		/// <param name="to">The node that this edge is pointing to.</param>
		public MapEdge(int lanesNumber, MapNode from, MapNode to)
		{
			this.lanes = new List<Lane>();
			this.fromNode = from;
			this.toNode = to;
			this.orthonalVector = new Vector(from.Point, to.Point);
			this.orthonalVector.Rotate90();
			this.maximumSpeed = defaultMaximumSpeed;
			this.length = to.Distance(from.Point.X, from.Point.Y);

			for (int i = 1; i <= lanesNumber; i++)
				this.lanes.Add(new Lane(this, lanesNumber, i));
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Runs a simulation step.
		/// </summary>
		/// <param name="simulationStep">Simulation step value.</param>
		public void SimulationStep(int simulationStep)
		{
			foreach (Lane lane in lanes)
			{
				lock (lane.Vehicles)
				{
					foreach (Vehicle vehicle in lane.Vehicles)
						SimulateVehicle(vehicle, simulationStep);

					// Clear vehicles that reached a final node.
					foreach (Vehicle vehicle in lane.ToBeRemoved)
						lane.Vehicles.Remove(vehicle);
					lane.ToBeRemoved.Clear();
				}
			}
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Make the necessary changes in a given vehicle object (and other related objects) 
		/// for a simulation step.
		/// </summary>
		/// <param name="vehicle">Vehicle being simulated.</param>
		/// <param name="simulationStep">Simulation step value.</param>
		private void SimulateVehicle(Vehicle vehicle, int simulationStep)
		{
			vehicle.Position += vehicle.Speed * simulationStep / 1000;
			vehicle.Speed += vehicle.Acceleration * simulationStep / 1000;
			if (vehicle.Speed >= maximumSpeed)
				vehicle.Speed = maximumSpeed;

			if (vehicle.Position > length)
			{
				if (toNode.InEdges.Count == 1)
				{
					// A final node has been reached.
					vehicle.Lane.ToBeRemoved.Add(vehicle);
				}
				else
				{
					// Vehicle reached other edge.
				}
			}
		}
		#endregion
	}
}
