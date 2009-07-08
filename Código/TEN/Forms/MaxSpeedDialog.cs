using System;
using System.Windows.Forms;

namespace TEN.Forms
{
	/// <summary>
	/// Dialog that sets the maximum speed.
	/// </summary>
	public partial class MaxSpeedDialog : Form
	{
		#region Fields
		/// <summary>
		/// Maximum speed that the user has input in the form.
		/// </summary>
		public int MaxSpeed
		{
			get { return int.Parse(maxSpeed.Text); }
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
		/// <param name="currentSpeed">Value to be put in the text control by default.</param>
		public MaxSpeedDialog(float currentSpeed)
		{
			InitializeComponent();
			this.maxSpeed.Text = currentSpeed.ToString();
			this.maxSpeed.Focus();
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

			maxSpeed.Focus();
		}

		private void MaxSpeedDialog_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing || cancelled)
				return;
			
			int value;
			if (!int.TryParse(maxSpeed.Text, out value) || value < 0 || value > 200)
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
