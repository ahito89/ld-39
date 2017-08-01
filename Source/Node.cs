using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour {

	public string stage;
	public int stars = 0;
	public int index = 0;

	public GameObject text;
	public GameObject branch;
	private MeshRenderer mesh;
	public bool unlocked = false;

	private float alpha = 0;
	public bool highlighted = false;

	private void Start() {
		mesh = text.GetComponent<MeshRenderer>();
		mesh.material.color = new Color (1, 1, 1, 0);
	}

	private void Update() {
		text.GetComponent<TextMesh>().text = stars + "/3";
		if (highlighted) {
			alpha = alpha < 1 ? alpha += 0.03f : 1;
			mesh.material.color = new Color (1, 1, 1, alpha);
		} else {
			alpha = alpha > 0 ? alpha -= 0.03f : 0;
			mesh.material.color = new Color (1, 1, 1, alpha);
		}
	}
}
