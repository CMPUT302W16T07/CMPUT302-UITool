using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour {

	public TextMesh positivePaceDisplay;
	public TextMesh negativePaceDisplay;


	static float trackLength;
	static double targetPaceIncrement;
	static double targetPace = 0;
	static double actualPace;
	public static UIOverlay uiOverlay;
	static int checkPointDistance = 250;
	public TextMesh sessionSuccessText;
	private TimeSpan pace;

	private string finishMessage = "Track complete!\nReturning to main menu\nin 5 seconds.";
	private string timeUpMessage = "Time is up!\nReturning to main menu\nin 5 seconds.";

	void Start () {

		if (ExerciseSettings.durationIsDistance == false) {
			if ((((ExerciseSettings.time / 60) * ExerciseSettings.targetSpeed * 1000) % 10) >= 5) {
				trackLength = ((ExerciseSettings.time / 60) * ExerciseSettings.targetSpeed * 1000) + (10 - (((ExerciseSettings.time / 60) * ExerciseSettings.targetSpeed * 1000) % 10));
			} else {
				trackLength = ((ExerciseSettings.time / 60) * ExerciseSettings.targetSpeed * 1000) - (((ExerciseSettings.time / 60) * ExerciseSettings.targetSpeed * 1000) % 10);
			}
				
			targetPaceIncrement = ((checkPointDistance/(ExerciseSettings.targetSpeed * 1000)) * 3600);
			uiOverlay = GameObject.Find ("GameBike").GetComponent<UIOverlay> ();

		}
	}

	void Update(){
		actualPace = uiOverlay.currentTime.TotalHours 
			+ uiOverlay.currentTime.TotalMinutes
			+ uiOverlay.currentTime.TotalSeconds
			+ uiOverlay.currentTime.TotalMilliseconds;
		
		actualPace = (actualPace / 1000);

		if(actualPace >= (ExerciseSettings.time * 60)){
			sessionSuccessText.GetComponent<Renderer> ().enabled = true;
			sessionSuccessText.text = timeUpMessage;
			StartCoroutine(RestartTrack(sessionSuccessText));
		}
	}
	
	void OnTriggerEnter (Collider collider) {
		//Delete start/finish lines and check points on collision
		if (collider.tag == "Start" || collider.tag == "Finish" || collider.tag == "CheckPoint") {

			Destroy (collider.gameObject);

			//Enables mesh renderer of 3D text if checkpoint is hit. Displays current pace (how many seconds ahead or behind of target).
			if (collider.tag == "CheckPoint") {
				targetPace += targetPaceIncrement;
				actualPace = targetPace - actualPace;

				if (actualPace >= 0) {
					pace = TimeSpan.FromSeconds (actualPace);
					positivePaceDisplay.text = String.Format ("+{3:#0}:{2:00}:{1:00}.{0:00}",
						Mathf.Floor ((float)pace.Milliseconds / 10),
						Mathf.Floor ((float)pace.Seconds),
						Mathf.Floor ((float)pace.Minutes),
						Mathf.Floor ((float)pace.TotalHours));

					positivePaceDisplay.GetComponent<Renderer> ().enabled = true;
					StartCoroutine (HideRenderer (positivePaceDisplay));

				} else if (actualPace < 0) {
					pace = TimeSpan.FromSeconds (Math.Abs (actualPace));
					negativePaceDisplay.text = positivePaceDisplay.text = String.Format ("-{3:#0}:{2:00}:{1:00}.{0:00}",
						Mathf.Floor ((float)pace.Milliseconds / 10),
						Mathf.Floor ((float)pace.Seconds),
						Mathf.Floor ((float)pace.Minutes),
						Mathf.Floor ((float)pace.TotalHours));

					negativePaceDisplay.GetComponent<Renderer> ().enabled = true;
					StartCoroutine (HideRenderer (negativePaceDisplay));
				}

			} else if (collider.tag == "Finish" && ExerciseSettings.durationIsDistance == false) {
				
				actualPace = (ExerciseSettings.time * 60) - actualPace;

				if (actualPace >= 0) {
					pace = TimeSpan.FromSeconds (actualPace);
					positivePaceDisplay.text = String.Format ("+{3:#0}:{2:00}:{1:00}.{0:00}",
						Mathf.Floor ((float)pace.Milliseconds / 10),
						Mathf.Floor ((float)pace.Seconds),
						Mathf.Floor ((float)pace.Minutes),
						Mathf.Floor ((float)pace.TotalHours));

					positivePaceDisplay.GetComponent<Renderer> ().enabled = true;
					sessionSuccessText.text = finishMessage;
					StartCoroutine (RestartTrack (sessionSuccessText));

				} else if (actualPace < 0) {
					pace = TimeSpan.FromSeconds (Math.Abs (actualPace));
					negativePaceDisplay.text = positivePaceDisplay.text = String.Format ("-{3:#0}:{2:00}:{1:00}.{0:00}",
						Mathf.Floor ((float)pace.Milliseconds / 10),
						Mathf.Floor ((float)pace.Seconds),
						Mathf.Floor ((float)pace.Minutes),
						Mathf.Floor ((float)pace.TotalHours));

					negativePaceDisplay.GetComponent<Renderer> ().enabled = true;
					sessionSuccessText.text = finishMessage;
					StartCoroutine (RestartTrack (sessionSuccessText));
				}
			} else if (collider.tag == "Finish" && ExerciseSettings.durationIsDistance == true) {
				sessionSuccessText.GetComponent<Renderer> ().enabled = true;
				StartCoroutine (RestartTrack (sessionSuccessText));
			}
		}
	}

	//call StartCoroutine(HideRenderer()); to hide text mesh after 3 seconds
	IEnumerator HideRenderer(TextMesh paceDisplay){
		yield return new WaitForSeconds (5.0f);
		paceDisplay.GetComponent<Renderer> ().enabled = false;
	}

	IEnumerator RestartTrack(TextMesh textMesh){
		textMesh.GetComponent<Renderer> ().enabled = true;
		yield return new WaitForSeconds (5.0f);
		textMesh.GetComponent<Renderer> ().enabled = false;
		SceneManager.LoadScene ("UITool");
	}


}
