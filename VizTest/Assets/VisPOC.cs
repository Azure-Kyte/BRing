using UnityEngine;
using System.Collections;
using System.IO;
using System.Linq;

public class VisPOC : MonoBehaviour {
	
	public int i;
	int j;
	public GameObject cube;
	public GameObject cube2;
	public int numshows88;
	public int numshows89;
	float camTime = 1;
	int CameraLerp;

	public int showPos;
	BroadcastStruct ep;
	public int episodeCount;
	public int epNum;
	public float[] distances = new float[764];
	int xvalue;
	public string test;

	//basic struct for the broadcast data.
	[System.Serializable] public class BroadcastStruct {
		public string program;
		public string region;
		public string date;
		public string duration;
		public int ID;
		public BroadcastStruct episode;
	}

	//instantiating a sample broadcaststruct array, each member in the array is a show, with an internal episode.
	public BroadcastStruct[] year1988 = new BroadcastStruct[500];
	public BroadcastStruct[] year1989 = new BroadcastStruct[500];


	void Start () {

		//Parsing the CSV file.
		using (StreamReader reader = new StreamReader(@"./assets/ABC data 50000.csv")) {
			reader.ReadLine (); //Skip the first line, which demonstrates the Data Structure.

			//While i is less than the array length, read a line of data, then parse that into the array.
			for (i = 0; i < 50000; i++) {
				string title = "";
				string[] line = reader.ReadLine().Split (','); //Split the read line into sections by ,
				/*If the line begins with ", it means there is a comma in the title, which means we 
												have to traverse through the array until we reach a terminating " */

				if (line[1].StartsWith("1989")) {
					numshows89++;
					if (line [6].StartsWith ("\"")) { 
						
						for (j = 0; !line[6 + j].EndsWith("\""); j++) {
							title += line [6 + j]; //Append the line array item to the title string.
						}
						title += line [6 + j]; //Append the final item to the title string. This completes the title.
					} else {
						title = line [6]; //There wasn't a " at the start, so there's no commas, meaning we can just use the split easily.
					}
					
					if (line[6] == " " || line [5].StartsWith ("\"") || line[7] == " "){
						title = "";
						if (line [5].StartsWith ("\"")) { 
							
							for (j = 0; !line[5 + j].EndsWith("\""); j++) {
								title += line [5 + j]; //Append the line array item to the title string.
							}
							title += line [5 + j]; //Append the final item to the title string. This completes the title.
						} else {
							title = line [5]; //There wasn't a " at the start, so there's no commas, meaning we can just use the split easily.
						}
					}
					
					j = 0; 	//initialise j, traverse through until either we reach an empty area in the array or an object with the same 
					//program name. j will be the correct spot in either case.
					if (line[1].StartsWith("1989")){
						while (year1989[j].program != "" && year1989[j].program != title){
							j++;
						}
						title = title.Replace("\"", "");
						if (year1989[j].program == ""){ //empty array case. Chuck the object in straight away.
							year1989 [j].region = line [0];
							year1989 [j].date = line [1];
							year1989 [j].program = title;
							year1989 [j].duration = line [2];
							year1989 [j].episode = null;
							year1989[j].ID = j;
						} else if (year1989[j].program == title){ //Populated entry case, traverse through the LL to find the right spot to place the Episode.
							BroadcastStruct LL = year1989[j];
							while (LL.episode != null){
								LL = LL.episode;
							}
							
							BroadcastStruct episode = new BroadcastStruct();
							episode.ID = j;
							episode.region = line [0];
							episode.date = line [1];
							episode.program = title;
							episode.duration = line [2];
							episode.episode = null;
							LL.episode = episode;
						}
					}
				}

				if (line[1].StartsWith("1988")) {
					numshows88++;
					if (line [6].StartsWith ("\"")) { 

						for (j = 0; !line[6 + j].EndsWith("\""); j++) {
							title += line [6 + j]; //Append the line array item to the title string.
						}
						title += line [6 + j]; //Append the final item to the title string. This completes the title.
					} else {
						title = line [6]; //There wasn't a " at the start, so there's no commas, meaning we can just use the split easily.
					}

					if (line[6] == " " || line [5].StartsWith ("\"") || line[7] == " "){
						title = "";
						if (line [5].StartsWith ("\"")) { 
							
							for (j = 0; !line[5 + j].EndsWith("\""); j++) {
								title += line [5 + j]; //Append the line array item to the title string.
							}
							title += line [5 + j]; //Append the final item to the title string. This completes the title.
						} else {
							title = line [5]; //There wasn't a " at the start, so there's no commas, meaning we can just use the split easily.
						}
					}

					j = 0; 	//initialise j, traverse through until either we reach an empty area in the array or an object with the same 
							//program name. j will be the correct spot in either case.
					if (line[1].StartsWith("1988")){
						while (year1988[j].program != "" && year1988[j].program != title){
							j++;
						}
						title = title.Replace("\"", "");
						if (year1988[j].program == ""){ //empty array case. Chuck the object in straight away.
							year1988 [j].region = line [0];
							year1988 [j].date = line [1];
							year1988 [j].program = title;
							year1988 [j].duration = line [2];
							year1988 [j].episode = null;
							year1988[j].ID = j;
						} else if (year1988[j].program == title){ //Populated entry case, traverse through the LL to find the right spot to place the Episode.
							BroadcastStruct LL = year1988[j];
							while (LL.episode != null){
								LL = LL.episode;
							}

							BroadcastStruct episode = new BroadcastStruct();
							episode.ID = j;
							episode.region = line [0];
							episode.date = line [1];
							episode.program = title;
							episode.duration = line [2];
							episode.episode = null;
							LL.episode = episode;
						}
					}

				}
			}


		}
		for (int k = 0; k < 764; k++){
			Object spawner;
			float angle = 2 * Mathf.PI / 764;

			float x = 65 * Mathf.Sin(angle * k);
			float y = 65 * Mathf.Cos (angle * k);
			string name = "cube" + k.ToString();
			spawner = GameObject.Instantiate(cube, new Vector3(x, y, 0), Quaternion.Euler(0,0,0));
			spawner.name = name;

			GameObject.Find(name).transform.parent = GameObject.Find("Ring").transform;
			GameObject.Find(name).GetComponent<ShowInfo>().Showname = year1988[k].program;

			BroadcastStruct List = year1988[k];
			episodeCount = 1;
			while(List.episode != null ){
				List = List.episode;
				episodeCount++;
			}

			GameObject.Find(name).GetComponent<ShowInfo>().episodes = episodeCount;

		}

		for (int k = 0; k < 748; k++) {
			Object spawner;
			float angle = 2 * Mathf.PI / 748;
			
			float x = 65 * Mathf.Sin (angle * k);
			float y = 65 * Mathf.Cos (angle * k);
			string names = "cube" + k.ToString () + "-2";
			spawner = GameObject.Instantiate (cube2, new Vector3 (x, y, 15), Quaternion.Euler (0, 0, 0));
			spawner.name = names;
			
			GameObject.Find (names).transform.parent = GameObject.Find ("Ring2").transform;
			GameObject.Find (names).GetComponent<ShowInfo> ().Showname = year1989 [k].program;
			
			BroadcastStruct List = year1989 [k];
			episodeCount = 1;
			while (List.episode != null) {
				List = List.episode;
				episodeCount++;
			}
			
			GameObject.Find (names).GetComponent<ShowInfo> ().episodes = episodeCount;
		}
		//WORKING CODE: Create an Array of x entries.
		/* using (StreamReader reader = new StreamReader(@"./assets/ABC data 50000.csv")) {
			reader.ReadLine ();
			for (i = 0; i < test.Length; i++) {
				string title = "";
				string[] line = reader.ReadLine ().Split (',');

				test [i].region = line [0];
				test [i].date = line [1];
				if (line [5].StartsWith ("\"")) {

					for (j = 0; !line[5 + j].EndsWith("\""); j++) {
						title += line [5 + j];
					}
					test [i].program = title + line [5 + j];
				}
				else {
					test [i].program = line [5];
				}
				test [i].duration = line [2];
			}
		}*/


	}

