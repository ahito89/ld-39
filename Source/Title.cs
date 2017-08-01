using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour {

	private Image mesh;

	private void Start () {
		Screen.SetResolution (960, 600, false);
		mesh = GetComponent<Image>();
		mesh.color = new Color (1, 1, 1, 0);
		StartCoroutine ("Animation");
	}

	private IEnumerator Animation() {
		float alpha = 0;
		while (alpha < 1) {
			alpha += 0.005f;
			mesh.color = new Color (1, 1, 1, alpha);
			yield return null;
		}
		yield return new WaitForSeconds (1.0f);
		while (alpha > 0) {
			alpha -= 0.025f;
			mesh.color = new Color (1, 1, 1, alpha);
			yield return null;
		}
		SceneManager.LoadScene ("Tutorial");
	}
}
