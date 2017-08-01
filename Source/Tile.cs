using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Tile : MonoBehaviour {

	public enum Type {Default, Stage, Level, Ice, Portal, Drain, Fire, Door};
	public Type type = Type.Default;

	public GameObject flame;
	public GameObject exit;
	public bool startsOff = true;
	private int oldTurn = 0;
	private bool flagged = false;
	private new Renderer renderer;

	private bool locked = true;

	private void Start () {
		renderer = GetComponent<Renderer>();
	}

	private void Update() {
		gameObject.name = gameObject.tag;
		if (type == Type.Default) {
			gameObject.tag = "Default Tile";
			switch (Game.index + 1) {
				case 1:
					//renderer.material = (Material)Resources.Load("Materials/Pastel/Orange");
					break;
				case 2:
					//renderer.material = (Material)Resources.Load("Materials/Grey");
					break;
				case 3:
					//renderer.material = (Material)Resources.Load("Materials/DarkGrey");
					break;
			}
		}
		if (type == Type.Stage) {
			gameObject.tag = "Stage Tile";
			renderer.material = (Material)Resources.Load("Materials/Pastel/Red");
		}
		if (type == Type.Level) {
			gameObject.tag = "Level Tile";
			renderer.material = (Material)Resources.Load("Materials/Pastel/Red");
		}
		if (type == Type.Ice) {
			gameObject.tag = "Ice Tile";
			renderer.material = (Material)Resources.Load("Materials/Pastel/Blue");
		}
		if (type == Type.Portal) {
			gameObject.tag = "Portal Tile";
			//renderer.material = (Material)Resources.Load("Materials/Ring");
		}
		if (type == Type.Fire) {
			if (oldTurn != Stage.turn) {
				oldTurn = Stage.turn;
				flagged = false;
			}
			if ((Stage.turn % 2 == 1) == startsOff) {
				renderer.material = (Material)Resources.Load("Materials/Pastel/Orange");
				if (!flagged) {
					//Instantiate (flame, transform.position, Quaternion.identity);
					flagged = true;
				}
			} else {
				renderer.material = (Material)Resources.Load("Materials/White");
			}
		}
		if (type == Type.Door) {
			gameObject.tag = "Door Tile";
			int x = (int)transform.position.x;
			int z = (int)transform.position.z;
			renderer.material = (Material)Resources.Load("Materials/Pastel/Green");
			if (locked) {
				Stage.Disable (x, z);
			} else {
				Stage.Enable (x, z);
				Debug.Log (Stage.Check (x, z));
				gameObject.SetActive (false);
			}
		}
	}

	private IEnumerator Fade() {
		float alpha = renderer.material.color.a;
		while (alpha > 0) {
			alpha -= 0.03f;
			Color color = renderer.material.color;
			color.a = alpha;
			renderer.material.color = color;
			yield return null;
		}
		Destroy (gameObject);
	}

	private void OnTriggerEnter (Collider other) {
		if (other.tag == "Player") {
			if (type == Type.Fire) {
				if ((Stage.turn % 2 == 1) == startsOff) {
					StartCoroutine (Stage.instance.Preload ("Live"));
				}
			}
			if (type == Type.Door) {
				if (Player.instance.keys > 0) {
					Player.instance.keys -= 1;
					locked = false;
				}
			}
		}
	}

	private void OnTriggerExit (Collider other) {
		if (other.tag == "Player") {
			if (type == Type.Ice) {
				int x = (int)transform.position.x;
				int z = (int)transform.position.z;
				Stage.Disable (x, z);
				StartCoroutine (Fade());
			}
		}
	}
}