	void Update() {
		camTime += Time.deltaTime * 1.3f;
		if (Input.GetMouseButton (1)) {
			if (Input.GetAxis ("Mouse X") < 0) {
				if (Input.GetKey (KeyCode.LeftShift))
					xvalue -= 4;
				xvalue--;
				if (xvalue < 0)
					xvalue = 360;
			}
			if (Input.GetAxis ("Mouse X") > 0) {
				if (Input.GetKey (KeyCode.LeftShift))
					xvalue += 4;
				xvalue++;
				if (xvalue > 360)
					xvalue = 0;
			}
		}

		GameObject ring = GameObject.Find ("Global");
		if (xvalue > 0)
			ring.transform.rotation = Quaternion.Euler (0, 0, xvalue);
		else
			ring.transform.rotation = Quaternion.Euler (0, 0, 0);

		if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
			if (CameraLerp == 1 && camTime > 1){
				camTime = 0f;
				CameraLerp = 0;
			}
		} else if (Input.GetAxis ("Mouse ScrollWheel") > 0) {
			if (CameraLerp == 0 && camTime > 1){
				camTime = 0f;
				CameraLerp = 1;
			}
		}

		if (CameraLerp == 0) {
			if (Camera.main.transform.position.z != -10){
				Camera.main.transform.position = Vector3.Lerp(new Vector3(0,69,0), new Vector3(0,69,-10), camTime);
			}
		} else if (CameraLerp == 1) {
			if (Camera.main.transform.position.z != 10){
				Camera.main.transform.position = Vector3.Lerp(new Vector3(0,69,-10), new Vector3(0,69,0), camTime);
			}
		}
		
	}
}
