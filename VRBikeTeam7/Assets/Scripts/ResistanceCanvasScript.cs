using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResistanceCanvasScript : MonoBehaviour {

	public InputField minResistanceInputField;
	public InputField maxResistanceInputField;

	public int minResistance;
	public int maxResistance;

	public bool canvasValid;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (int.TryParse(minResistanceInputField.text, out minResistance)) {
			if (int.TryParse(maxResistanceInputField.text, out maxResistance)) {

				if (minResistance < 1) {
					minResistance = 1;
					setResistanceText ();
				}

				if (maxResistance > 100) {
					maxResistance = 100;
					setResistanceText ();
				} 

				if (maxResistance < 1) {
					maxResistance = 1;
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
