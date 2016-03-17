using UnityEngine;
using System.Collections;

public class TrackGenerator : MonoBehaviour {

	public GameObject roadSection;
	static float trackLength;
	static int valleyPeak;
	static float unit;
	static float valley;
	static float peak;
	static float valleyAngle;
	static float peakAngle;
	// Use this for initialization
	void Start () {

		if (ExerciseSettings.durationIsDistance == true) {
			trackLength = ExerciseSettings.distance * 1000;
			Debug.Log ("The track length is " + trackLength);
		}else{
			trackLength = (ExerciseSettings.time / 60) * ExerciseSettings.targetSpeed;
			Debug.Log ("The track length is " + trackLength);
		}

		valleyAngle = (ExerciseSettings.minResistance - 1) /4;
		peakAngle = ExerciseSettings.maxResistance/4;

		Debug.Log ("The minimum angle is " + valleyAngle);
		Debug.Log ("The maximum angle is " + peakAngle);

		if (ExerciseSettings.trainingIsInterval == true) {
			unit = ExerciseSettings.unitDistance;
			valleyPeak = ExerciseSettings.basePeakDropdownValue; 
			if (valleyPeak == 0) {
				valley = 3 * unit * 1000;
				peak = 1 * unit * 1000;
				Debug.Log ("The ratio is " + valley + ":" + peak);
			} else if (valleyPeak == 1) {
				valley = 2 * unit * 1000;
				peak = 1 * unit * 1000;
				Debug.Log ("The ratio is " + valley + ":" + peak);
			} else if (valleyPeak == 2) {
				valley = 1 * unit * 1000;
				peak = 1 * unit * 1000;
				Debug.Log ("The ratio is " + valley + ":" + peak);
			} else if (valleyPeak == 3) {
				valley = 1 * unit * 1000;
				peak = 2 * unit * 1000;
				Debug.Log ("The ratio is " + valley + ":" + peak);
			} else if (valleyPeak == 4) {
				valley = 1 * unit * 1000;
				peak = 3 * unit * 1000;
				Debug.Log ("The ratio is " + valley + ":" + peak);
			}
		}

		for (int i = 10; i < trackLength; i += 10) {
			Instantiate (roadSection, new Vector3 (transform.position.x, transform.position.y, transform.position.z + i), transform.rotation);
		}
	}
}

