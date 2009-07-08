using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TEN.Structures;

namespace TEN.Forms
{
	public partial class SemaphoreDialog : Form
	{
		#region Fields
		/// <summary>
		/// Lanes number that the user has input in the form.
		/// </summary>
		public int CycleInterval
		{
			get { return int.Parse(cycleInterval.Text); }
		}

		/// <summary>
		/// Reference to the temporization object of the semaphore.
		/// </summary>
		private List<KeyValuePair<MapEdge, int>> temporization;

		private List<KeyValuePair<MapEdge, int>> temporizationCopy;
		/// <summary>
		/// Copy of the temporization object.
		/// </summary>
		public List<KeyValuePair<MapEdge, int>> TemporizationCopy
		{
			get { return temporizationCopy; }
		}

		/// <summary>
		/// Previous selected index. Refreshed when changing indexes.
		/// </summary>
		private int previousIndex;

		/// <summary>
		/// States if the user clicked the cancel button.
		/// </summary>
		private bool cancelled;
		#endregion

		#region Constructors
		/// <summary>
		/// Constructor, initialize components and sets focus to the text control.
		/// </summary>
		public SemaphoreDialog(int cycInterval, List<KeyValuePair<MapEdge, int>> timing)
		{
			InitializeComponent();

			this.cycleInterval.Text = cycInterval.ToString();
			this.temporization = timing;
			int i = 1;
			foreach (KeyValuePair<MapEdge, int> kvp in timing)
			{
				this.listSemaphores.Items.Add("Edge " + i);
				i++;
			}
			this.temporizationCopy = new List<KeyValuePair<MapEdge, int>>(timing);
			this.listSemaphores.SelectedIndex = 0;
			this.previousIndex = 0;
			this.cycleInterval.Focus();
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

			cycleInterval.Focus();
		}

		private void listSemaphores_SelectedIndexChanged(object sender, EventArgs e)
		{
			KeyValuePair<MapEdge, int> kvp = temporizationCopy[previousIndex];
			int newTime = kvp.Value;
			if (int.TryParse(greenTime.Text, out newTime) && newTime > 0)
				temporizationCopy[previousIndex] = new KeyValuePair<MapEdge, int>(kvp.Key, newTime);

			greenTime.Text = temporizationCopy[listSemaphores.SelectedIndex].Value.ToString();
			TENApp.frmMain.Drawer.SelectedEdge = temporizationCopy[listSemaphores.SelectedIndex].Key;
			TENApp.frmMain.Drawer.Refresh();

			previousIndex = listSemaphores.SelectedIndex;
		}

		private void SemaphoreDialog_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing || cancelled)
				return;

			KeyValuePair<MapEdge, int> kvp = temporizationCopy[previousIndex];
			int newTime = kvp.Value;
			if (int.TryParse(greenTime.Text, out newTime) && newTime > 0)
				temporizationCopy[previousIndex] = new KeyValuePair<MapEdge, int>(kvp.Key, newTime);

			int value;
			if (!int.TryParse(cycleInterval.Text, out value) || value < 0)
			{
				e.Cancel = true;
				MessageBox.Show("Invalid input value.", "TEN - Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			TENApp.frmMain.Drawer.SelectedEdge = null;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			cancelled = true;
		}
		#endregion
	}
}
