using UnityEngine;
using System.Collections;

public class ShowInfo : MonoBehaviour {

	public string Showname;
	public int episodes;
	LineRenderer line;
	public int Ring;
	public bool hasChild;
	public bool hasLine;
	public bool onTop;
	float width;
	bool shrink;
	GameObject LinkedObject;
	Color col;
	public GameObject obj;
	void Start () {
		width = Random.Range (0.2f, 0.5f);
		if (Random.value >= 0.5f)
			shrink = true;
		else
			shrink = false;

		if (hasLine){
			line = GetComponent<LineRenderer> ();
			col = new Color(Random.Range(0.2f,1f),Random.Range(0.2f,1f),Random.Range(0.2f,1f), 0.35f);

			line.SetColors(col, new Color(1,1,1,0.35f));
			if (Ring == -1){
			}
			else if (Ring == 0){
				foreach(Transform child in GameObject.Find ("Ring").transform){
					ShowInfo info = child.GetComponent<ShowInfo>();
					if (Showname == info.Showname){
						info.LinkedObject = gameObject;
						obj = child.gameObject;
						hasChild = true;
						break;
					}
				}
			} else {
				foreach(Transform child in GameObject.Find ("Ring" + Ring.ToString()).transform){
					ShowInfo info = child.GetComponent<ShowInfo>();
					if (Showname == info.Showname){
						info.LinkedObject = gameObject;
						obj = child.gameObject;
						hasChild = true;
						break;
					}
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
		if (LinkedObject != null) {


			LineRenderer lines = LinkedObject.GetComponent<LineRenderer>();
			lines.SetColors (new Color (col.r, col.g, col.b, 0.85f), new Color (1, 1, 10.85f));
		}

		transform.FindChild("TestObject").GetComponent<TextMesh>().text = Showname;	
		transform.FindChild ("Plane").GetComponent<MeshRenderer> ().enabled = true;
		transform.FindChild ("Plane").transform.rotation = Quaternion.Euler (-90, 0, 0);
	}
	void OnMouseExit () {
		if (LinkedObject != null) {
			LineRenderer lines = LinkedObject.GetComponent<LineRenderer> ();
			lines.SetColors (new Color (col.r, col.g, col.b, 0.35f), new Color (1, 1, 1,0.35f));
		}


		transform.FindChild("TestObject").GetComponent<TextMesh>().text = "";
		transform.FindChild ("Plane").GetComponent<MeshRenderer> ().enabled = false;
	}

}
