using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour {

	public static int stars;
	public static int moves;

	public Text starsText;
	public Text stepsText;

	public Image battery;
	public static int stars11;
	public static int stars21;
	public static int stars31;
	public static int index = -1;

	public UI instance;
		public static int starsTimer = 0;
		public static int stepsTimer = 0;
		private int starsOld = 0;
		private bool starsFaded = false;
		private int stepsOld = 0;
		private bool stepsFaded = false;

	void Awake() {
		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
		}
		Scene scene = SceneManager.GetActiveScene();
		int bi = scene.buildIndex;
		if (bi == 2) {
			Destroy (gameObject);
		}
		index = -1;
		starsOld = 0;
		stepsOld = 0;
		stepsTimer = 1;
		starsFaded = false;
		stepsFaded = false;
		starsTimer = 1;
		index = 0;
	}

	void Start() {
		stars11 = 0;
		stars21 = 0;
		stars31 = 0;
		if (Player.instance != null) {
			stepsOld = Player.instance.energy;
		}
	}

	public static int GetStars(int index) {
		switch (SceneManager.GetActiveScene().buildIndex) {
			case 5:
				return stars11;
			case 1:
				return stars21;
			case 2:
				return stars31;
			default:
				return 0;
		}
	}

	void Update () {
		if (index != -1) {
			switch (index) {
				case 0:
					stars11 = stars;
					break;
				case 1:
					stars21 = stars;
					break;
				case 2:
					stars31 = stars;
					break;
				default:
					break;
			}
		}
		starsText.text = Game.tempStars + " / 3";
		starsTimer++;
		if (starsOld != Game.tempStars && starsFaded) {
			StartCoroutine (FadeIn (starsText));
			starsFaded = false;
			starsOld = Game.tempStars;
			starsTimer = 0;
		}
		if (starsTimer > 300 && Game.tempStars == stars && !starsFaded) {
			StartCoroutine (FadeOut (starsText));
			starsFaded = true;
		}
		if (Player.instance != null) {
			stepsText.text = (Player.instance.energy.ToString());
					stepsTimer++;
			if (stepsTimer > 300 && stepsOld == Player.instance.energy && !stepsFaded) {
				StartCoroutine (FadeOut (stepsText));
				StartCoroutine (FadeOut ());
				stepsFaded = true;
			}
			if (stepsOld != Player.instance.energy && stepsFaded) {
				StartCoroutine (FadeIn (stepsText));
				StartCoroutine (FadeIn ());
				stepsFaded = false;
				stepsOld = Player.instance.energy;
				stepsTimer = 0;
			}
		}


	}

	private IEnumerator FadeOut(Text text) {
		float alpha = text.color.a;
		alpha = 1;
		while (alpha > 0) {
			alpha -= 0.015f;
			text.color = new Color (1, 1, 1, alpha);
			yield return null;
		}
	}

	private IEnumerator FadeOut() {
		float alpha = battery.color.a;
		alpha = 1;
		while (alpha > 0) {
			alpha -= 0.015f;
			battery.color = new Color (1, 1, 1, alpha);
			yield return null;
		}
	}

	private IEnumerator FadeIn(Text text) {
		float alpha = text.color.a;
		alpha = 0;
		while (alpha < 1) {
			alpha += 0.015f;
			text.color = new Color (1, 1, 1, alpha);
			yield return null;
		}
	}

	private IEnumerator FadeIn() {
		float alpha = battery.color.a;
		alpha = 0;
		while (alpha < 1) {
			alpha += 0.015f;
			battery.color = new Color (1, 1, 1, alpha);
			yield return null;
		}
	}
}
