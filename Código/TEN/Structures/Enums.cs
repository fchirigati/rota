namespace TEN.Structures
{
	/// <summary>
	/// Available macro application states.
	/// </summary>
	public enum AppState : short
	{
		/// <summary>
		/// Simulation is running.
		/// </summary>
		SimulationRunning,

		/// <summary>
		/// Simulation is paused.
		/// </summary>
		SimulationPaused,

		/// <summary>
		/// Simulation is stopped and the edition mode is set to Pointer.
		/// </summary>
		EditingPointer,

		/// <summary>
		/// Simulation is stopped and the edition mode is set to New Road.
		/// </summary>
		EditingNewRoad,

		/// <summary>
		/// Simulation is stopped and the edition mode is set to New Traffic Light.
		/// </summary>
		EditingNewTrafficLight,

		/// <summary>
		/// Other application state.
		/// </summary>
		Other
	}

	/// <summary>
	/// Possible node types.
	/// </summary>
	public enum NodeType : short
	{
		/// <summary>
		/// TO-DO
		/// </summary>
		FlowNode
	}
}
