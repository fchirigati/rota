using System;
using System.Drawing;
using System.Collections.Generic;
using TEN.Util;

namespace TEN.Structures
{
	/// <summary>
	/// This class represets a edge in the map.
	/// </summary>
	public class MapEdge
	{
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

		private List<MapEdge> inConnections;
		/// <summary>
		/// List of connection edges that arrives to this edge.
		/// </summary>
		public List<MapEdge> InConnections
		{
			get { return inConnections; }
		}

		private List<MapEdge> outConnections;
		/// <summary>
		/// List of connection edges that goes from this edge.
		/// </summary>
		public List<MapEdge> OutConnections
		{
			get { return outConnections; }
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

		private int trimSizeTo;
		/// <summary>
		/// Trimmed size (in number of lanes) of the destination side.
		/// </summary>
		public int TrimSizeTo
		{
			get { return trimSizeTo; }
		}

		private int trimSizeFrom;
		/// <summary>
		/// Trimmed size (in number of lanes) of the origin side.
		/// </summary>
		public int TrimSizeFrom
		{
			get { return trimSizeFrom; }
		}

		private List<PointF[]> boundings;
		/// <summary>
		/// List of quadrangles that bounds this edge.
		/// </summary>
		public List<PointF[]> Boundings
		{
			get { return boundings; }
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Creates a new MapEdge object.
		/// </summary>
		/// <param name="lanesNumber">Number of lanes that this edge contains.</param>
		/// <param name="from">The node that this edge is coming from.</param>
		/// <param name="to">The node that this edge is pointing to.</param>
		/// <param name="maxSpeed">Maximum speed allowed in this edge.</param>
		public MapEdge(int lanesNumber, MapNode from, MapNode to, float maxSpeed)
		{
			this.lanes = new List<Lane>();
			this.fromNode = from;
			this.toNode = to;
			this.inConnections = new List<MapEdge>();
			this.outConnections = new List<MapEdge>();
			this.senseVector = new Vector(from.Point, to.Point).ToUnitary();
			this.orthogonalVector = new Vector(from.Point, to.Point).ToUnitary().Rotate90();
			this.maximumSpeed = maxSpeed;
			this.length = to.Distance(from.Point.X, from.Point.Y);
			this.trimSizeTo = 0;
			this.trimSizeFrom = 0;
			this.boundings = new List<PointF[]>();

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
			TrimToEdge();
			TrimFromEdge(); 
			#endregion

			SetBoundings();
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
		/// States if this edge can be trimmed on the destination side by a given value.
		/// </summary>
		/// <param name="size">Trim size in number of edges.</param>
		public bool CanTrimTo(int size)
		{
			if (size <= trimSizeTo)
				return true;

			return (length + trimSizeTo * Simulator.LaneWidth >= size * Simulator.LaneWidth);
		}

		/// <summary>
		/// States if this edge can be trimmed on the origin side by a given value.
		/// </summary>
		/// <param name="size">Trim size in number of edges.</param>
		public bool CanTrimFrom(int size)
		{
			if (size <= trimSizeFrom)
				return true;

			return (length + trimSizeFrom * Simulator.LaneWidth >= size * Simulator.LaneWidth);
		}

		/// <summary>
		/// States if a given point is inside the edge's boundings.
		/// </summary>
		/// <param name="x">X coordinate of the point.</param>
		/// <param name="y">Y coordinate of the point.</param>
		public bool PointInsideEdge(float x, float y)
		{
			bool inside = false;

			foreach (PointF[] quadrangle in boundings)
			{
				if (Polygons.InsideQuadrangle(quadrangle, new Vector(x, y)))
				{
					inside = true;
					break;
				}
			}

			return inside;
		}

		/// <summary>
		/// Sets this edge's boundings.
		/// </summary>
		public void SetBoundings()
		{
			boundings.Clear();

			PointF[] mainBoundings = new PointF[4];
			mainBoundings[0] = lanes[0].UpperSourcePoint.ToPointF();
			mainBoundings[1] = lanes[0].UpperDestinationPoint.ToPointF();
			mainBoundings[2] = lanes[lanes.Count - 1].LowerDestinationPoint.ToPointF();
			mainBoundings[3] = lanes[lanes.Count - 1].LowerSourcePoint.ToPointF();
			boundings.Add(mainBoundings);

			foreach (MapEdge edge in inConnections)
			{
				for (int i = 0; i < 10; i++)
				{
					PointF[] connectionBoundings = new PointF[4];
					float distance01 = i * edge.lanes[0].Length / 10;
					float distance02 = (i + 1) * edge.lanes[0].Length / 10;
					float distance11 = i * edge.lanes[edge.lanes.Count - 1].Length / 10;
					float distance12 = (i + 1) * edge.lanes[edge.lanes.Count - 1].Length / 10;

					connectionBoundings[0] = (edge.lanes[0].GetPoint(distance01) -
						edge.lanes[0].GetPointer(distance01).NewRotated90() * Simulator.LaneWidth * 0.5F).ToPointF();
					connectionBoundings[1] = (edge.lanes[0].GetPoint(distance02) -
						edge.lanes[0].GetPointer(distance02).NewRotated90() * Simulator.LaneWidth * 0.5F).ToPointF();
					connectionBoundings[2] = (edge.lanes[edge.lanes.Count - 1].GetPoint(distance12) +
						edge.lanes[edge.lanes.Count - 1].GetPointer(distance12).NewRotated90() * Simulator.LaneWidth * 0.5F).ToPointF();
					connectionBoundings[3] = (edge.lanes[edge.lanes.Count - 1].GetPoint(distance11) +
						edge.lanes[edge.lanes.Count - 1].GetPointer(distance11).NewRotated90() * Simulator.LaneWidth * 0.5F).ToPointF();

					boundings.Add(connectionBoundings);
				}
			}
		}

		/// <summary>
		/// Trims edges on the origin side. Returns false if it is not possible to trim, true otherwise.
		/// </summary>
		public bool TrimFromEdge()
		{
			int maxLanesNum = lanes.Count;
			foreach (MapEdge edge in fromNode.InEdges)
				maxLanesNum = Math.Max(maxLanesNum, edge.Lanes.Count);

			if (fromNode.InEdges.Count == 0)
				maxLanesNum = 0;

			if (maxLanesNum == trimSizeFrom)
				return true;

			if (!CanTrimFrom(maxLanesNum))
				return false;

			float adjustTrimSize = (maxLanesNum - trimSizeFrom) * Simulator.LaneWidth;
			Vector trimVector = adjustTrimSize * senseVector;

			length -= adjustTrimSize;

			foreach (Lane lane in lanes)
			{	
				lane.SourcePoint += trimVector;
				lane.LowerSourcePoint += trimVector;
				lane.UpperSourcePoint += trimVector;
			}
			trimSizeFrom = maxLanesNum;

			SetBoundings();

			return true;
		}

		/// <summary>
		/// Trims edges on the destination side. Returns false if it is not possible to trim, true otherwise.
		/// </summary>
		public bool TrimToEdge()
		{
			int maxLanesNum = lanes.Count;
			foreach (MapEdge edge in toNode.OutEdges)
				maxLanesNum = Math.Max(maxLanesNum, edge.Lanes.Count);

			if (toNode.OutEdges.Count == 0)
				maxLanesNum = 0;

			if (maxLanesNum == trimSizeTo)
				return true;

			if (!CanTrimFrom(maxLanesNum))
				return false;

			float adjustTrimSize = (maxLanesNum - trimSizeTo) * Simulator.LaneWidth;
			Vector trimVector = adjustTrimSize * senseVector;

			length -= adjustTrimSize;

			foreach (Lane lane in lanes)
			{
				lane.DestinationPoint -= trimVector;
				lane.LowerDestinationPoint -= trimVector;
				lane.UpperDestinationPoint -= trimVector;
			}
			trimSizeTo = maxLanesNum;

			SetBoundings();

			return true;
		}
		#endregion
	}
}
