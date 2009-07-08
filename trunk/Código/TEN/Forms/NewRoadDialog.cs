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
		#endregion

		#region Constructors
		/// <summary>
		/// Constructor, initialize components and sets focus to the text control.
		/// </summary>
		public NewRoadDialog()
		{
			InitializeComponent();
			this.lanesNumber.Focus();
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
		#endregion
	}
}
