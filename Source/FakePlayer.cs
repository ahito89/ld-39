using UnityEngine;
using System;
using System.Collections;

public class FakePlayer : MonoBehaviour {

	public int energy = 10;
	public int keys;
	public int stars = 0;
	public bool interactable = false;

	private bool teleporting = true;
	private bool flagged = false;

	private Tile portal;
	private bool touchingPortal = false;

	public static FakePlayer instance;

	private void Awake() {
		instance = this;
		keys = 0;
	}

	private void Update() {
		if (!flagged &&  Tutorial.phase > 1) {
			Vector2 target = GetTarget (StageCamera.angle);
			InitiateMove ((int)target.x, (int)target.y);
		}
		if (!flagged && touchingPortal && Input.GetKeyDown (KeyCode.Space)) {
			float p = portal.exit.transform.position.x;
			float q = portal.exit.transform.position.z;
			transform.position = new Vector3 (p, 1, q);
		}
	}

	public Vector2 GetTarget(float angle) {
		int i = 0;
		int j = 0;
		angle = angle % 360;
		if (angle >= 0 || angle < 90) {
			i = (int)transform.position.x + (int)Input.GetAxis ("Horizontal");
			j = (int)transform.position.z + (int)Input.GetAxis ("Vertical");
		}
		if (angle >= 90 && angle < 180) {
			i = (int)transform.position.x + (int)Input.GetAxis ("Vertical");
			j = (int)transform.position.z - (int)Input.GetAxis ("Horizontal");
		}
		if (angle >= 180 && angle < 270) {
			i = (int)transform.position.x - (int)Input.GetAxis ("Horizontal");
			j = (int)transform.position.z -	 (int)Input.GetAxis ("Vertical");
		}
		if (angle >= 270 && angle < 360) {
			i = (int)transform.position.x - (int)Input.GetAxis ("Vertical");
			j = (int)transform.position.z + (int)Input.GetAxis ("Horizontal");
		}
		return new Vector2 ((int)i, (int)j);
	}

	public void SetPosition (Vector3 position) {
		transform.position = position;
	}

	private void InitiateMove (int i, int j) {
		int x = (int)transform.position.x;
		int z = (int)transform.position.z;
		Vector3 position = transform.position;
		Vector3 snap = new Vector3 (x, 1, z);
		bool snapped = (position == snap);
		bool diagonal  = (i - x) != 0 && (j - z) != 0;
		bool different = (x != i) || (z != j);
		bool valid = Stage.Check (i, j);
		if (snapped && !diagonal && different && valid) {
			StartCoroutine (Move (i, j));
		}
	}

	private IEnumerator Move (int i, int j) {
		flagged = true;
		if (Tutorial.phase == 2) {
			Tutorial.phase = 3;
		}
		Vector3 target = new Vector3 (i, 1, j);
		Vector3 position = transform.position;
		Vector3 velocity = Vector3.zero;
	     while (position != target) {
			position = transform.position;
			transform.position = Vector3.SmoothDamp (position, target, ref velocity, 0.03f);
	        yield return null;
		}
		transform.position = target;
		Stage.counter+= 1 * Stage.instance.	enemies;
		Stage.turn++;
		flagged = false;
	}

	private void OnTriggerEnter (Collider collider) {
		if (collider.tag == "Stage Tile") {
			StartCoroutine (Stage.instance.Preload ("Next"));
		}
		if (collider.tag == "Level Tile") {
			StartCoroutine (Stage.instance.Preload ("Map"));
		}
		if (collider.tag == "Drain Tile") {
			energy -= 2;
		}
		if (collider.tag == "Portal Tile") {
			GameObject portalObject = collider.gameObject;
			portal = portalObject.GetComponent<Tile>();
			teleporting = teleporting ? false : true;
			touchingPortal = true;
		}
		if (collider.tag == "Small Battery") {
			energy += 4;
			Destroy (collider.gameObject);
		}
		if (collider.tag == "Large Battery") {
			energy += 10;
			Destroy (collider.gameObject);
		}
		if (collider.tag == "Key") {
			keys++;
			Destroy (collider.gameObject);
		}
		if (collider.tag == "Star") {
			stars++;
			Destroy (collider.gameObject);
		}
		if (collider.tag == "Enemy") {
			StartCoroutine (Stage.instance.Preload ("Live"));
		}
	}
}
