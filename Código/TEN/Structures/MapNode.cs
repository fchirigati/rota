using System;
using System.Collections.Generic;
using System.Drawing;

namespace TEN.Structures
{
	/// <summary>
	/// This class represents a node in the map.
	/// </summary>
	public class MapNode
	{
		#region Constants
		/// <summary>
		/// Value that represents a non-existant node.
		/// </summary>
		public const MapNode NoNode = null;
		#endregion

		#region Fields
		private Point point;
		/// <summary>
		/// Point that corresponds to this node's position.
		/// </summary>
		public Point Point
		{
			get { return point; }
		}

		private List<MapEdge> inEdges;
		/// <summary>
		/// List of edges that enter this node.
		/// </summary>
		public List<MapEdge> InEdges
		{
			get { return inEdges; }
		}

		private List<MapEdge> outEdges;
		/// <summary>
		/// List of edges that exit this node.
		/// </summary>
		public List<MapEdge> OutEdges
		{
			get { return outEdges; }
		}

		private Semaphore semaphore;
		/// <summary>
		/// Semaphore object associated with this node.
		/// </summary>
		public Semaphore Semaphore
		{
			get { return semaphore; }
			set { semaphore = value; }
		}

		private MapEdge willEnter;
		/// <summary>
		/// Edge that has cars willing to enter the node.
		/// </summary>
		public MapEdge WillEnter
		{
			get { return willEnter; }
			set { willEnter = value; }
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Creates a new MapNode object.
		/// </summary>
		/// <param name="positionX">The X coordinate of the node.</param>
		/// <param name="positionY">The Y coordinate of the node.</param>
		public MapNode(float positionX, float positionY)
		{
			this.point = new Point((int)positionX, (int)positionY);
			this.inEdges = new List<MapEdge>();
			this.outEdges = new List<MapEdge>();
			this.semaphore = Semaphore.NoSemaphore;
			this.willEnter = null;
		}

		/// <summary>
		/// Creates a new MapNode object.
		/// </summary>
		/// <param name="positionPoint">The position of the node.</param>
		public MapNode(Point positionPoint)
			: this(positionPoint.X, positionPoint.Y)
		{ }

		/// <summary>
		/// Clones a MapNode object.
		/// </summary>
		/// <param name="mapNode">MapNode object to be cloned.</param>
		public MapNode(MapNode mapNode)
		{
			this.point.X = mapNode.Point.X;
			this.point.Y = mapNode.Point.Y;
			this.inEdges = mapNode.inEdges;
			this.outEdges = mapNode.outEdges;
			this.semaphore = mapNode.semaphore;
			this.willEnter = null;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// States if any connection edge of this node other than the 
		/// connection edge formed by the given edge is occupied.
		/// </summary>
		/// <param name="excludeEdge">Edge to be excluded when verifying if the connections are occupied.</param>
		public bool OccupiedConnection(MapEdge excludeEdge)
		{
			bool occupied = false;
			foreach (MapEdge edge in inEdges)
			{
				if (edge == excludeEdge)
					continue;

				foreach (Lane lane in edge.Lanes)
				{
					foreach (ConnectionLane connLane in lane.ToLanes)
					{
						if (connLane.Vehicles.Count > 0)
						{
							occupied = true;
							break;
						}
					}

					if (occupied)
						break;
				}

				if (occupied)
					break;
			}

			return occupied;
		}

		/// <summary>
		/// Evaluates the distance between this node and other point.
		/// </summary>
		/// <param name="positionX">The X coordinate of the other point.</param>
		/// <param name="positionY">The Y coordinate of the other point.</param>
		public float Distance(float positionX, float positionY)
		{
			return (float)Math.Sqrt((point.X - positionX) * (point.X - positionX)
				+ (point.Y - positionY) * (point.Y - positionY));
		}

		/// <summary>
		/// Evaluates the distance between this node and other point.
		/// </summary>
		/// <param name="otherPoint">The other point.</param>
		public float Distance(Point otherPoint)
		{
			return Distance(otherPoint.X, otherPoint.Y);
		}
		#endregion
	}
}
