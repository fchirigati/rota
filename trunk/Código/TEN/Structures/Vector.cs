using System;
using System.Collections.Generic;
using System.Drawing;

namespace TEN.Structures
{
	/// <summary>
	/// A struct that represents a bi-dimensional unitary vector.
	/// </summary>
	public struct Vector
	{
		#region Fields
		private float x;
		/// <summary>
		/// The X coordinate of this vector.
		/// </summary>
		public float X
		{
			get { return x; }
			set { x = value; }
		}

		private float y;
		/// <summary>
		/// The Y coordinate of this vector.
		/// </summary>
		public float Y
		{
			get { return y; }
			set { y = value; }
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Creates a vector given its coordinates.
		/// </summary>
		/// <param name="coordinateX">X coordinate of the vector.</param>
		/// <param name="coordinateY">Y coordinate of the vector.</param>
		public Vector(float coordinateX, float coordinateY)
		{
			this.x = coordinateX;
			this.y = coordinateY;
		}

		/// <summary>
		/// Creates a vector based on two points.
		/// </summary>
		/// <param name="from">The point that this vector comes from.</param>
		/// <param name="to">The point that this vector is pointing to.</param>
		public Vector(Point from, Point to)
		{
			this.x = to.X - from.X;
			this.y = to.Y - from.Y;
		}

		/// <summary>
		/// Creates a vector based on two vectors.
		/// </summary>
		/// <param name="from">The point that this vector comes from.</param>
		/// <param name="to">The point that this vector is pointing to.</param>
		public Vector(Vector from, Vector to)
			: this(from.ToPoint(), to.ToPoint())
		{ }
		#endregion

		#region Public Methods
		/// <summary>
		/// Sums two vectors.
		/// </summary>
		public static Vector operator +(Vector leftSide, Vector rightSide)
		{
			return new Vector(leftSide.X + rightSide.X, leftSide.Y + rightSide.Y);
		}

		/// <summary>
		/// Subtracts two vectors. :D
		/// </summary>
		public static Vector operator -(Vector leftSide, Vector rightSide)
		{
			return new Vector(leftSide.X - rightSide.X, leftSide.Y - rightSide.Y);
		}

		/// <summary>
		/// Returns the dot product between two vectors.
		/// </summary>
		public static float operator *(Vector leftSide, Vector rightSide)
		{
			return leftSide.X * rightSide.X + leftSide.Y * rightSide.Y;
		}

		/// <summary>
		/// Multiplies a vector by a float.
		/// </summary>
		public static Vector operator *(float leftSide, Vector rightSide)
		{
			return new Vector(rightSide.X * leftSide, rightSide.Y * leftSide);
		}
		public static Vector operator *(Vector leftSide, float rightSide)
		{
			return rightSide * leftSide;
		}

		/// <summary>
		/// Returns the euclidean distance between this vector (as a point) and another one.
		/// </summary>
		/// <param name="secondVector">Vector to be compared.</param>
		public float Distance(Vector secondVector)
		{
			return (float)Math.Sqrt((x - secondVector.X) * (x - secondVector.X) + (y - secondVector.Y) * (y - secondVector.Y));
		}

		/// <summary>
		/// Returns the Point-equivalent object of this Vector.
		/// </summary>
		public Point ToPoint()
		{
			return (new Point((int)x, (int)y));
		}

		public PointF ToPointF()
		{
			return new PointF(x, y);
		}

		/// <summary>
		/// Transforms the vector in an unitary vector and returns itself.
		/// </summary>
		public Vector ToUnitary()
		{
			float size = (float)Math.Sqrt(x * x + y * y);

			x /= size;
			y /= size;

			return this;
		}

		/// <summary>
		/// Rotates the vector by 90 degrees counter-clockwise and returns itself.
		/// </summary>
		public Vector Rotate90()
		{
			float temp = x;
			x = -y;
			y = temp;

			return this;
		}

		/// <summary>
		/// Rotates the vector by 180 degrees and returns itself.
		/// </summary>
		public Vector Rotate180()
		{
			x = -x;
			y = -y;

			return this;
		}

		/// <summary>
		/// Rotates the vector by 270 degrees counter-clockwise and returns itself.
		/// </summary>
		public Vector Rotate270()
		{
			float temp = y;
			y = -x;
			x = temp;

			return this;
		}
		#endregion
	}
}
