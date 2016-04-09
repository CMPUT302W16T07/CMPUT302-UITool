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
	public GameObject sessionSuccessText;

	private TimeSpan pace;

	void Start () {

		if (ExerciseSettings.durationIsDistance == false) {
			if ((((ExerciseSettings.time / 60) * ExerciseSettings.targetSpeed * 1000) % 10) >= 5) {
				trackLength = ((ExerciseSettings.time / 60) * ExerciseSettings.targetSpeed * 1000) + (10 - (((ExerciseSettings.time / 60) * ExerciseSettings.targetSpeed * 1000) % 10));
			} else {
				trackLength = ((ExerciseSettings.time / 60) * ExerciseSettings.targetSpeed * 1000) - (((ExerciseSettings.time / 60) * ExerciseSettings.targetSpeed * 1000) % 10);
			}

			sessionSuccessText = GameObject.FindGameObjectWithTag ("EndMessage");
			targetPaceIncrement = ((checkPointDistance/(ExerciseSettings.targetSpeed * 1000)) * 3600);
			uiOverlay = GameObject.Find ("GameBike").GetComponent<UIOverlay> ();

		}
	}

	void Update(){
		actualPace = uiOverlay.currentTime.TotalHours 
			+ uiOverlay.currentTime.TotalMinutes
			+ uiOverlay.currentTime.TotalSeconds
			+ uiOverlay.currentTime.TotalMilliseconds;
		
		pace = TimeSpan.FromSeconds (actualPace);

		if(actualPace >= (ExerciseSettings.time * 60 * 1000)){
			sessionSuccessText.GetComponent<Renderer> ().enabled = true;
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

				actualPace = (actualPace / 1000);
				actualPace = targetPace - actualPace;

				if (actualPace >= 0) {
					positivePaceDisplay.text = String.Format ("+{3:#0}:{2:00}:{1:00}.{0:00}",
						Mathf.Floor ((float)pace.Milliseconds / 10),
						Mathf.Floor ((float)pace.Seconds),
						Mathf.Floor ((float)pace.Minutes),
						Mathf.Floor ((float)pace.TotalHours));

					positivePaceDisplay.GetComponent<Renderer> ().enabled = true;
					StartCoroutine (HideRenderer (positivePaceDisplay));

				} else if (actualPace < 0){
					
					negativePaceDisplay.text = positivePaceDisplay.text = String.Format ("-{3:#0}:{2:00}:{1:00}.{0:00}",
							Mathf.Floor ((float)pace.Milliseconds / 10),
							Mathf.Floor ((float)pace.Seconds),
							Mathf.Floor ((float)pace.Minutes),
							Mathf.Floor ((float)pace.TotalHours));

					negativePaceDisplay.GetComponent<Renderer> ().enabled = true;
					StartCoroutine (HideRenderer (negativePaceDisplay));
				}

			} else if (collider.tag == "Finish") {

				actualPace = (actualPace / 1000);
				actualPace = (ExerciseSettings.time * 60) - actualPace;

				if (actualPace >= 0) {
					pace = TimeSpan.FromSeconds (actualPace);
					positivePaceDisplay.text = String.Format ("+{3:#0}:{2:00}:{1:00}.{0:00}",
						Mathf.Floor ((float)pace.Milliseconds / 10),
						Mathf.Floor ((float)pace.Seconds),
						Mathf.Floor ((float)pace.Minutes),
						Mathf.Floor ((float)pace.TotalHours));

					positivePaceDisplay.GetComponent<Renderer> ().enabled = true;

				} else if (actualPace < 0){
					pace = TimeSpan.FromSeconds (Math.Abs (actualPace));
					negativePaceDisplay.text = positivePaceDisplay.text = String.Format ("-{3:#0}:{2:00}:{1:00}.{0:00}",
						Mathf.Floor ((float)pace.Milliseconds / 10),
						Mathf.Floor ((float)pace.Seconds),
						Mathf.Floor ((float)pace.Minutes),
						Mathf.Floor ((float)pace.TotalHours));

					negativePaceDisplay.GetComponent<Renderer> ().enabled = true;
				}
			}
		}
	}

	//call StartCoroutine(HideRenderer()); to hide text mesh after 3 seconds
	IEnumerator HideRenderer(TextMesh paceDisplay){
		yield return new WaitForSeconds (5.0f);
		paceDisplay.GetComponent<Renderer> ().enabled = false;
	}

	IEnumerator RestartTrack(GameObject textMesh){
		yield return new WaitForSeconds (5.0f);
		textMesh.GetComponent<Renderer> ().enabled = false;
		SceneManager.LoadScene ("UITool");
	}


}
