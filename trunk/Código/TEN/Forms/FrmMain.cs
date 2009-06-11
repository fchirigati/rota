using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;
using TEN.Structures;

namespace TEN.Forms
{
	/// <summary>
	/// TO-DO
	/// </summary>
	public partial class FrmMain : Form
	{
		#region Fields
		private bool running;
		/// <summary>
		/// States if the form's constructor has been executed gracefully.
		/// </summary>
		public bool Running
		{
			get { return running; }
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes the form.
		/// </summary>
		public FrmMain()
		{
			this.running = false;

			InitializeComponent();

			this.btnPause.Enabled = false;
			this.btnStop.Enabled = false;
			this.btnRestart.Enabled = false;

			this.running = true;
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Sets a new state to the application, making the necessary changes.
		/// </summary>
		/// <param name="newState">New state of the application.</param>
		private void SetState(AppState newState)
		{
			if (TENApp.state == newState)
				return;

			mapDrawer.ClearClickedPoints();
			TENApp.state = newState;

			switch (newState)
			{
				case AppState.SimulationRunning:
					mapDrawer.ClearClickedPoints();

					CheckButton(btnPointer);
					UncheckButton(btnNewRoad);
					UncheckButton(btnNewTrafficLight);

					btnNewRoad.Enabled = false;
					btnNewTrafficLight.Enabled = false;
					btnStart.Enabled = false;
					btnPause.Enabled = true;
					btnStop.Enabled = true;
					btnRestart.Enabled = true;

					TENApp.simulator.Start();
					break;

				case AppState.SimulationPaused:
					if (TENApp.simulator.IsSimulating)
						TENApp.simulator.Pause();

					mapDrawer.ClearClickedPoints();

					CheckButton(btnPointer);
					UncheckButton(btnNewRoad);
					UncheckButton(btnNewTrafficLight);

					btnNewRoad.Enabled = false;
					btnNewTrafficLight.Enabled = false;
					btnStart.Enabled = true;
					btnPause.Enabled = false;
					btnStop.Enabled = true;
					btnRestart.Enabled = true;
					break;

				case AppState.EditingPointer:
					if (TENApp.simulator.IsSimulating)
						TENApp.simulator.Stop();

					mapDrawer.ClearClickedPoints();

					CheckButton(btnPointer);
					UncheckButton(btnNewRoad);
					UncheckButton(btnNewTrafficLight);

					btnPointer.Enabled = true;
					btnNewRoad.Enabled = true;
					btnNewTrafficLight.Enabled = true;
					btnStart.Enabled = true;
					btnPause.Enabled = false;
					btnStop.Enabled = false;
					btnRestart.Enabled = false;

					mapDrawer.Refresh();
					break;

				case AppState.EditingNewRoad:
					UncheckButton(btnPointer);
					CheckButton(btnNewRoad);
					UncheckButton(btnNewTrafficLight);
					mapDrawer.Refresh();
					break;

				case AppState.EditingNewTrafficLight:
					mapDrawer.ClearClickedPoints();
					UncheckButton(btnPointer);
					UncheckButton(btnNewRoad);
					CheckButton(btnNewTrafficLight);
					mapDrawer.Refresh();
					break;

				case AppState.Other:
					break;
			}
		}

		/// <summary>
		/// TO-DO
		/// </summary>
		/// <param name="button"></param>
		private void CheckButton(ToolStripButton button)
		{
			button.Checked = true;
		}

		/// <summary>
		/// TO-DO
		/// </summary>
		/// <param name="button"></param>
		private void UncheckButton(ToolStripButton button)
		{
			button.Checked = false;
		}
		#endregion

		#region Event Handlers
		private void btnPointer_Click(object sender, EventArgs e)
		{
			SetState(AppState.EditingPointer);
		}

		private void btnNewRoad_Click(object sender, EventArgs e)
		{
			SetState(AppState.EditingNewRoad);
		}

		private void btnNewTrafficLight_Click(object sender, EventArgs e)
		{
			SetState(AppState.EditingNewTrafficLight);
		}

		private void btnStart_Click(object sender, EventArgs e)
		{
			SetState(AppState.SimulationRunning);
		}

		private void btnPause_Click(object sender, EventArgs e)
		{
			SetState(AppState.SimulationPaused);
		}

		private void btnStop_Click(object sender, EventArgs e)
		{
			SetState(AppState.EditingPointer);
		}

		private void btnRestart_Click(object sender, EventArgs e)
		{
			SetState(AppState.EditingPointer);
			SetState(AppState.SimulationRunning);
		}

		private void FrmMain_Load(object sender, EventArgs e)
		{
			mapDrawer.AutoScrollPosition = new Point(5000, 5000);
		}

		private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			TENApp.Shutdown();
		}

		private void btnZoomIn_Click(object sender, EventArgs e)
		{
			if (mapDrawer.Zoom < 3)
				mapDrawer.Zoom += 0.1F;

			mapDrawer.Refresh();
		}

		private void btnZoomOut_Click(object sender, EventArgs e)
		{
			if (mapDrawer.Zoom > 0.1F)
				mapDrawer.Zoom -= 0.1F;

			mapDrawer.Refresh();
		}
		#endregion
	}
}
