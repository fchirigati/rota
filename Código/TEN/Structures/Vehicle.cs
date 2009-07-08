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

		private float totalDistance;
		/// <summary>
		/// Total distance percurred by this vehicle.
		/// </summary>
		public float TotalDistance
		{
			get { return totalDistance; }
		}

		private uint totalSteps;
		/// <summary>
		/// Number of simulation steps that this vehicle has done since its creation.
		/// </summary>
		public uint TotalSteps
		{
			get { return totalSteps; }
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
			this.totalDistance = 0;
			this.totalSteps = 0;
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
			position += (speed * simulationStep) * 0.001F;
			totalDistance += (speed * simulationStep) * 0.001F;
			totalSteps++;

			MapNode toNode = lane.Edge.ToNode;
			bool canIncreaseSpeed = true;
			bool shouldDecelerate = false;

			// Next vehicle of this lane
			if (nextVehicle != null)
			{
				float distance = nextVehicle.Position - position - length;
				float warningDistance = Math.Max(speed * TENApp.simulator.SafetyDistance * 0.1F,
					TENApp.simulator.SafetyDistance);
				if (distance <= warningDistance)
				{
					if (distance < TENApp.simulator.SafetyDistance || speed > nextVehicle.Speed)
						shouldDecelerate = true;

					canIncreaseSpeed = false;
				}
			}

			// Next lane vehicle
			if (canIncreaseSpeed && lane.ToLanes.Count > 0)
			{
				float warningDistance = Math.Max(speed * TENApp.simulator.SafetyDistance * 0.1F,
					TENApp.simulator.SafetyDistance);
				float distanceToNextLane = lane.Length - position - length;

				foreach (Lane toLane in lane.ToLanes)
				{
					if (toLane.Vehicles.Count == 0)
						continue;

					Vehicle nextLaneVehicle = toLane.Vehicles.First.Value;
					float distance = nextLaneVehicle.Position + distanceToNextLane;

					if (distance <= warningDistance)
					{
						if (distance < TENApp.simulator.SafetyDistance || speed > nextLaneVehicle.Speed)
							shouldDecelerate = true;

						canIncreaseSpeed = false;
						break;
					}
				}
			}

			// Occupied connection
			if (!shouldDecelerate && toNode.OutEdges.Count > 0 && lane.GetType() != typeof(ConnectionLane))
			{
				float distance = lane.Length - position - length;
				float warningDistance = Math.Max(speed * TENApp.simulator.SafetyDistance * 0.1F,
					TENApp.simulator.SafetyDistance);

				if (distance <= warningDistance && distance > 0 && toNode.OccupiedConnection(lane.Edge))
				{
					shouldDecelerate = true;
					canIncreaseSpeed = false;
				}
			}

			// Semaphore
			if (!shouldDecelerate)
			{
				// No vehicles ahead in the warning distance of this lane.

				Semaphore semaphore = toNode.Semaphore;
				if (semaphore != Semaphore.NoSemaphore && lane.GetType() != typeof(ConnectionLane))
				{
					// Destination node has a semaphore.
					float distance = lane.Length - position - length;
					float warningDistance = Math.Max(speed * TENApp.simulator.SafetyDistance * 0.1F,
						TENApp.simulator.SafetyDistance);

					if (semaphore.CurrentEdge != lane.Edge || semaphore.CurrentEdgeState == Semaphore.State.Yellow)
					{
						// Traffic light is red or yellow.
						if (distance <= warningDistance && distance > 0)
						{
							shouldDecelerate = true;
							canIncreaseSpeed = false;
						}
					}
				}
			}

			// Intersection without semaphore.
			if (!shouldDecelerate && toNode.InEdges.Count > 1 && 
				toNode.Semaphore == Semaphore.NoSemaphore && lane.GetType() != typeof(ConnectionLane))
			{
				float distance = lane.Length - position - length;
				float warningDistance = Math.Max(speed * TENApp.simulator.SafetyDistance * 0.05F,
					TENApp.simulator.SafetyDistance);

				if (distance <= warningDistance)
				{
					if (toNode.WillEnter == null)
					{
						toNode.WillEnter = lane.Edge;
					}
					else if (toNode.WillEnter != lane.Edge)
					{
						shouldDecelerate = true;
						canIncreaseSpeed = false;
					}

					if (speed > TENApp.simulator.WarningSpeed)
					{
						shouldDecelerate = true;
						canIncreaseSpeed = false;
					}
				}
			}

			if (shouldDecelerate)
			{
				speed -= deceleration * simulationStep * 0.001F;
				if (speed < 0)
					speed = 0;
			}
			if (canIncreaseSpeed)
			{
				speed += acceleration * simulationStep * 0.001F;
				if (speed >= lane.Edge.MaximumSpeed)
					speed = lane.Edge.MaximumSpeed;
			}

			if (position > lane.Length)
			{
				lane.ToBeRemoved.Add(this);
				if (lane.ToLanes.Count == 0)
				{
					// A final node has been reached.
					TENApp.simulator.CarsOut++;
					TENApp.simulator.AverageSpeedSum += 1000 * totalDistance / (totalSteps * TENApp.simulator.SimulationStepTime);
				}
				else
				{
					// Vehicle reached other edge.
					if (toNode.WillEnter == lane.Edge)
						toNode.WillEnter = null;

					Lane destLane = lane.ToLanes[TENApp.simulator.Random.Next(lane.ToLanes.Count)];
					
					lock (destLane.Vehicles)
					{
						position -= lane.Length;
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
