using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapCamera : MonoBehaviour {

	public Node[] nodes;

	private Node focus;
	public static int index = 0;

	public GameObject image;
	private Vector3 velocity = Vector3.zero;
	private float smooth = 0.3f;
	public Material stageTemplate;

	private void Start() {
		StartCoroutine ( Fade());
		focus = nodes[index];
		nodes[0].unlocked = true;
		if (Game.index >= 0) {
			nodes[Game.index].stars = nodes[Game.index].stars > Game.GetStars() ? nodes[Game.index].stars : Game.GetStars();
			nodes[Game.index + 1].unlocked = true;
		}
	}

	private void Update () {
		focus.highlighted = true;
		Vector3 position = transform.position;
		Vector3 target = focus.transform.position;
		transform.position = Vector3.SmoothDamp (position, target, ref velocity, 0.3f);
		if ((Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.RightArrow)) && (index < nodes.Length - 1)) {
			index++;
			focus.highlighted = false;
			focus = nodes[index];
		}
		if ((Input.GetKeyDown (KeyCode.DownArrow) || Input.GetKeyDown (KeyCode.LeftArrow)) && (index > 0)) {
			index--;
			focus.highlighted = false;
			focus = nodes[index];
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			Game.index = index;
			StartCoroutine (Preload (focus.stage));
		}
	}

	private IEnumerator Fade() {
		//GUITexture texture = GetComponent<GUITexture>();
		//texture.enabled = true;
		Image im = image.GetComponent<Image>();
		Color color = im.color;
		float alpha = color.a;
		while (alpha > 0) {
			alpha -= 0.01f;
			im.color = new Color (0, 0, 0, alpha);
			yield return null;
		}
	}

	public IEnumerator Preload (string stage) {
		//GUITexture texture = GetComponent<GUITexture>();
		//texture.enabled = true;
		float alpha = image.GetComponent<Image>().color.a;
		while (alpha < 1) {
			alpha += 0.01f;
			image.GetComponent<Image>().color = new Color (0.0f, 0.0f, 0.0f, alpha);
			Debug.Log (alpha);
			yield return null;
		}
		Load (stage);
	}

	public static void Load (string stage) {
		Scene scene = SceneManager.GetActiveScene();
		int index = scene.buildIndex;
		SceneManager.LoadScene (stage);
	}
}
