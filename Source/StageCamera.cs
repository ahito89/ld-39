using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCamera : MonoBehaviour {

	public static float angle;

	private void Start() {
		transform.eulerAngles = new Vector3 (0, 0, 0);
		angle = transform.eulerAngles.y;
	}

	private void Update() {
		float velocity = Input.GetAxis ("Angular");
		transform.eulerAngles -= new Vector3 (0, velocity, 0);
		angle = transform.eulerAngles.y;
	}
}
