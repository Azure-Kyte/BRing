using UnityEngine;
using System.Collections;

public class ShowInfo : MonoBehaviour {

	public string Showname;
	public int episodes;
	LineRenderer line;
	bool hasChild;
	public bool hasLine;
	float width;
	bool shrink;
	public GameObject obj;
	void Start () {
		width = Random.Range (0.2f, 0.5f);
		if (Random.value >= 0.5f)
			shrink = true;
		else
			shrink = false;

		if (hasLine){
			line = GetComponent<LineRenderer> ();
			line.SetColors(new Color(Random.Range(0.6f,1f),Random.Range(0.6f,1f),Random.Range(0.6f,1f), 0.35f), new Color(1,1,1,0.35f));

			foreach(Transform child in GameObject.Find ("Ring").transform){
				ShowInfo info = child.GetComponent<ShowInfo>();
				if (Showname == info.Showname){
					obj = child.gameObject;
					hasChild = true;
				}
			}
		}

	}
	void Update () {
		if (shrink) {
			if (width > 0.2f)
				width -= 0.005f;
			else {
				width = 0.2f;
				shrink = !shrink;
			}
		} else {
			if (width < 0.5f)
				width += 0.005f;
			else {
				width = 0.5f;
				shrink = !shrink;
			}
		}

		if (hasChild && hasLine) {
			line.SetWidth(width,width);
			line.SetPosition(0, transform.position);
			line.SetPosition(1, obj.transform.position);
		}
	}

	void OnMouseOver () {
		transform.FindChild("TestObject").GetComponent<TextMesh>().text = Showname;	
		transform.FindChild ("Plane").GetComponent<MeshRenderer> ().enabled = true;
		transform.FindChild ("Plane").transform.rotation = Quaternion.Euler (-90, 0, 0);
	}
	void OnMouseExit () {
		transform.FindChild("TestObject").GetComponent<TextMesh>().text = "";
		transform.FindChild ("Plane").GetComponent<MeshRenderer> ().enabled = false;
	}

}
