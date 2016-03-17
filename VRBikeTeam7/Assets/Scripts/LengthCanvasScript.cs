using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LengthCanvasScript : MonoBehaviour {

	public Toggle distanceToggle;
	public Toggle timeToggle;

	public InputField distanceInputField;
	public InputField timeInputField;
	public InputField targetSpeedInputField;

	public bool durationIsDistance;

	public float distance;
	public float time;
	public float targetSpeed;


	public bool canvasValid;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (distanceToggle.isOn) {
			durationIsDistance = true;
			distanceInputField.interactable = true;
			timeInputField.interactable = false;
			targetSpeedInputField.interactable = false;

			if (float.TryParse (distanceInputField.text, out distance)) {
				if (distance > 1) {
					canvasValid = true;
				} else {
					canvasValid = false;
				}
				return;
			}

		} else if (timeToggle.isOn) {
			durationIsDistance = false;
			distanceInputField.interactable = false;
			timeInputField.interactable = true;
			targetSpeedInputField.interactable = true;

			if (float.TryParse (timeInputField.text, out time)) {
				if (float.TryParse (targetSpeedInputField.text, out targetSpeed)) {
					if (time > 1 && targetSpeed > 1) {
						canvasValid = true;
					} else {
						canvasValid = false;
					}
					return;
				}
			}
		}

		canvasValid = false;
	}
}
