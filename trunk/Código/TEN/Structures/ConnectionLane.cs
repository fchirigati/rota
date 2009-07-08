using System;
using System.Drawing;
using System.Collections.Generic;
using TEN.ThreadManagers;

namespace TEN.Structures
{
	/// <summary>
	/// Lane that is created by the simulator when two edges are connected.
	/// </summary>
	public class ConnectionLane : Lane
	{
		#region Fields
		private float length;
		/// <summary>
		/// Length of this lane.
		/// </summary>
		public override float Length
		{
			get { return length; }
		}

		private Lane fromLane;
		/// <summary>
		/// Lane from which this lane comes from.
		/// </summary>
		public Lane FromLane
		{
			get { return fromLane; }
		}

		private Lane toLane;
		/// <summary>
		/// Lane to which this lane goes to.
		/// </summary>
		public Lane ToLane
		{
			get { return toLane; }
		}

		private Vector[] bezierVectors;
		private PointF[] bezierPoints;
		/// <summary>
		/// Ordenated list of bezier points of this lane.
		/// </summary>
		public PointF[] BezierPoints
		{
			get { return bezierPoints; }
		}

		private PointF[] upperBezierPoints;
		/// <summary>
		/// Ordernated list of the upper bezier points of this lane.
		/// </summary>
		public PointF[] UpperBezierPoints
		{
			get { return upperBezierPoints; }
		}

		private PointF[] lowerBezierPoints;
		/// <summary>
		/// Ordernated list of the lower bezier points of this lane.
		/// </summary>
		public PointF[] LowerBezierPoints
		{
			get { return lowerBezierPoints; }
		}
		#endregion

		#region Constructors
		public ConnectionLane(Lane parentLane, Lane targetLane)
			: base(parentLane.Edge, 0, 0)
		{
			this.fromLane = parentLane;
			this.toLane = targetLane;
			this.bezierPoints = SetBezierPoints(fromLane.DestinationPoint, 
				toLane.SourcePoint, fromLane.Pointer, toLane.Pointer);
			this.upperBezierPoints = SetBezierPoints(fromLane.UpperDestinationPoint,
				toLane.UpperSourcePoint, fromLane.Pointer, toLane.Pointer);
			this.lowerBezierPoints = SetBezierPoints(fromLane.LowerDestinationPoint,
				toLane.LowerSourcePoint, fromLane.Pointer, toLane.Pointer);
			this.bezierVectors = new Vector[4];

			#region Set bezier vectors
			this.bezierVectors[0] = fromLane.DestinationPoint;
			this.bezierVectors[1] = new Vector(bezierPoints[1].X, bezierPoints[1].Y);
			this.bezierVectors[2] = new Vector(bezierPoints[2].X, bezierPoints[2].Y);
			this.bezierVectors[3] = toLane.SourcePoint;
			#endregion

			this.length = CalculateLength(30);
		}
		#endregion

		#region Private Methods
		private PointF[] SetBezierPoints(Vector from, Vector to, Vector pointerFrom, Vector pointerTo)
		{
			PointF[] beziers = new PointF[4];

			beziers[0] = from.ToPointF();
			
			Vector moveVector = new Vector(from, to);
			float moveSize = moveVector.GetSize() * 0.333F;

			beziers[1] = (from + pointerFrom * moveSize).ToPointF();
			beziers[2] = (to - pointerTo * moveSize).ToPointF();
			// usando intersecção
#if false
			Vector rotatedUnitary = new Vector(pointerFrom.X, pointerFrom.Y).Rotate90();
			Vector firstPoint, secondPoint;
			float denominator = rotatedUnitary * pointerTo;
			if (Math.Abs(denominator) <= 0.1)
			{
				// Connected lanes are almost in the same direction.
				firstPoint = from + moveVector * 0.333F;
				secondPoint = from + moveVector * 0.667F;
				beziers[1] = firstPoint.ToPointF();
				beziers[2] = secondPoint.ToPointF();
			}
			else
			{
				float coefficient = rotatedUnitary * moveVector / denominator;
				Vector intersection = to - coefficient * pointerTo;

				firstPoint = from + (intersection - from) * 0.5F;
				secondPoint = to - (to - intersection) * 0.5F;
				beziers[1] = firstPoint.ToPointF();
				beziers[2] = secondPoint.ToPointF();
			} 
#endif

			beziers[3] = to.ToPointF();

			return beziers;
		}

		/// <summary>
		/// Calculate the approximate length of this lane.
		/// </summary>
		/// <param name="iterations">Number of iterations used to calculate the approximate length.</param>
		private float CalculateLength(int iterations)
		{
			float size = 0;
			Vector[] pts = new Vector[iterations];

			if (iterations <= 4)
			{
				size = bezierVectors[0].Distance(bezierVectors[1])
					+ bezierVectors[1].Distance(bezierVectors[2])
					+ bezierVectors[2].Distance(bezierVectors[3]);

				return size;
			}

			for (int i = 0; i < iterations; i++)
				pts[i] = GetCurvePoint((float)i/iterations);

			for (int i = 0; i < iterations - 1; i++)
			{
				size += pts[i].Distance(pts[i + 1]);
			}

			return size;
		}

		/// <summary>
		/// Returns the point that corresponds to the position of a vehicle after some distance percurred in the lane.
		/// </summary>
		/// <param name="t">Bezier curve parameter between 0 and 1.</param>
		private Vector GetCurvePoint(float t)
		{
			if (t < 0 || t > 1)
				throw new Exception("Requested point is not valid (parameter t is greater than 1 or negative).");

			float ct = 1 - t;

			Vector retVector = ct * ct * ct * bezierVectors[0] + 3 * ct * ct * t * bezierVectors[1]
				+ 3 * ct * t * t * bezierVectors[2] + t * t * t * bezierVectors[3];

			return retVector;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Returns the point that corresponds to the position of a vehicle after some distance percurred in the lane.
		/// </summary>
		/// <param name="distance">Distance percurred by the vehicle.</param>
		public override Vector GetPoint(float distance)
		{
			return GetCurvePoint(distance / length);
		}

		/// <summary>
		/// Returns the unitary vector that points in the sense of the lane in a given point.
		/// </summary>
		/// <param name="distance">Distance from the beggining of the lane to the requested point.</param>
		public override Vector GetPointer(float distance)
		{
			float t = distance / length;
			float ct = 1 - t;

			Vector retPointer = ct * ct * (bezierVectors[1] - bezierVectors[0])
				+ 2 * ct * t * (bezierVectors[2] - bezierVectors[1])
				+ t * t * (bezierVectors[3] - bezierVectors[2]);

			return retPointer.ToUnitary();
		}
		#endregion
	}
}
