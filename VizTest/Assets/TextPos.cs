using UnityEngine;
using System.Collections;

public class TextPos : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		transform.position = transform.parent.transform.position + new Vector3 (0, 0.5f, 0);
		transform.rotation = Quaternion.Euler (0, 0, 0);
	}
}
