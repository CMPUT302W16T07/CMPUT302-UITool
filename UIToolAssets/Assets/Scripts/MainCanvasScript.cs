using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainCanvasScript : MonoBehaviour {
	
	// The main house of going ons
	// calls the loadLevel function and sets the ExerciseSettings

	// variables for determining setting validity
	public LengthCanvasScript lengthCanvas;
	public ResistanceCanvasScript resistanceCanvas;

	public IntervalCanvasScript intervalCanvas;
	public ProgressiveCanvasScript progressiveCanvas;

	private bool settingsValid;
	private bool trainingCanvasValid;

	public Button startButton;

	//ExerciseSettingsScript exerciseSettings;

	// varables for interval/progresive toggling
	public Toggle intervalToggle;
	public Toggle progressiveToggle;

	public Dropdown basePeakDropdown;
	public InputField defineUnitInputField;

	public InputField climbingInputField;
	public InputField peakInputField;

	public void loadLevel () {
		SceneManager.LoadScene ("Level");
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		// handle UI to show either interval OR progressive training option and determine validity
		if (intervalToggle.isOn) {
			// turn interval on
			basePeakDropdown.interactable = true;
			defineUnitInputField.interactable = true;
			climbingInputField.interactable = false;

			trainingCanvasValid = intervalCanvas.canvasValid;
			ExerciseSettings.trainingIsInterval = true;

		
		} else if (progressiveToggle.isOn) {
			// turn progressive on
			basePeakDropdown.interactable = false;
			defineUnitInputField.interactable = false;
			climbingInputField.interactable = true;

			trainingCanvasValid = progressiveCanvas.canvasValid;
			ExerciseSettings.trainingIsInterval = false;

		}

		// determine if settings are valid
		if (lengthCanvas.canvasValid &&
		    resistanceCanvas.canvasValid &&
			trainingCanvasValid) {
			settingsValid = true;
		} else {
			settingsValid = false;
		}

		if (settingsValid) {
			startButton.interactable = true;
			setExerciseSettings ();

		} else {
			startButton.interactable = false;
		}
	}



	void setExerciseSettings () {

		// handle duration settings
		ExerciseSettings.durationIsDistance = lengthCanvas.durationIsDistance;
		if (ExerciseSettings.durationIsDistance) {
			ExerciseSettings.distance = lengthCanvas.distance;
		} else {
			ExerciseSettings.time = lengthCanvas.time;
			ExerciseSettings.targetSpeed = lengthCanvas.targetSpeed;
		}

		// handle resistance settings
		ExerciseSettings.maxResistance = resistanceCanvas.maxResistance;
		ExerciseSettings.minResistance = resistanceCanvas.minResistance;

		// handle training settings
		// NOTE: ExerciseSettingsScript.trainingIsInterval is set in the update loop
		if (ExerciseSettings.trainingIsInterval) {
			ExerciseSettings.basePeakDropdownValue = intervalCanvas.basePeakDropdown.value;
			ExerciseSettings.unitDistance = intervalCanvas.unitDistance;
		} else {
			ExerciseSettings.climbingPercent = progressiveCanvas.climbingPercent;
			ExerciseSettings.peakPercent = progressiveCanvas.peakPercent;
		}
	}
}
