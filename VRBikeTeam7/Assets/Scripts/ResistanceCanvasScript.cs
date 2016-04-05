using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResistanceCanvasScript : MonoBehaviour {

	public InputField minResistanceInputField;
	public InputField maxResistanceInputField;

	public float minResistance;
	public float maxResistance;

	public bool canvasValid;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (float.TryParse(minResistanceInputField.text, out minResistance)) {
			if (float.TryParse(maxResistanceInputField.text, out maxResistance)) {

				if (minResistance < 0) {
					minResistance = 0;
					setResistanceText ();
				}

				if (maxResistance > 25) {
					maxResistance = 25;
					setResistanceText ();
				} 

				if (maxResistance < 0) {
					maxResistance = 0;
					setResistanceText ();
				}

				if (minResistance > maxResistance) {
					minResistance = maxResistance - 1;
					setResistanceText ();
				}

				canvasValid = true;
				return;
			}
		}

		canvasValid = false;
	}

	void setResistanceText () {
		minResistanceInputField.text = minResistance.ToString ();
		maxResistanceInputField.text = maxResistance.ToString ();
	}
}
