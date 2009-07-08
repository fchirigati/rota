using System;
using System.Threading;
using System.Windows.Forms;

namespace TEN.Forms
{
	/// <summary>
	/// Dialog that sets the maximum speed.
	/// </summary>
	public partial class ReportDialog : Form
	{
		#region Fields
		/// <summary>
		/// Text box of the dialog.
		/// </summary>
		public TextBox TextBox
		{
			get { return textBox; }
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Constructor, initialize components and sets focus to the text control.
		/// </summary>
		/// <param name="text">Report text.</param>
		public ReportDialog(string text)
		{
			InitializeComponent();
			this.textBox.Text = text;
			this.textBox.Select(text.Length, 0);
		}
		#endregion

		#region Event Handlers
		private void btnSave_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "Text File|*.txt";
			saveFileDialog.Title = "Save Report File";
			Thread.CurrentThread.SetApartmentState(ApartmentState.STA);
			saveFileDialog.ShowDialog();

			if (saveFileDialog.FileName != "")
			{
				System.IO.StreamWriter sw = new System.IO.StreamWriter(saveFileDialog.OpenFile());
				sw.Write(textBox.Text);
				sw.Close();
			}
		}
		#endregion
	}
}
