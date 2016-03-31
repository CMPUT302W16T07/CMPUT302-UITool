using UnityEngine;
using System;
using System.Collections;

public class PaceTimer : MonoBehaviour {

	private GameObject gameBike;
	private UIOverlay uiOverlay;
	private BikeController bikeController;
	public TextMesh paceDisplay;
	static float trackLength;
	static float targetPaceIncrement;
	static float targetPace = 0f;
	static float actualPace;
	int checkPointCounter = 100;

	// Use this for initialization
	void Start () {

		if (ExerciseSettings.durationIsDistance == false) {
			trackLength = ((ExerciseSettings.time / 60) * ExerciseSettings.targetSpeed * 1000) - (((ExerciseSettings.time / 60) * ExerciseSettings.targetSpeed * 1000) % 10);

			//Disable script if track isn't long enough.
			if (trackLength <= 100) {
				GetComponent<PaceTimer> ().enabled = false;
			}
		}

		gameBike = GameObject.Find ("GameBike");
		uiOverlay = gameBike.GetComponent<UIOverlay> ();
		bikeController = UIOverlay.bikeController;
	
		paceDisplay = gameObject.GetComponent<TextMesh> ();
		targetPaceIncrement = ((trackLength / (ExerciseSettings.targetSpeed * 1000)) * 60 * 60)/100 ;

	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (bikeController.DistanceTravelled);
		if (bikeController.TimerStarted) {
			TimeSpan currentTime;
			currentTime = DateTime.Now - bikeController.ReferenceTime;

			if (bikeController.DistanceTravelled >= checkPointCounter && trackLength >= 100) {
				targetPace += targetPaceIncrement;
				checkPointCounter += 100;

				if (targetPace >= 0f){
					paceDisplay.text = "+" + targetPace;
				}else{
					paceDisplay.text = "" + targetPace;
				}
				paceDisplay.GetComponent<Renderer> ().enabled = true;
				StartCoroutine (HideRenderer ());
			}
		}
	}

	IEnumerator HideRenderer(){
		yield return new WaitForSeconds (3.0f);
		paceDisplay.GetComponent<Renderer> ().enabled = false;
	}

}
