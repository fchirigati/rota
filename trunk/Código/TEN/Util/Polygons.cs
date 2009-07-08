using System.Collections.Generic;
using System.Drawing;
using TEN.Structures;

namespace TEN.Util
{
	/// <summary>
	/// Class that encapuslates miscelaneous polygon-related methods.
	/// </summary>
	public static class Polygons
	{
		/// <summary>
		/// States if a given point is inside a triangle.
		/// Reference: http://www.blackpawn.com/texts/pointinpoly/default.html
		/// </summary>
		/// <param name="trianglePts">Array of vectors that represents the points of the triangle.</param>
		/// <param name="point">Point to be checked.</param>
		static public bool InsideTriangle(PointF[] trianglePts, Vector point)
		{
			Vector v0 = new Vector(trianglePts[0], trianglePts[2]);
			Vector v1 = new Vector(trianglePts[0], trianglePts[1]);
			Vector v2 = new Vector(trianglePts[0], point.ToPointF());

			float dot00 = v0 * v0;
			float dot01 = v0 * v1;
			float dot02 = v0 * v2;
			float dot11 = v1 * v1;
			float dot12 = v1 * v2;

			float denominator = (dot00 * dot11 - dot01 * dot01);
			float u = (dot11 * dot02 - dot01 * dot12) / denominator;
			float v = (dot00 * dot12 - dot01 * dot02) / denominator;

			return (u > 0) && (v > 0) && (u + v < 1);
		}

		/// <summary>
		/// States if a given point is inside a quadrangle.
		/// </summary>
		/// <param name="quadPts">Array of PointF objects that represents the vertex of the quadrangle.</param>
		/// <param name="point">Point to be checked.</param>
		static public bool InsideQuadrangle(PointF[] quadPts, Vector point)
		{
			PointF[] triangle1 = new PointF[3];
			PointF[] triangle2 = new PointF[3];

			for (int i = 0; i < 3; i++)
			{
				triangle1[i] = quadPts[i];
				triangle2[i] = quadPts[(i + 2) % 4];
			}

			return InsideTriangle(triangle1, point) || InsideTriangle(triangle2, point);
		}
	}
}