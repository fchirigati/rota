using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using TEN;

namespace TEN.Structures
{
	/// <summary>
	/// This class represents a lane.
	/// </summary>
	public class Lane
	{
		#region Fields
		private LinkedList<Vehicle> vehicles;
		/// <summary>
		/// Vehicles that are currently in the lane.
		/// </summary>
		public LinkedList<Vehicle> Vehicles
		{
			get { return vehicles; }
		}

		private List<Vehicle> toBeRemoved;
		/// <summary>
		/// List of vehicles to be removed in the current simulation step.
		/// </summary>
		public List<Vehicle> ToBeRemoved
		{
			get { return toBeRemoved; }
		}

		private MapEdge edge;
		/// <summary>
		/// Reference to the edge that contains this lane.
		/// </summary>
		public MapEdge Edge
		{
			get { return edge; }
		}

		private int laneNumber;
		/// <summary>
		/// Number of the lane in the edge.
		/// </summary>
		public int LaneNumber
		{
			get { return laneNumber; }
		}

		private Vector pointer;
		/// <summary>
		/// Unitary vector that indicates the lane's direction.
		/// </summary>
		public Vector Pointer
		{
			get { return pointer; }
		}

		/// <summary>
		/// Length of this lane.
		/// </summary>
		public virtual float Length
		{
			get { return edge.Length; }
		}

		#region Lane References
		private Lane rightLane;
		/// <summary>
		/// Lane that is in the right of this lane.
		/// </summary>
		public Lane RightLane
		{
			get { return rightLane; }
			set { rightLane = value; }
		}

		private Lane leftLane;
		/// <summary>
		/// Lane that is in the left of this lane.
		/// </summary>
		public Lane LeftLane
		{
			get { return leftLane; }
			set { leftLane = value; }
		}

		private List<Lane> toLanes;
		/// <summary>
		/// List of lanes to which this lane goes to.
		/// </summary>
		public List<Lane> ToLanes
		{
			get { return toLanes; }
		} 
		#endregion

		#region Point References
		private Vector sourcePoint;
		/// <summary>
		/// Point in the map where the lane starts.
		/// </summary>
		public Vector SourcePoint
		{
			get { return sourcePoint; }
			set { sourcePoint = value; }
		}

		private Vector upperSourcePoint;
		/// <summary>
		/// Point in the map that corresponds to the upper point of the source node.
		/// </summary>
		public Vector UpperSourcePoint
		{
			get { return upperSourcePoint; }
			set { upperSourcePoint = value; }
		}

		private Vector lowerSourcePoint;
		/// <summary>
		/// Point in the map that corresponds to the lower point of the source node.
		/// </summary>
		public Vector LowerSourcePoint
		{
			get { return lowerSourcePoint; }
			set { lowerSourcePoint = value; }
		}

		private Vector destinationPoint;
		/// <summary>
		/// Point in the map where the lane ends.
		/// </summary>
		public Vector DestinationPoint
		{
			get { return destinationPoint; }
			set { destinationPoint = value; }
		}

		private Vector upperDestinationPoint;
		/// <summary>
		/// Point in the map that corresponds to the upper point of the destination node.
		/// </summary>
		public Vector UpperDestinationPoint
		{
			get { return upperDestinationPoint; }
			set { upperDestinationPoint = value; }
		}

		private Vector lowerDestinationPoint;
		/// <summary>
		/// Point in the map that corresponds to the lower point of the source node.
		/// </summary>
		public Vector LowerDestinationPoint
		{
			get { return lowerDestinationPoint; }
			set { lowerDestinationPoint = value; }
		} 
		#endregion
		#endregion

