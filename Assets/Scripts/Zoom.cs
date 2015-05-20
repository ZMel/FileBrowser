using UnityEngine;
using System.Collections;

public class Zoom : MonoBehaviour {

	float minFov = 15f;
	float maxFov = 90f;
	float sensitivity = 70f;
	float sensitivity2 = -5f;

	void Update () {


		// If pushing alt and moving scroll wheel
		if(Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.Z))
		{
			float verticle = Input.GetAxis("Mouse Y") * sensitivity2;
			float horizontal = Input.GetAxis("Mouse X") * sensitivity2;
			float zAxis = Input.GetAxis("Mouse ScrollWheel") * sensitivity;

			Vector3 position = new Vector3(Camera.main.transform.position.x + horizontal,	Camera.main.transform.position.y + verticle,	Camera.main.transform.position.z + zAxis);
			Camera.main.transform.position = position;
	
		}

	}
}
