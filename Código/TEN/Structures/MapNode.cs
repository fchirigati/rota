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
		#endregion

		#region Constructors
		/// <summary>
		/// Creates a new MapNode object.
		/// </summary>
		/// <param name="positionX">The X coordinate of the node.</param>
		/// <param name="positionY">The Y coordinate of the node.</param>
		public MapNode(int positionX, int positionY)
		{
			this.point = new Point(positionX, positionY);
			this.inEdges = new List<MapEdge>();
			this.outEdges = new List<MapEdge>();
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
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Evaluates the distance between this node and other point.
		/// </summary>
		/// <param name="positionX">The X coordinate of the other point.</param>
		/// <param name="positionY">The Y coordinate of the other point.</param>
		public float Distance(int positionX, int positionY)
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
