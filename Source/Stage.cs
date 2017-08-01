using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Stage : MonoBehaviour {

	public static int[,] matrix = new int[9, 9];
	public static int counter = 0;
	public static int turn = 0;
	public static bool loadComplete = true;

	public static Stage instance;
	public int enemies = 1;

	private void Awake() {
		loadComplete = true;
     	instance = this;
		counter = 0;
		turn = 0;
 	}

	private void Start() {
		for (int i = 0; i < 7; i++) {
			for (int j = 0; j < 7; j++) {
				matrix[i, j] = 0;
			}
		}
		StartCoroutine (Fade());
		Transform parent = transform.Find ("Tiles");
		foreach (Transform child in parent) {
			Debug.Log (child.name);
			int i = (int)child.position.x + 4;
			int j = (int)child.position.z + 4;
			matrix[i, j] = 1;
		}
		Debug.Log (matrix[6, 3]);
	}

	public static bool Check (int x, int z) {
		return matrix[x + 4, z + 4] != 0;
	}

	public static void Disable (int x, int z) {
		matrix[x + 4, z + 4] = 0;
	}

	public static void Enable (int x, int z) {
		matrix[x + 4, z + 4] = 1;
	}

	public IEnumerator Preload (string type) {
		GUITexture texture = GetComponent<GUITexture>();
		texture.enabled = true;
		float alpha = texture.color.a;
		while (alpha < 1) {
			alpha += 0.01f;
			texture.color = new Color (0.0f, 0.0f, 0.0f, alpha);
			yield return null;
		}
		Load (type);
	}

	public static void Load (string type) {
		if (loadComplete) {
			loadComplete = false;
			Scene scene = SceneManager.GetActiveScene();
			int index = scene.buildIndex;
			switch (type) {
				case "Map":
					Game.SetStar(Game.tempStars);
					Game.tempStars = 0;
					SceneManager.LoadScene (2);
					break;
				case "Live":
					SceneManager.LoadScene (index);
					break;
				case "Next":
					SceneManager.LoadScene (index + 1);
					break;
			}
		}

	}

	private IEnumerator Fade() {
		GUITexture texture = GetComponent<GUITexture>();
		texture.enabled = true;
		float alpha = texture.color.a;
		while (alpha > 0) {
			alpha -= 0.01f;
			texture.color = new Color (0, 0, 0, alpha);
			yield return null;
		}
	}

	private void OnDrawGizmos() {
        Gizmos.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(7, 1, 7));
    }
}
