﻿using System;
using System.Threading;
using System.Windows.Forms;
using TEN.Forms;
using TEN.ThreadManagers;
using TEN.Structures;
using TEN;

namespace TEN
{
	/// <summary>
	/// TO-DO
	/// </summary>
	static public class TENApp
	{
		#region Fields
		/// <summary>
		/// A reference to the traffic simulator object.
		/// </summary>
		static public volatile Simulator simulator;

		/// <summary>
		/// A reference to the Refresher object.
		/// </summary>
		static public volatile Refresher refresher;

		/// <summary>
		/// A reference to the singletlon monitor form object.
		/// </summary>
		static public volatile FrmMain frmMain;

		/// <summary>
		/// Indicates if the shutdown process has been started.
		/// This is to avoid it from being run twice.
		/// </summary>
		static public volatile bool shutdownStarted;

		/// <summary>
		/// States if the program is running.
		/// </summary>
		static public volatile bool running;

		/// <summary>
		/// Gets the state of the application.
		/// </summary>
		static public volatile AppState state;

		#region Thread References
		/// <summary>
		/// A reference to the thread where the program's Main method runs on.
		/// </summary>
		static public volatile Thread threadMain;

		/// <summary>
		/// A reference to the thread the TrafficSimulator runs on.
		/// </summary>
		static public volatile Thread threadSimulator;

		/// <summary>
		/// A reference to the thread the Refresher runs on.
		/// </summary>
		static public volatile Thread threadRefresher;
		#endregion
		#endregion

		#region Main Method
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			TENApp.frmMain = new FrmMain();
			TENApp.frmMain.Show();
			TENApp.running = true;
			TENApp.shutdownStarted = false;

			TENApp.threadMain = Thread.CurrentThread;
			TENApp.threadMain.Name = "MainThread";

			#region Initialize other modules
			TENApp.simulator = new Simulator();
			TENApp.refresher = new Refresher(40);
			#endregion

			#region Start the threads
			TENApp.threadSimulator = new Thread(new ThreadStart(TENApp.simulator.ThreadRun));
			TENApp.threadSimulator.Name = "Simulator";
			TENApp.threadSimulator.Start();

			TENApp.threadRefresher = new Thread(new ThreadStart(TENApp.refresher.Run));
			TENApp.threadRefresher.Name = "Refresher";
			TENApp.threadRefresher.Start();
			#endregion

			TENApp.frmMain.SetState(AppState.EditingPointer);
			Application.Run(frmMain);
		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Ends the program gracefully.
		/// </summary>
		public static void Shutdown()
		{
			if (TENApp.shutdownStarted)
				return;

			TENApp.shutdownStarted = true;
			TENApp.running = false;

			// Stop the threads.
			TENApp.refresher.Stop();
			TENApp.simulator.ThreadStop();
			Application.Exit();
		}
		#endregion
	}
}
