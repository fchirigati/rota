using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using TrafficSimulator;

namespace TrafficSimulator.Structures
{
	/// <summary>
	/// This class represents a lane.
	/// </summary>
	public class Lane
	{
		#region Fields
		private List<Vehicle> vehicles;
		/// <summary>
		/// Vehicles that are currently in the lane.
		/// </summary>
		public List<Vehicle> Vehicles
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

		private Vector sourcePoint;
		/// <summary>
		/// Point in the map where the lane starts.
		/// </summary>
		public Vector SourcePoint
		{
			get { return sourcePoint; }
		}

		private Vector upperSourcePoint;
		/// <summary>
		/// Point in the map that corresponds to the upper point of the source node.
		/// </summary>
		public Vector UpperSourcePoint
		{
			get { return upperSourcePoint; }
		}

		private Vector lowerSourcePoint;
		/// <summary>
		/// Point in the map that corresponds to the lower point of the source node.
		/// </summary>
		public Vector LowerSourcePoint
		{
			get { return lowerSourcePoint; }
		}

		private Vector destinationPoint;
		/// <summary>
		/// Point in the map where the lane ends.
		/// </summary>
		public Vector DestinationPoint
		{
			get { return destinationPoint; }
		}

		private Vector upperDestinationPoint;
		/// <summary>
		/// Point in the map that corresponds to the upper point of the destination node.
		/// </summary>
		public Vector UpperDestinationPoint
		{
			get { return upperDestinationPoint; }
		}

		private Vector lowerDestinationPoint;
		/// <summary>
		/// Point in the map that corresponds to the lower point of the source node.
		/// </summary>
		public Vector LowerDestinationPoint
		{
			get { return lowerDestinationPoint; }
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
			this.vehicles = new List<Vehicle>();
			this.toBeRemoved = new List<Vehicle>();
			this.laneNumber = laneNum;

			#region Initialize point variables
			this.sourcePoint = new Vector(
				parentEdge.FromNode.Point.X +
				parentEdge.OrthonalVector.X * (2 * laneNum - lanesNumber - 1) * Simulator.LaneWidth / 2,
				parentEdge.FromNode.Point.Y +
				parentEdge.OrthonalVector.Y * (2 * laneNum - lanesNumber - 1) * Simulator.LaneWidth / 2);

			this.lowerSourcePoint = new Vector(parentEdge.FromNode.Point.X +
				parentEdge.OrthonalVector.X * (2 * laneNum - lanesNumber) * Simulator.LaneWidth / 2,
				parentEdge.FromNode.Point.Y +
				parentEdge.OrthonalVector.Y * (2 * laneNum - lanesNumber) * Simulator.LaneWidth / 2);

			this.upperSourcePoint = new Vector(parentEdge.FromNode.Point.X +
				parentEdge.OrthonalVector.X * (2 * laneNum - lanesNumber - 2) * Simulator.LaneWidth / 2,
				parentEdge.FromNode.Point.Y +
				parentEdge.OrthonalVector.Y * (2 * laneNum - lanesNumber - 2) * Simulator.LaneWidth / 2);

			this.destinationPoint = new Vector(
				parentEdge.ToNode.Point.X +
				parentEdge.OrthonalVector.X * (2 * laneNum - lanesNumber - 1) * Simulator.LaneWidth / 2,
				parentEdge.ToNode.Point.Y +
				parentEdge.OrthonalVector.Y * (2 * laneNum - lanesNumber - 1) * Simulator.LaneWidth / 2);

			this.lowerDestinationPoint = new Vector(
				parentEdge.ToNode.Point.X +
				parentEdge.OrthonalVector.X * (2 * laneNum - lanesNumber) * Simulator.LaneWidth / 2,
				parentEdge.ToNode.Point.Y +
				parentEdge.OrthonalVector.Y * (2 * laneNum - lanesNumber) * Simulator.LaneWidth / 2);

			this.upperDestinationPoint = new Vector(
				parentEdge.ToNode.Point.X +
				parentEdge.OrthonalVector.X * (2 * laneNum - lanesNumber - 2) * Simulator.LaneWidth / 2,
				parentEdge.ToNode.Point.Y +
				parentEdge.OrthonalVector.Y * (2 * laneNum - lanesNumber - 2) * Simulator.LaneWidth / 2);
			#endregion

			this.pointer = new Vector(this.sourcePoint, this.destinationPoint);
		}
		#endregion
	}
}
