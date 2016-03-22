using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProgressiveCanvasScript : MonoBehaviour {

	public InputField climbingInputField;
	public InputField peakInputField;

	public int climbingPercent;
	public int peakPercent;

	public bool canvasValid;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (int.TryParse(climbingInputField.text, out climbingPercent)) {
			if (int.TryParse(peakInputField.text, out peakPercent)) {

				if (climbingPercent > 100) {
					climbingPercent = 100;
				}

				peakPercent = 100 - climbingPercent;

				setClimbingPeakText ();
				canvasValid = true;
				return;
			}
		}

		canvasValid = false;
	}

	void setClimbingPeakText () {
		climbingInputField.text = climbingPercent.ToString ();
		peakInputField.text = peakPercent.ToString ();
	}
}
