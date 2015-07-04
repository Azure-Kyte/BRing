using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System;
using ABCTVBroadcast;
using ABCTVBroadcast.Models;

namespace VisPOC {

	[Serializable]
	public class BroadcastStruct
	{
		public string SeriesName;
		
		public List<EpisodeStruct> episodeList;
		
		public BroadcastStruct(string Name, EpisodeStruct episode)
		{
			episodeList = new List<EpisodeStruct>();
			SeriesName = Name;
			
			episodeList.Add(episode);
		}
		
		public void order()
		{
			episodeList.Sort();
		}
		
		public void printSeries()
		{
			//Console.WriteLine("{0}:", SeriesName);
			foreach (EpisodeStruct e in episodeList)
			{
				// Console.WriteLine("\t{0} {1} {2}", e.date.ToShortDateString(),e.programName,e.region);
			}
		}
	}
	
	public class EpisodeStruct:IComparable<EpisodeStruct>
	{
		public string programName;
		public string region;
		public DateTime date;
		public int CompareTo(EpisodeStruct other)
		{
			return date.CompareTo(other.date);
		}
		
		public EpisodeStruct(string ProgramName, string Region, string Date)
		{
			programName = ProgramName;
			region = Region;
			date = DateTime.ParseExact(Date,"yyyy/MM/dd",null);
		}
	}

	public class VisPOC : MonoBehaviour {
	
		public static List<BroadcastStruct> getYearData(int year, List<BroadcastStruct> lst)
		{
			List<BroadcastStruct> newList = new List<BroadcastStruct>();
			var list = lst
					.OrderBy(r => r.SeriesName);

			foreach (BroadcastStruct b in lst) {
				foreach (EpisodeStruct c in b.episodeList){
					if (c.date.Year == year){
						newList.Add(b);
						break;
					}
				}
			}
			return newList.ToList();
			
		}

		public int i;
		int j;
		public float Radius;
		float camX;
		float camY;
		float CamHeight;
		public GameObject[] Nodes = new GameObject[40];
		float CamPos;
		public GameObject Ring;

		public int showPos;
		BroadcastStruct ep;
		public int episodeCount;
		public int epNum;
		public float[] distances = new float[764];
		int xvalue;
		public string test;
		int l;
		void Start () {
				CamHeight = Camera.main.transform.position.y;
				CamPos = -15;
			var csv = new CSVImport
			{
				CSVFile = Application.dataPath + "/ABC data 35000.csv"
			};
			
			csv.StartImport();
			
			var OrderTrimmedDataset = csv.lst
				.Where(c => c.episodeList.Count > 3)
						.OrderBy(c => c.SeriesName);


			for (int i = 0; i < 7; i++){
				var m = getYearData((1978 + i), OrderTrimmedDataset.ToList());
				l = 0;
				UnityEngine.Object spawner;
				spawner = GameObject.Instantiate(Ring, new Vector3(0,0,i * 15), Quaternion.Euler(0,0,0));
				if (i == 0){
					spawner.name = "Ring"; 
					GameObject.Find("Ring").transform.parent = GameObject.Find("Global").transform;
				}
				else {
					spawner.name = "Ring" + (i + 1).ToString ();
					GameObject.Find("Ring" + (i + 1).ToString()).transform.parent = GameObject.Find("Global").transform;
				}
				foreach (BroadcastStruct b in m)
				{

					float angle = 2 * Mathf.PI / m.Count();
					
					float x = Radius * Mathf.Sin(angle * l);
					float y = Radius * Mathf.Cos (angle * l);
					string name = "cube" + l.ToString();
					if (i > 0)
						name = "cube" + l.ToString() + "-" + (i + 1).ToString(); 
					spawner = GameObject.Instantiate(Nodes[i], new Vector3(x, y, i * 15), Quaternion.Euler(0,0,0));
					spawner.name = name;

					if (i == 0)
						GameObject.Find(name).transform.parent = GameObject.Find("Ring").transform;
					else
						GameObject.Find(name).transform.parent = GameObject.Find("Ring" + (i + 1).ToString()).transform;
					GameObject.Find(name).GetComponent<ShowInfo>().Showname = b.SeriesName;
					GameObject.Find(name).GetComponent<ShowInfo>().episodes = b.episodeList.Count();
					l++;
				}
			}
		}


		void Update() {
			if (Input.GetKeyDown(KeyCode.Escape))
					Application.Quit();
			
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

			if (Input.GetMouseButton (0)) {
				if (Input.GetAxis ("Mouse X") < 0) {
					camX -= 1f;
					if (camX < -55)
						camX = -55;
				}
				if (Input.GetAxis ("Mouse X") > 0) {
					camX += 1f;
					if (camX > 55)
						camX = 55;

				}
				if (Input.GetAxis ("Mouse Y") < 0) {
					camY += 1f;
					if (camY > 55)
						camY = 55;
				}
				if (Input.GetAxis ("Mouse Y") > 0) {

					camY -= 1f;
					if (camY < -55)
						camY = -55;

				}

			} else {
				if (camX != 0)
					camX = camX * 0.75f;
				if (camY != 0)
					camY = camY * 0.75f;
			}
			
			Camera.main.transform.rotation = Quaternion.Euler (31.5f + camY, camX, 0f);
			GameObject ring = GameObject.Find ("Global");
			if (xvalue > 0)
				ring.transform.rotation = Quaternion.Euler (0, 0, xvalue);
			else
				ring.transform.rotation = Quaternion.Euler (0, 0, 0);

			if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
				CamPos -= 2;
				if (CamPos < -15) {
					CamPos = -15;
				}
			} else if (Input.GetAxis ("Mouse ScrollWheel") > 0) {
				CamPos += 2;
				if (CamPos > 7*15){
					CamPos = 7*15;
				}
			}
			Camera.main.transform.position = new Vector3(0,CamHeight, CamPos);
		}


		void OnGUI () {
			GUI.Label(new Rect(45,45,500,250), "Controls:");
			GUI.Label(new Rect(45,65,500,250), "Right Mouse Button + Mouse Left/Right: Rotate visualisation");
			GUI.Label(new Rect(45,85,500,250), "+ Shift Key: Speed up Rotation");
			GUI.Label(new Rect(45,105,500,250), "Left Mouse Button + Mouse Left/Right/Up/Down: Freecam");
			GUI.Label(new Rect(45,125,500,250), "Scrollwheel: Move Forward/Backwards");
            GUI.Label(new Rect(45,145,500,250), "Esc: Exit");

			
		}
	}
}
