using UnityEngine;
using System.Collections;

public class TimerController : MonoBehaviour {

	public SessionCompleteCanvasScript sessionCompleteCanvas;

	void OnTriggerEnter(Collider collider){
		if (collider.tag == "Start" || collider.tag == "Finish") {
			Destroy (collider.gameObject);
			Debug.Log ("Collision Detected");
			if (collider.tag == "Finish") {
				sessionCompleteCanvas.sessionWillFinish ();
			}
		}
	}
}
