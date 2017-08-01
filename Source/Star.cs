using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour {

	public float amplitude = 0.15f;
	private float speed = 2;
	private float tempVal;
	private Vector3 tempPos;

	private void Start () {
		tempVal = transform.position.y;
		tempPos = transform.position;
	}

void Update ()
{
	transform.Rotate (new Vector3 (0, 45, 0) * Time.deltaTime);
	tempPos.y = tempVal + amplitude * Mathf.Sin(speed * Time.time);
	transform.position = tempPos;
}
}
