using UnityEngine;
using System.Collections;


//There are no comments. It was hard to write, it should be hard to read. 
public class TrackGenerator : MonoBehaviour {

	public GameObject roadSection;
	static float trackLength;
	static int valleyPeak;
	static float unit;
	static float valley;
	static float peak;

	static float valleyAngle;
	static float valleyRad;

	static float peakAngle;
	static float peakRad;

	static float valley_z_translation;
	static float valley_y_translation;

	static float peak_z_translation;
	static float peak_y_translation;

	static float previous_valley_z = 0;
	static float previous_valley_y = 0;

	static float previous_peak_z = 0;
	static float previous_peak_y = 0;

	int i = 0;

	// Use this for initialization
	void Start () {

		if (ExerciseSettings.durationIsDistance == true) {
			trackLength = ExerciseSettings.distance * 100;
			Debug.Log ("The track length is " + trackLength);
		}else{
			trackLength = (ExerciseSettings.time / 60) * ExerciseSettings.targetSpeed * 100;
			Debug.Log ("The track length is " + trackLength);
		}

		valleyAngle = (ExerciseSettings.minResistance - 1)/4;
		valleyRad = (valleyAngle * Mathf.Deg2Rad);
		peakAngle = ExerciseSettings.maxResistance/4;
		peakRad = (peakAngle * Mathf.Deg2Rad);

		Debug.Log ("The minimum angle is " + valleyAngle);
		Debug.Log ("The maximum angle is " + peakAngle);

		if (ExerciseSettings.trainingIsInterval == true) {
			unit = ExerciseSettings.unitDistance;
			valleyPeak = ExerciseSettings.basePeakDropdownValue; 
			if (valleyPeak == 0) {
				valley = 3 * unit * 100;
				peak = 1 * unit * 100;
				Debug.Log ("The ratio is " + valley + ":" + peak);
			} else if (valleyPeak == 1) {
				valley = 2 * unit * 100;
				peak = 1 * unit * 100;
				Debug.Log ("The ratio is " + valley + ":" + peak);
			} else if (valleyPeak == 2) {
				valley = 1 * unit * 100;
				peak = 1 * unit * 100;
				Debug.Log ("The ratio is " + valley + ":" + peak);
			} else if (valleyPeak == 3) {
				valley = 1 * unit * 100;
				peak = 2 * unit * 100;
				Debug.Log ("The ratio is " + valley + ":" + peak);
			} else if (valleyPeak == 4) {
				valley = 1 * unit * 100;
				peak = 3 * unit * 100;
				Debug.Log ("The ratio is " + valley + ":" + peak);
			}
		}

		if (valleyAngle == 0) {
			valley_y_translation = 0;
			valley_z_translation = 10;
		} else {
			valley_y_translation = (Mathf.Sin (valleyRad) * 10);
			valley_z_translation = (valley_y_translation) / (Mathf.Tan (valleyRad));
		}

		previous_valley_y = valley_y_translation / 2;
		previous_valley_z = valley_z_translation / 2;
	
		if (peakAngle == 0) {
			peak_y_translation = 0;
			peak_z_translation = 10;
		} else {
			peak_y_translation = (Mathf.Sin (peakRad) * 10);
			peak_z_translation = (peak_y_translation) / (Mathf.Tan (peakRad));
		}

		previous_peak_y = peak_y_translation / 2;
		previous_peak_z = peak_z_translation / 2;

		/*
		for (int i = 10; i < trackLength; i += 10) {
			Instantiate (roadSection, new Vector3 (transform.position.x, transform.position.y, transform.position.z + i), transform.rotation);
		}
		*/

		while (i < trackLength) {
			for (int v = 0; v < valley; v++) {
				if (i == trackLength) {
					break;
				}
					Instantiate (roadSection, new Vector3 (0, previous_valley_y, previous_valley_z), Quaternion.Euler (360 - valleyAngle, 0, 0));
			
					previous_valley_y += valley_y_translation;
					previous_valley_z += valley_z_translation;

					i++;

			}

			previous_peak_y = (previous_valley_y - (valley_y_translation / 2)) + (peak_y_translation / 2);
			previous_peak_z = (previous_valley_z - (valley_z_translation / 2)) + (peak_z_translation / 2);
			
			for (int p = 0; p < peak; p++) {
				if (i == trackLength) {
					break;
				}
					Instantiate (roadSection, new Vector3 (0, previous_peak_y, previous_peak_z), Quaternion.Euler (360 - peakAngle, 0, 0));

					previous_peak_y += peak_y_translation;
					previous_peak_z += peak_z_translation;

					i++;

			}

			previous_valley_y = (previous_peak_y - (peak_y_translation / 2)) + (valley_y_translation / 2);
			previous_valley_z = (previous_peak_z - (peak_z_translation / 2)) + (valley_z_translation / 2);
		}
	}
}

