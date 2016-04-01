using UnityEngine;
using System.Collections;
using System;

public class GameTimer : MonoBehaviour {

	public SessionCompleteCanvasScript sessionCompleteCanvas;

	public TextMesh positivePaceDisplay;
	public TextMesh negativePaceDisplay;

	static float trackLength;
	static float targetPaceIncrement;
	static float targetPace = 0f;
	static double actualPace;
	public static UIOverlay uiOverlay;
	static int checkPointDistance = 100;

	void Start () {

		if (ExerciseSettings.durationIsDistance == false) {
			if ((((ExerciseSettings.time / 60) * ExerciseSettings.targetSpeed * 1000) % 10) >= 5) {
				trackLength = ((ExerciseSettings.time / 60) * ExerciseSettings.targetSpeed * 1000) + (10 - (((ExerciseSettings.time / 60) * ExerciseSettings.targetSpeed * 1000) % 10));
			} else {
				trackLength = ((ExerciseSettings.time / 60) * ExerciseSettings.targetSpeed * 1000) - (((ExerciseSettings.time / 60) * ExerciseSettings.targetSpeed * 1000) % 10);
			}

			//Disable script if track isn't long enough.
			if (trackLength <= 100) {
				GetComponent<GameTimer> ().enabled = false;
			}else{
				targetPaceIncrement = ((100/(ExerciseSettings.targetSpeed * 1000)) * 3600);
				uiOverlay = GameObject.Find ("GameBike").GetComponent<UIOverlay> ();
			}
		}
	}

	
	void OnTriggerEnter(Collider collider){
		//Delete start/finish lines and check points on collision
		if (collider.tag == "Start" || collider.tag == "Finish" || collider.tag == "CheckPoint") {

			Destroy (collider.gameObject);

			//Enables mesh renderer of 3D text if checkpoint is hit. Displays current pace (how many seconds ahead or behind of target).
			if (collider.tag == "CheckPoint") {
				targetPace += targetPaceIncrement;
				actualPace = uiOverlay.currentTime.TotalHours 
					+ uiOverlay.currentTime.TotalMinutes
					+ uiOverlay.currentTime.TotalSeconds
					+ uiOverlay.currentTime.TotalMilliseconds;
				actualPace = (actualPace / 1000);

				actualPace = targetPace - actualPace;
				if (actualPace >= 0) {
					positivePaceDisplay.text = "+" + actualPace.ToString("F2");
					positivePaceDisplay.GetComponent<Renderer> ().enabled = true;
					StartCoroutine (HideRenderer (positivePaceDisplay));
				} else if (actualPace < 0){
					negativePaceDisplay.text = "" + actualPace.ToString("F2");
					negativePaceDisplay.GetComponent<Renderer> ().enabled = true;
					StartCoroutine (HideRenderer (negativePaceDisplay));
				}

			


			}else if (collider.tag == "Finish") {
				sessionCompleteCanvas.sessionWillFinish ();
		
			}
		}
	}

	//call StartCoroutine(HideRenderer()); to hide text mesh after 3 seconds
	IEnumerator HideRenderer(TextMesh paceDisplay){
		yield return new WaitForSeconds (3.0f);
		paceDisplay.GetComponent<Renderer> ().enabled = false;
	}
}
