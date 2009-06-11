using System;
using System.Collections.Generic;
using TEN.Structures;
using System.Linq;
using System.Text;

namespace TEN.Structures
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

		private Vector senseVector;
		/// <summary>
		/// Unitary vector that indicates the sense of this edge.
		/// </summary>
		public Vector SenseVector
		{
			get { return senseVector; }
		}

		private Vector orthogonalVector;
		/// <summary>
		/// Unitary orthogonal vector related to the edge.
		/// </summary>
		public Vector OrthogonalVector
		{
			get { return orthogonalVector; }
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
			this.senseVector = new Vector(from.Point, to.Point).ToUnitary();
			this.orthogonalVector = new Vector(from.Point, to.Point).ToUnitary().Rotate90();
			this.maximumSpeed = defaultMaximumSpeed;
			this.length = to.Distance(from.Point.X, from.Point.Y);

			for (int i = 1; i <= lanesNumber; i++)
				this.lanes.Add(new Lane(this, lanesNumber, i));

			#region Set right and left lane references for each lane
			for (int i = 1; i < lanesNumber - 1; i++)
			{
				this.lanes[i].LeftLane = this.lanes[i - 1];
				this.lanes[i].RightLane = this.lanes[i + 1];
			}
			if (this.lanes.Count > 1)
			{
				this.lanes[0].RightLane = this.lanes[1];
				this.lanes[lanesNumber - 1].LeftLane = this.lanes[lanesNumber - 2];
			}
			#endregion

			#region Trim edges if necessary
			if (toNode.OutEdges.Count > 0)
				TrimToEdge();
			if (fromNode.InEdges.Count > 0)
				TrimFromEdge(); 
			#endregion
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
				lane.SimulationStep(simulationStep);
		}

		/// <summary>
		/// Trims edges.
		/// </summary>
		public void TrimFromEdge()
		{
			float trimSize = lanes.Count * Simulator.LaneWidth * 0.75F;
			Vector trimVector = trimSize * senseVector;

			length -= trimSize;

			foreach (Lane lane in lanes)
			{	
				lane.SourcePoint += trimVector;
				lane.LowerSourcePoint += trimVector;
				lane.UpperSourcePoint += trimVector;
			}
		}

		/// <summary>
		/// Trims edges.
		/// </summary>
		public void TrimToEdge()
		{
			float trimSize = lanes.Count * Simulator.LaneWidth * 0.75F;
			Vector trimVector = trimSize * senseVector;

			length -= trimSize;

			foreach (Lane lane in lanes)
			{
				lane.DestinationPoint -= trimVector;
				lane.LowerDestinationPoint -= trimVector;
				lane.UpperDestinationPoint -= trimVector;
			}
		}
		#endregion
	}
}
