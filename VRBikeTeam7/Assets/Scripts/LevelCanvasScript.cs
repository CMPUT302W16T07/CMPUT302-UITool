using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelCanvasScript : MonoBehaviour {

	public Text durationText;

	// Use this for initialization
	void Start () {
		if (ExerciseSettings.durationIsDistance) {
			durationText.text = ExerciseSettings.distance.ToString ();
		} else {
			durationText.text = ExerciseSettings.time.ToString ();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
