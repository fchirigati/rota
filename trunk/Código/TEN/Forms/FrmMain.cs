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

		/// <summary>
		/// Gets the status bar object used in the window.
		/// </summary>
		public ToolStripStatusLabel StatusBar
		{
			get { return statusBar; }
		}

		/// <summary>
		/// Gets the MapDrawer object used in the window.
		/// </summary>
		public MapDrawer Drawer
		{
			get { return mapDrawer; }
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

		#region Public Methods
		/// <summary>
		/// Sets a new state to the application, making the necessary changes.
		/// </summary>
		/// <param name="newState">New state of the application.</param>
		public void SetState(AppState newState)
		{
			if (TENApp.state == newState)
				return;

			mapDrawer.ResetSelections();
			TENApp.state = newState;

			switch (newState)
			{
				case AppState.SimulationRunning:
					mapDrawer.ResetSelections();

					CheckButton(btnPointer);
					UncheckButton(btnNewRoad);
					UncheckButton(btnNewTrafficLight);

					newToolStripMenuItem.Enabled = false;
					playToolStripMenuItem.Enabled = false;
					pauseToolStripMenuItem.Enabled = true;
					stopToolStripMenuItem1.Enabled = true;
					restartToolStripMenuItem1.Enabled = true;
					reportToolStripMenuItem.Enabled = false;
					optionsToolStripMenuItem1.Enabled = false;

					btnNewRoad.Enabled = false;
					btnNewTrafficLight.Enabled = false;
					btnConfigure.Enabled = false;
					btnDelete.Enabled = false;
					btnStart.Enabled = false;
					btnPause.Enabled = true;
					btnStop.Enabled = true;
					btnRestart.Enabled = true;

					TENApp.simulator.Start();
					break;

				case AppState.SimulationPaused:
					if (TENApp.simulator.IsSimulating)
						TENApp.simulator.Pause();

					mapDrawer.ResetSelections();

					CheckButton(btnPointer);
					UncheckButton(btnNewRoad);
					UncheckButton(btnNewTrafficLight);

					newToolStripMenuItem.Enabled = false;
					playToolStripMenuItem.Enabled = true;
					pauseToolStripMenuItem.Enabled = false;
					stopToolStripMenuItem1.Enabled = true;
					restartToolStripMenuItem1.Enabled = true;
					reportToolStripMenuItem.Enabled = true;
					optionsToolStripMenuItem1.Enabled = true;

					btnNewRoad.Enabled = false;
					btnNewTrafficLight.Enabled = false;
					btnConfigure.Enabled = false;
					btnDelete.Enabled = false;
					btnStart.Enabled = true;
					btnPause.Enabled = false;
					btnStop.Enabled = true;
					btnRestart.Enabled = true;
					break;

				case AppState.EditingPointer:
					if (TENApp.simulator.IsSimulating)
						TENApp.simulator.Stop();
					else
						TENApp.simulator.Reset();

					mapDrawer.ResetSelections();

					CheckButton(btnPointer);
					UncheckButton(btnNewRoad);
					UncheckButton(btnNewTrafficLight);

					newToolStripMenuItem.Enabled = true;
					playToolStripMenuItem.Enabled = true;
					pauseToolStripMenuItem.Enabled = false;
					stopToolStripMenuItem1.Enabled = false;
					restartToolStripMenuItem1.Enabled = false;
					reportToolStripMenuItem.Enabled = false;
					optionsToolStripMenuItem1.Enabled = true;

					btnPointer.Enabled = true;
					btnNewRoad.Enabled = true;
					btnNewTrafficLight.Enabled = true;
					btnConfigure.Enabled = true;
					btnDelete.Enabled = true;
					btnStart.Enabled = true;
					btnPause.Enabled = false;
					btnStop.Enabled = false;
					btnRestart.Enabled = false;

					mapDrawer.Refresh();
					break;

				case AppState.EditingNewRoad:
					mapDrawer.ResetSelections();

					UncheckButton(btnPointer);
					CheckButton(btnNewRoad);
					UncheckButton(btnNewTrafficLight);
					mapDrawer.Refresh();
					break;

				case AppState.EditingNewTrafficLight:
					mapDrawer.ResetSelections();

					mapDrawer.ResetSelections();
					UncheckButton(btnPointer);
					UncheckButton(btnNewRoad);
					CheckButton(btnNewTrafficLight);
					mapDrawer.Refresh();
					break;

				case AppState.Other:
					break;
			}
		}
		#endregion

		#region Private Methods
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

		private void btnDelete_Click(object sender, EventArgs e)
		{
			TENApp.simulator.DeleteEdge();
			TENApp.simulator.DeleteSemaphore();

			mapDrawer.Refresh();
		}

		private void btnConfigure_Click(object sender, EventArgs e)
		{
			mapDrawer.Configure();
		}

		private void newToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TENApp.simulator.Clear();
			mapDrawer.Refresh();
			SetState(AppState.EditingPointer);
		}

		private void optionsToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			mapDrawer.SetParameters();
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void reportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			mapDrawer.ShowReport();
		}

		private void mapDrawer_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
				btnDelete_Click(sender, e);
		}
		#endregion
	}
}
