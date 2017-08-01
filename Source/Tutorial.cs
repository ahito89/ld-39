using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {

	public Text text;
	public static int phase = 0;

	void Start () {
		phase = 0;
	}

	// Update is called once per frame
	void Update () {
		Debug.Log (phase);
		if ((Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.D)) && phase == 0){
			StartCoroutine ("FadeOut");
			phase = 1;
		}
		if (phase == 3) {
			StartCoroutine ("FadeOut");
		}
	}

	private IEnumerator FadeOut() {
		float alpha = text.color.a;
		alpha = 1;
		while (alpha > -0.03) {
			alpha -= 0.015f;
			text.color = new Color (1, 1, 1, alpha);
			yield return null;
		}
		if (text.text == "Use A & D to rotate camera" && phase == 1 && alpha <= 0) {
			text.text = "Use arrow keys to move";
			phase = 2;
		}
		if (text.text == "Use arrow keys to move" && phase == 3 && alpha <= 0) {
			text.text = "Reach the goal to start game";
			phase = 4;
		}
		while (alpha < 1) {
			alpha += 0.01f;
			text.color = new Color (1, 1, 1, alpha);
			yield return null;
		}
	}
}
