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
	public partial class NumberDialog : Form
	{
		#region Fields
		/// <summary>
		/// Integer that the user has input in the form.
		/// </summary>
		public int Response
		{
			get { return int.Parse(txtResponse.Text); }
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Constructor, initialize components and sets focus to the text control.
		/// </summary>
		public NumberDialog()
		{
			InitializeComponent();
			this.txtResponse.Focus();
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Shows the form as a modal dialog.
		/// </summary>
		/// <param name="title">The title of the dialog.</param>
		/// <param name="description">Text description above the text control.</param>
		/// <param name="defaultVal">Default value of the text control.</param>
		public DialogResult ShowDialog(string title, string description, string defaultVal)
		{
			Text = title;
			lblDescription.Text = description;
			txtResponse.Text = defaultVal;
			txtResponse.Focus();

			return ShowDialog();
		}
		#endregion

		#region Event Handlers
		/// <summary>
		/// Handles the OnActivated event.
		/// </summary>
		protected override void OnActivated(EventArgs e)
		{
			base.OnActivated(e);

			txtResponse.Focus();
		}
		#endregion
	}
}
