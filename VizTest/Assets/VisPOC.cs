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
			/*foreach (BroadcastStruct b in newList)
			{
				for (int i = 0;i<b.episodeList.Count;i++)
				{
					if ((b.episodeList[i].date.Year <= year))
					{
						b.episodeList.RemoveRange(i,b.episodeList.Count()-i);
					}
				}
			}*/
			return newList.ToList();
			
		}

	public int i;
	int j;

		float camX;
		float camY;
	public GameObject cube;
	public GameObject cube2;
	public GameObject cube3;
		public GameObject cube4;
		public GameObject cube5;
		float CamPos;

	public int showPos;
	BroadcastStruct ep;
	public int episodeCount;
	public int epNum;
	public float[] distances = new float[764];
	int xvalue;
	public string test;
		int l;
	void Start () {

		var csv = new CSVImport
		{
			CSVFile = "./Assets/ABC data 50000.csv"
		};
		
		csv.StartImport();
		
		/*foreach (BroadcastStruct b in csv.lst)
		{
			b.order();
		}*/
		
		var OrderTrimmedDataset = csv.lst
			.Where(c => c.episodeList.Count > 3)
					.OrderBy(c => c.SeriesName)//.episodeList[0].date)
				;
		//Console.WriteLine("{0}",OrderTrimmedDataset.Count());
		

		var m = getYearData(1988, OrderTrimmedDataset.ToList());
		foreach (BroadcastStruct b in m)
		{
				UnityEngine.Object spawner;
				float angle = 2 * Mathf.PI / m.Count();
				
				float x = 65 * Mathf.Sin(angle * l);
				float y = 65 * Mathf.Cos (angle * l);
				string name = "cube" + l.ToString();
				spawner = GameObject.Instantiate(cube, new Vector3(x, y, 0), Quaternion.Euler(0,0,0));
				spawner.name = name;
				
				GameObject.Find(name).transform.parent = GameObject.Find("Ring").transform;
				GameObject.Find(name).GetComponent<ShowInfo>().Showname = b.SeriesName;
				GameObject.Find(name).GetComponent<ShowInfo>().episodes = b.episodeList.Count();
				l++;
		}
			l = 0;
			m = getYearData(1989, OrderTrimmedDataset.ToList());
			foreach (BroadcastStruct b in m)
			{
				UnityEngine.Object spawner;
				float angle = 2 * Mathf.PI / m.Count();
				
				float x = 65 * Mathf.Sin(angle * l);
				float y = 65 * Mathf.Cos (angle * l);
				string name = "cube" + l.ToString() + "-2";
				spawner = GameObject.Instantiate(cube2, new Vector3(x, y, 15), Quaternion.Euler(0,0,0));
				spawner.name = name;
				
				GameObject.Find(name).transform.parent = GameObject.Find("Ring2").transform;

				GameObject.Find(name).GetComponent<ShowInfo>().Showname = b.SeriesName;
				GameObject.Find(name).GetComponent<ShowInfo>().episodes = b.episodeList.Count();

				l++;
			}

			l = 0;
			m = getYearData(1990, OrderTrimmedDataset.ToList());
			foreach (BroadcastStruct b in m)
			{
				UnityEngine.Object spawner;
				float angle = 2 * Mathf.PI / m.Count();
				
				float x = 65 * Mathf.Sin(angle * l);
				float y = 65 * Mathf.Cos (angle * l);
				string name = "cube" + l.ToString() + "-3";
				spawner = GameObject.Instantiate(cube3, new Vector3(x, y, 30), Quaternion.Euler(0,0,0));
				spawner.name = name;
				
				GameObject.Find(name).transform.parent = GameObject.Find("Ring3").transform;
				
				GameObject.Find(name).GetComponent<ShowInfo>().Showname = b.SeriesName;
				GameObject.Find(name).GetComponent<ShowInfo>().episodes = b.episodeList.Count();
				
				l++;
			}

			l = 0;
			m = getYearData(1991, OrderTrimmedDataset.ToList());
			foreach (BroadcastStruct b in m)
			{
				UnityEngine.Object spawner;
				float angle = 2 * Mathf.PI / m.Count();
				
				float x = 65 * Mathf.Sin(angle * l);
				float y = 65 * Mathf.Cos (angle * l);
				string name = "cube" + l.ToString() + "-4";
				spawner = GameObject.Instantiate(cube4, new Vector3(x, y, 45), Quaternion.Euler(0,0,0));
				spawner.name = name;
				
				GameObject.Find(name).transform.parent = GameObject.Find("Ring4").transform;
				
				GameObject.Find(name).GetComponent<ShowInfo>().Showname = b.SeriesName;
				GameObject.Find(name).GetComponent<ShowInfo>().episodes = b.episodeList.Count();
				
				l++;
			}

			l = 0;
			m = getYearData(1992, OrderTrimmedDataset.ToList());
			foreach (BroadcastStruct b in m)
			{
				UnityEngine.Object spawner;
				float angle = 2 * Mathf.PI / m.Count();
				
				float x = 65 * Mathf.Sin(angle * l);
				float y = 65 * Mathf.Cos (angle * l);
				string name = "cube" + l.ToString() + "-5";
				spawner = GameObject.Instantiate(cube5, new Vector3(x, y, 60), Quaternion.Euler(0,0,0));
				spawner.name = name;
				
				GameObject.Find(name).transform.parent = GameObject.Find("Ring5").transform;
				
				GameObject.Find(name).GetComponent<ShowInfo>().Showname = b.SeriesName;
				GameObject.Find(name).GetComponent<ShowInfo>().episodes = b.episodeList.Count();
				
				l++;
			}

	}

	void Update() {
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
			if (CamPos > 3*15){
				CamPos = 3*15;
			}
		}

		Camera.main.transform.position = new Vector3(0,69, CamPos);
		
		
	}
}
}