		#region Constructors
		/// <summary>
		/// Creates a new Lane object.
		/// </summary>
		/// <param name="parentEdge">Edge that contains this lane.</param>
		/// <param name="lanesNumber">Total number of lanes in the edge</param>
		/// <param name="laneNum">Number of the lane in the edge.</param>
		public Lane(MapEdge parentEdge, int lanesNumber, int laneNum)
		{
			this.edge = parentEdge;
			this.vehicles = new LinkedList<Vehicle>();
			this.toBeRemoved = new List<Vehicle>();
			this.laneNumber = laneNum;
			this.leftLane = null;
			this.rightLane = null;
			this.toLanes = new List<Lane>();

			#region Initialize point variables
			this.sourcePoint = new Vector(
				parentEdge.FromNode.Point.X +
				parentEdge.OrthogonalVector.X * (2 * laneNum - lanesNumber - 1) * Simulator.LaneWidth / 2,
				parentEdge.FromNode.Point.Y +
				parentEdge.OrthogonalVector.Y * (2 * laneNum - lanesNumber - 1) * Simulator.LaneWidth / 2);

			this.lowerSourcePoint = new Vector(parentEdge.FromNode.Point.X +
				parentEdge.OrthogonalVector.X * (2 * laneNum - lanesNumber) * Simulator.LaneWidth / 2,
				parentEdge.FromNode.Point.Y +
				parentEdge.OrthogonalVector.Y * (2 * laneNum - lanesNumber) * Simulator.LaneWidth / 2);

			this.upperSourcePoint = new Vector(parentEdge.FromNode.Point.X +
				parentEdge.OrthogonalVector.X * (2 * laneNum - lanesNumber - 2) * Simulator.LaneWidth / 2,
				parentEdge.FromNode.Point.Y +
				parentEdge.OrthogonalVector.Y * (2 * laneNum - lanesNumber - 2) * Simulator.LaneWidth / 2);

			this.destinationPoint = new Vector(
				parentEdge.ToNode.Point.X +
				parentEdge.OrthogonalVector.X * (2 * laneNum - lanesNumber - 1) * Simulator.LaneWidth / 2,
				parentEdge.ToNode.Point.Y +
				parentEdge.OrthogonalVector.Y * (2 * laneNum - lanesNumber - 1) * Simulator.LaneWidth / 2);

			this.lowerDestinationPoint = new Vector(
				parentEdge.ToNode.Point.X +
				parentEdge.OrthogonalVector.X * (2 * laneNum - lanesNumber) * Simulator.LaneWidth / 2,
				parentEdge.ToNode.Point.Y +
				parentEdge.OrthogonalVector.Y * (2 * laneNum - lanesNumber) * Simulator.LaneWidth / 2);

			this.upperDestinationPoint = new Vector(
				parentEdge.ToNode.Point.X +
				parentEdge.OrthogonalVector.X * (2 * laneNum - lanesNumber - 2) * Simulator.LaneWidth / 2,
				parentEdge.ToNode.Point.Y +
				parentEdge.OrthogonalVector.Y * (2 * laneNum - lanesNumber - 2) * Simulator.LaneWidth / 2);
			#endregion

			this.pointer = new Vector(this.sourcePoint, this.destinationPoint).ToUnitary();
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Runs a simulation step.
		/// </summary>
		/// <param name="simulationStep">Simulation step value.</param>
		public void SimulationStep(int simulationStep)
		{
			lock (vehicles)
			{
				Vehicle previousVehicle = null;
				foreach (Vehicle vehicle in vehicles)
				{
					if (previousVehicle == null)
					{
						previousVehicle = vehicle;
						continue;
					}

					previousVehicle.SimulationStep(simulationStep, vehicle);
					previousVehicle = vehicle;
				}
				if (vehicles.Count > 0)
					vehicles.Last.Value.SimulationStep(simulationStep, null);

				#region Clear vehicles that should not be in this lane
				foreach (Vehicle vehicle in toBeRemoved)
					vehicles.Remove(vehicle);
				toBeRemoved.Clear(); 
				#endregion
			}
		}

		/// <summary>
		/// Returns the point that corresponds to the position of a vehicle after some distance percurred in the lane.
		/// </summary>
		/// <param name="distance">Distance percurred by the vehicle.</param>
		public virtual Vector GetPoint(float distance)
		{
			return (sourcePoint + pointer * distance);
		}

		/// <summary>
		/// Returns the unitary vector that points in the sense of the lane in a given point.
		/// </summary>
		/// <param name="distance">Distance from the beggining of the lane to the requested point.</param>
		public virtual Vector GetPointer(float distance)
		{
			return Pointer;
		}
		#endregion
	}
}
