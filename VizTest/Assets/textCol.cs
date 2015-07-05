using UnityEngine;
using System.Collections;

public class textCol : MonoBehaviour {
	Color col;
	float alpha;
	// Use this for initialization
	void Start() {
		col = new Color (Random.Range (0.4f, 1f), Random.Range (0.4f, 1f), Random.Range (0.4f, 1f));
	}

	void Update () {
		float alpha = 0.5f;
		float distance = Vector3.Distance (transform.position, Camera.main.transform.position);

		if (distance > 24f) {

			alpha = 1 - distance / 55f;
			Mathf.Clamp(alpha, 0f, 1f);
		}

		Color col2 = new Color (col.r, col.g, col.b, alpha);
		GetComponent<TextMesh> ().color = col2;
	}

}
