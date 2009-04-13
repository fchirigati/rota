using System;
using System.Collections.Generic;
using System.Windows.Forms;
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

		private EditorMode editionMode;
		/// <summary>
		/// Gets the current edition mode.
		/// </summary>
		public EditorMode EditionMode
		{
			get { return editionMode; }
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Initializes the form.
		/// </summary>
		public FrmMain()
		{
			this.running = false;
			this.editionMode = EditorMode.Pointer;

			InitializeComponent();

			this.btnPause.Enabled = false;
			this.btnStop.Enabled = false;
			this.btnRestart.Enabled = false;

			this.running = true;
		}
		#endregion

		#region Private Methods
		private void SetState()
		{
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
		private void MainForm_FormClosing(object sender, EventArgs e)
		{
			TENApp.Shutdown();
		}

		private void btnPointer_Click(object sender, EventArgs e)
		{
			editionMode = EditorMode.Pointer;
			mapDrawer1.ClearClickedPoints();
			CheckButton(btnPointer);
			UncheckButton(btnNewRoad);
			UncheckButton(btnNewTrafficLight);
			mapDrawer1.Refresh();
		}

		private void btnNewRoad_Click(object sender, EventArgs e)
		{
			editionMode = EditorMode.NewRoad;
			mapDrawer1.ClearClickedPoints();
			UncheckButton(btnPointer);
			CheckButton(btnNewRoad);
			UncheckButton(btnNewTrafficLight);
			mapDrawer1.Refresh();
		}

		private void btnNewTrafficLight_Click(object sender, EventArgs e)
		{
			editionMode = EditorMode.NewTrafficLight;
			mapDrawer1.ClearClickedPoints();
			UncheckButton(btnPointer);
			UncheckButton(btnNewRoad);
			CheckButton(btnNewTrafficLight);
			mapDrawer1.Refresh();
		}

		private void btnStart_Click(object sender, EventArgs e)
		{
			TENApp.threadSimulator.Start();
			btnStart.Enabled = false;
			btnPause.Enabled = true;
			btnStop.Enabled = true;
			btnRestart.Enabled = true;

			btnNewRoad.Enabled = false;
			btnNewTrafficLight.Enabled = false;

			editionMode = EditorMode.Pointer;
			mapDrawer1.ClearClickedPoints();
			CheckButton(btnPointer);
			UncheckButton(btnNewRoad);
			UncheckButton(btnNewTrafficLight);
			mapDrawer1.Refresh();
		}
		#endregion
	}
}
