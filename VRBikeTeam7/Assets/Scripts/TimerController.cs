using UnityEngine;
using System.Collections;

public class TimerController : MonoBehaviour {


	void OnTriggerEnter(Collider collider){
		if (collider.tag == "Start" || collider.tag == "Finish") {
			Destroy (collider.gameObject);
			Debug.Log ("Collision Detected");
		}
	}
}
