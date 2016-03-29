using UnityEngine;
using System.Collections;

public class TrackGenerator : MonoBehaviour {

	public GameObject roadSection;
	public GameObject finishLine;

	static float trackLength;
	static float unitDistance;

	const int trackPlaneLength = 10;

	static float baseSectionLength;
	static float peakSectionLength;

	static float minAngle;
	static float maxAngle;

	int finishPoint;

	// Use this for initialization
	void Start () {

		// set trackLength
		if (ExerciseSettings.durationIsDistance == true) {
			trackLength = ExerciseSettings.distance * 1000;
			Debug.Log ("The track length is " + trackLength);
		} else {
			trackLength = (ExerciseSettings.time / 60) * ExerciseSettings.targetSpeed * 1000;
			Debug.Log ("The track length is " + trackLength);
		}

		// set min/max geometry
		minAngle = (ExerciseSettings.minResistance - 1)/4;
		maxAngle = ExerciseSettings.maxResistance/4;

		if (ExerciseSettings.trainingIsInterval == true) {
			handleIntervalTrack ();
		} else {
			handleProgressiveTrack ();
		}
		Instantiate (finishLine, new Vector3 (0, current_y_position - (previous_y_translation/2), (current_z_position - (previous_z_translation/2) + 5)), transform.rotation);
		Instantiate (roadSection, new Vector3 (0, current_y_position - (previous_y_translation/2), (current_z_position - (previous_z_translation/2) + 15)), transform.rotation);	
		Instantiate (roadSection, new Vector3 (0, current_y_position - (previous_y_translation/2), (current_z_position - (previous_z_translation/2) + 25)), transform.rotation);
	}
		
	void handleIntervalTrack () {

		// determine ratio
		int baseRatioTerm;
		int peakRatioTerm;
		unitDistance = ExerciseSettings.unitDistance;
		switch (ExerciseSettings.basePeakDropdownValue) {
		case 0:
			baseRatioTerm = 3;
			peakRatioTerm = 1;
			break;
		case 1:
			baseRatioTerm = 2;
			peakRatioTerm = 1;
			break;
		case 2:
			baseRatioTerm = 1;
			peakRatioTerm = 1;
			break;
		case 3:
			baseRatioTerm = 1;
			peakRatioTerm = 2;
			break;
		case 4:
			baseRatioTerm = 1;
			peakRatioTerm = 3;
			break;
		default:	
			baseRatioTerm = 0;
			peakRatioTerm = 0;
			break;
		}

		baseSectionLength = unitDistance * baseRatioTerm * 1000;
		peakSectionLength = unitDistance * peakRatioTerm * 1000;

		// create track 
		int trackIndex = 0;
		while (trackIndex < trackLength) {

			// build base section
			for (int b = 0; b < baseSectionLength; b += trackPlaneLength) {
				if (trackIndex == trackLength) {
					finishPoint = 0;
					break;
				}

				addPlaneWithAngle (minAngle);

				trackIndex += trackPlaneLength;
			}

			// build peak section
			for (int p = 0; p < peakSectionLength; p += trackPlaneLength) {
				if (trackIndex == trackLength) {
					finishPoint = 1;
					break;
				}
				addPlaneWithAngle (maxAngle);

				trackIndex += trackPlaneLength;
			}
		}
	}

	void handleProgressiveTrack () {

		float climbingLength = (ExerciseSettings.climbingPercent * trackLength) / 100;
		float peakLength = trackLength - climbingLength;

		int numberOfClimbingSections = (int)climbingLength / 10;
		int numberOfPeakSections = (int)peakLength / 10;

		float angleIncrement = (maxAngle - minAngle) / numberOfClimbingSections;
		float currentAngle = minAngle;

		// build climbing sections
		for (int section = 0; section < numberOfClimbingSections; section++) {
			currentAngle += angleIncrement;
			addPlaneWithAngle (currentAngle);
		}

		// build peak sections
		for (int section = 0; section < numberOfPeakSections; section++) {
			addPlaneWithAngle (maxAngle);
		}
	}

	float previousAngle;
	float previous_y_translation;
	float previous_z_translation;

	float current_y_position;
	float current_z_position;

	void addPlaneWithAngle (float angle) {

		float y_translation;
		float z_translation;

		if (angle == 0) {
			y_translation = 0;
			z_translation = 10;
		} else {
			float radians = (angle * Mathf.Deg2Rad);
			y_translation = (Mathf.Sin (radians) * 10);
			z_translation = (y_translation) / (Mathf.Tan (radians));
		}

		// connect planes of difffering angles
		if (angle != previousAngle) {
			current_y_position = (current_y_position - (previous_y_translation / 2)) + (y_translation / 2);
			current_z_position = (current_z_position - (previous_z_translation / 2)) + (z_translation / 2);
		}
			
		Instantiate (roadSection, new Vector3 (0, current_y_position, current_z_position), Quaternion.Euler (360 - angle, 0, 0));

		current_y_position += y_translation;
		current_z_position += z_translation;

		previous_y_translation = y_translation;
		previous_z_translation = z_translation;
		previousAngle = angle;
	}
}
