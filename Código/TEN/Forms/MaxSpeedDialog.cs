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
		#endregion
	}
}
