using System;
using System.Windows.Forms;
using System.Threading;
using ROTA.Forms;
using TrafficAnalyzer;
using TrafficSimulator;

namespace ROTA
{
	static public class ROTA
	{
		#region Fields
		/// <summary>
		/// A reference to the traffic analyzer object.
		/// </summary>
		static public volatile Analyzer analyzer;

		/// <summary>
		/// A reference to the traffic simulator object.
		/// </summary>
		static public volatile Simulator simulator;

		/// <summary>
		/// A reference to the singletlon monitor form object.
		/// </summary>
		static public volatile FrmMain frmMain;

		#region Thread References
		/// <summary>
		/// A reference to the thread where the program's Main method runs on.
		/// </summary>
		static public volatile Thread threadMain;

		/// <summary>
		/// A reference to the thread the TrafficAnalyzer runs on.
		/// </summary>
		static public volatile Thread threadAnalyzer;

		/// <summary>
		/// A reference to the thread the TrafficSimulator runs on.
		/// </summary>
		static public volatile Thread threadSimulator;
		#endregion
		#endregion

		#region Main Method
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			ROTA.threadMain = Thread.CurrentThread;
			ROTA.threadMain.Name = "MainThread";

			#region Initialize other modules
			ROTA.analyzer = new Analyzer();
			ROTA.simulator = new Simulator();
			#endregion

			#region Start the threads
			#endregion

			Application.Run(new FrmMain());
		}
		#endregion
	}
}
