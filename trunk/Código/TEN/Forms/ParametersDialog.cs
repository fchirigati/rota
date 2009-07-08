using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TEN.Forms
{
	public partial class ParametersDialog : Form
	{
		#region Fields
		/// <summary>
		/// Flow value that the user has input in the form.
		/// </summary>
		public int Flow
		{
			get { return int.Parse(flow.Text); }
		}

		/// <summary>
		/// Safety distance that the user has input in the form.
		/// </summary>
		public int SafetyDistance
		{
			get { return int.Parse(safetyDistance.Text); }
		}

		/// <summary>
		/// Simulation step that the user has input in the form.
		/// </summary>
		public int SimulationStep
		{
			get { return int.Parse(simulationStep.Text); }
		}

		/// <summary>
		/// States if the user clicked the cancel button.
		/// </summary>
		private bool cancelled;
		#endregion

		#region Constructors
		/// <summary>
		/// Constructor, initialize components and sets focus to the text control.
		/// </summary>
		/// <param name="currentFlow">Value to be put initially in the flow text control.</param>
		/// <param name="currentSafetyDistance">Value to be put initially in the safety distance text control.</param>
		/// <param name="currentStep">Value to be put initially in the simulation step text control.</param>
		public ParametersDialog(int currentFlow, int currentSafetyDistance, int currentStep)
		{
			InitializeComponent();
			this.flow.Text = currentFlow.ToString();
			this.flow.Focus();
			this.safetyDistance.Text = currentSafetyDistance.ToString();
			this.simulationStep.Text = currentStep.ToString();
			this.cancelled = false;
		}
		#endregion

		#region Event Handlers
		/// <summary>
		/// Handles the OnActivated event.
		/// </summary>
		protected override void OnActivated(EventArgs e)
		{
			base.OnActivated(e);

			flow.Focus();
		}

		private void ParametersDialog_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing || cancelled)
				return;

			int value1, value2, value3;
			if (!int.TryParse(flow.Text, out value1) || !int.TryParse(safetyDistance.Text, out value2) ||
				!int.TryParse(simulationStep.Text, out value3) || value1 < 0 || value2 < 0 || value3 < 1)
			{
				e.Cancel = true;
				MessageBox.Show("Invalid input value.", "TEN - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			cancelled = true;
		}
		#endregion
	}
}
