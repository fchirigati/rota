namespace TEN.Structures
{
	/// <summary>
	/// Available modes of edition.
	/// </summary>
	public enum EditorMode : short
	{
		/// <summary>
		/// Mode that lets the user select any map component.
		/// </summary>
		Pointer = 0,

		/// <summary>
		/// Mode that adds new roads to the map.
		/// </summary>
		NewRoad = 1,

		/// <summary>
		/// Mode that lets the user add new traffic lights to the map.
		/// </summary>
		NewTrafficLight = 2
	}

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
}
