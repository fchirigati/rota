using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TEN.Structures
{
	/// <summary>
	/// This class represents a car.
	/// </summary>
	public class Vehicle
	{
		#region Fields
		private int length;
		/// <summary>
		/// Length of the vehicle.
		/// </summary>
		public int Length
		{
			get { return length; }
			set { length = value; }
		}

		private float speed;
		/// <summary>
		/// Current vehicle speed.
		/// </summary>
		public float Speed
		{
			get { return speed; }
			set { speed = value; }
		}

		private float acceleration;
		/// <summary>
		/// This vehicle's acceleration.
		/// </summary>
		public float Acceleration
		{
			get { return acceleration; }
			set { acceleration = value; }
		}

		private float deceleration;
		/// <summary>
		/// Deceleration of the vehicle.
		/// </summary>
		public float Deceleration
		{
			get { return deceleration; }
			set { deceleration = value; }
		}

		private Color color;
		/// <summary>
		/// Color of the vehicle.
		/// </summary>
		public Color Color
		{
			get { return color; }
			set { color = value; }
		}

		private Lane lane;
		/// <summary>
		/// Current lane where the vehicle is.
		/// </summary>
		public Lane Lane
		{
			get { return lane; }
			set { lane = value; }
		}

		private float position;
		/// <summary>
		/// Current vehicle position in the lane.
		/// </summary>
		public float Position
		{
			get { return position; }
			set { position = value; }
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Vehicle's constructor.
		/// </summary>
		public Vehicle(int vehicleLength, float vehicleAcceleration, float vehicleDeceleration, 
			float initialSpeed, Color vehicleColor, Lane currentLane)
		{
			this.length = vehicleLength;
			this.speed = initialSpeed;
			this.acceleration = vehicleAcceleration;
			this.deceleration = vehicleDeceleration;
			this.color = vehicleColor;
			this.lane = currentLane;
			this.position = 0;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Get the points in the map that bounds the car.
		/// </summary>
		public Point[] GetPoints()
		{
			// TO-DO: Fazer método mais eficiente, guardando pontos anteriores,
			// que só precise fazer soma de vetores [ou não...].
			Point[] points = new Point[4];
			Vector basePoint = lane.SourcePoint + position * lane.Pointer;
			Vector baseVector = new Vector(lane.Pointer.X, lane.Pointer.Y);

			basePoint += baseVector.Rotate90() * Simulator.VehicleWidth * (float)0.5;
			points[0] = new Point((int)basePoint.X, (int)basePoint.Y);

			basePoint += baseVector.Rotate270() * length;
			points[1] = new Point((int)basePoint.X, (int)basePoint.Y);

			basePoint += baseVector.Rotate270() * Simulator.VehicleWidth;
			points[2] = new Point((int)basePoint.X, (int)basePoint.Y);

			basePoint += baseVector.Rotate270() * length;
			points[3] = new Point((int)basePoint.X, (int)basePoint.Y);

			return points;
		}
		#endregion
	}
}
