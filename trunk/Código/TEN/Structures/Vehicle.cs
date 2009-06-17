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
		/// Runs a simulation step.
		/// </summary>
		/// <param name="simulationStep">Simulation step value.</param>
		/// <param name="nextVehicle">Next vehicle in the lane. null if none</param>
		public void SimulationStep(int simulationStep, Vehicle nextVehicle)
		{
			position += speed * simulationStep / 1000;

			bool canIncreaseSpeed = false;
			if (nextVehicle != null)
			{
				if (nextVehicle.Position - position <= TENApp.simulator.SafetyDistance + length)
				{
					if (speed - deceleration > nextVehicle.speed)
					{
						speed = nextVehicle.speed;
					}
					else if (speed > nextVehicle.speed
						|| nextVehicle.Position - position <= TENApp.simulator.SafetyDistance)
					{
						speed -= deceleration * simulationStep / 1000;
					}

					if (speed < 0)
						speed = 0;
				}
				else
				{
					canIncreaseSpeed = true;
				}
			}
			else
			{
				canIncreaseSpeed = true;
			}

			if (canIncreaseSpeed)
			{
				speed += acceleration * simulationStep / 1000;
				if (speed >= lane.Edge.MaximumSpeed)
					speed = lane.Edge.MaximumSpeed;
			}

			if (position > lane.Length)
			{
				lane.ToBeRemoved.Add(this);
				if (lane.ToLanes.Count == 0)
				{
					// A final node has been reached.
				}
				else
				{
					// Vehicle reached other edge.
					Lane destLane = lane.ToLanes[TENApp.simulator.Random.Next(lane.ToLanes.Count)];
					
					lock (destLane.Vehicles)
					{
						position = 0;
						lane = destLane;
						destLane.Vehicles.AddFirst(this);
					}
				}
			}
		}

		/// <summary>
		/// Get the points in the map that bounds the car.
		/// </summary>
		public Point[] GetPoints()
		{
			// TO-DO: Fazer método mais eficiente, guardando pontos anteriores,
			// que só precise fazer soma de vetores [ou não...].
			Point[] points = new Point[4];
			Vector basePoint = lane.GetPoint(position);
			Vector baseVector = lane.GetPointer(position);

			basePoint += baseVector.Rotate90() * Simulator.VehicleWidth * 0.5F;
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
