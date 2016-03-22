using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IntervalCanvasScript : MonoBehaviour {

	public InputField defineUnitInputField;
	public Dropdown basePeakDropdown;

	public float unitDistance;

	public bool canvasValid;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (float.TryParse (defineUnitInputField.text, out unitDistance)) {
			if (unitDistance > 0) {
				canvasValid = true;
				return;
			}
		}
		canvasValid = false;
	}
}
