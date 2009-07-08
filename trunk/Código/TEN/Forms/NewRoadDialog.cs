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
	public partial class NewRoadDialog : Form
	{
		#region Fields
		/// <summary>
		/// Lanes number that the user has input in the form.
		/// </summary>
		public int LanesNumber
		{
			get { return int.Parse(lanesNumber.Text); }
		}

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
		public NewRoadDialog()
		{
			InitializeComponent();
			this.lanesNumber.Focus();
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

			lanesNumber.Focus();
		}

		private void NewRoadDialog_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing || cancelled)
				return;

			int value1, value2;
			if (!int.TryParse(maxSpeed.Text, out value1) || !int.TryParse(lanesNumber.Text, out value2) ||
				value1 < 0 || value1 > 200 || value2 < 1 || value2 > 5)
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
