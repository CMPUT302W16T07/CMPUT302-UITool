using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	private float movementSpeed = 0.25f;
	public GameObject character;

	void Update () {

		if (Input.GetKey(KeyCode.RightArrow)){

			Vector3 newPosition = character.transform.position;
			newPosition.x+=movementSpeed;
			character.transform.position = newPosition;

		}
		if (Input.GetKey(KeyCode.LeftArrow)){

			Vector3 newPosition = character.transform.position;
			newPosition.x-=movementSpeed;
			character.transform.position = newPosition;

		}
		if (Input.GetKey(KeyCode.UpArrow)){

			Vector3 newPosition = character.transform.position;
			newPosition.z+=movementSpeed;
			character.transform.position = newPosition;

		}
		if (Input.GetKey(KeyCode.DownArrow)){

			Vector3 newPosition = character.transform.position;
			newPosition.z-=movementSpeed;
			character.transform.position = newPosition;

		}

	}
}
