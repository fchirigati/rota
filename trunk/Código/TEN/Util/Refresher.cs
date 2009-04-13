using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TEN.Util
{
	public class Refresher
	{
		#region Fields
		delegate void RefreshMethod();

		/// <summary>
		/// States if this refresher should be running.
		/// </summary>
		private bool running;

		private int delay;
		/// <summary>
		/// Time between refreshes.
		/// </summary>
		public int Delay
		{
			get { return delay; }
			set { delay = value; }
		}
		#endregion

		#region Constructors
		/// <summary>
		/// Creates a new Refresher object.
		/// </summary>
		/// <param name="usedDelay">Delay time between refreshes.</param>
		public Refresher(int usedDelay)
		{
			this.delay = usedDelay;
			this.running = true;
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Runs the Refresher method.
		/// </summary>
		public void Run()
		{
			while (running)
			{
				if (!TENApp.running || !TENApp.frmMain.Running)
					continue;

				if (TENApp.frmMain.IsHandleCreated)
				{
					RefreshMethod d = TENApp.frmMain.Refresh;
					TENApp.frmMain.BeginInvoke(d);
				}

				Thread.Sleep(delay);
			}
		}

		public void Stop()
		{
			running = false;
		}
		#endregion
	}
}
