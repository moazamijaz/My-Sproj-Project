using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using System.Linq;

public class managerActivity5 : MonoBehaviour {

	public GameObject[] animals;
	public GameObject instructions;
	public ToggleGroup Tgroup;
	public Transform qPanel;
	public Transform myPanel;

	private int count;
	private float timer, timetaken;
	public static string cid = childrenscript.cid;
	public static string response = "[";
	// Use this for initialization
	void Start () {
		timer = 0;
		count = 0;
		timetaken = 0;
		instructions.SetActive(false);
		animals [0].SetActive (true);
		animals [1].SetActive (false);
		//animals [2].SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (Application.platform == RuntimePlatform.Android) {
			if (Input.GetKey (KeyCode.Escape)) {
				// Insert Code Here (I.E. Load Scene, Etc)
				// OR Application.Quit();
				SceneManager.LoadScene ("Activity instructions");

				return;
			}
		}
		
	}

	public void nextscene(){
		SceneManager.LoadScene ("Activities");
	}


	public void next (){
		Debug.Log ("count: "+count);
		if (Tgroup.ActiveToggles().Count()>0) {
			Debug.Log ("Active Toggles: " + Tgroup.ActiveToggles ().Count ());
			foreach (var item in Tgroup.ActiveToggles()) {
				Debug.Log("Response: "+item.name);
				string animal;
				if (count == 1) {
					animal = "dog";
				} else if (count == 4) {
					animal = "cat";
				} else {
					animal = "parrot";
				}
				response = response + "{" + @"""Child_id""" + ":" + '"' + cid + '"' + "," +
					@"""A5_screenAnimal""" + ":" + '"'+animal+'"' + "," +
					@"""A5_correctResponse""" + ":" + '"' + item.name + '"' + "," +
					@"""A5_screenstarttime""" + ":" + @"""0""" + "," +
					@"""A5_answertime""" + ":" + timetaken + "}";
				break;
			}
			count++;
		}
		GameObject.Destroy (GameObject.Find("Yes"));
		GameObject.Destroy (GameObject.Find("No"));
		GameObject.Destroy (GameObject.Find("qtext"));

		if (count < 8) {
			if (count == 0) {
				timer = 0;
				animals [0].SetActive (false);
				animals [2].SetActive (false);
				instructions.SetActive (true);
				count++;
			} else if (count == 1) {
				
				if (timetaken == 0)
					timetaken = timer;
				Debug.Log ("Time taken: " + timetaken + " Timer: " + timer);
				animals [0].SetActive (false);
				animals [1].SetActive (false);
				animals [2].SetActive (false);
				animals [3].SetActive (false);
				instructions.SetActive (false);

				//take input
				Vector3 temp = new Vector3 (0, 0, 0);
				Text qtext = (Text)Instantiate (Resources.Load ("Input Question", typeof(Text)), qPanel);
				qtext.name = "qtext";
				Toggle T1 = (Toggle)Instantiate (Resources.Load ("myToggle", typeof(Toggle)), myPanel);
				T1.transform.position = Tgroup.transform.position + temp;
				T1.group = Tgroup;
				T1.name = "Yes";
				temp = new Vector3 (200, 0, 0);
				Toggle T2 = (Toggle)Instantiate (Resources.Load ("myToggle", typeof(Toggle)), myPanel);
				T2.transform.position = Tgroup.transform.position + temp;
				T2.group = Tgroup;
				T2.name = "No";
				Text label = T1.GetComponentInChildren<Text> ();
				label.text = "Yes";
				label = T2.GetComponentInChildren<Text> ();
				label.text = "No";
					
			} else if (count == 2) {
				response = response + ',';
				animals [1].SetActive (true);
				animals [2].SetActive (true);
				animals [3].SetActive (true);
				instructions.SetActive (false);
				count++;
			} else if (count == 3) {
				timer = 0;
				timetaken = 0;
				animals [1].SetActive (false);
				animals [2].SetActive (false);
				instructions.SetActive (true);
				count++;
			} else if (count == 4) {
				
				if (timetaken == 0)
					timetaken = timer;
				Debug.Log ("Time taken: " + timetaken + " Timer: " + timer);
				animals [0].SetActive (false);
				animals [1].SetActive (false);
				animals [2].SetActive (false);
				animals [3].SetActive (false);
				instructions.SetActive (false);

				//take input
				Vector3 temp = new Vector3 (0, 0, 0);
				Text qtext = (Text)Instantiate (Resources.Load ("Input Question", typeof(Text)), qPanel);
				qtext.name = "qtext";
				Toggle T1 = (Toggle)Instantiate (Resources.Load ("myToggle", typeof(Toggle)), myPanel);
				T1.transform.position = Tgroup.transform.position + temp;
				T1.group = Tgroup;
				T1.name = "Yes";
				temp = new Vector3 (200, 0, 0);
				Toggle T2 = (Toggle)Instantiate (Resources.Load ("myToggle", typeof(Toggle)), myPanel);
				T2.transform.position = Tgroup.transform.position + temp;
				T2.group = Tgroup;
				T2.name = "No";
				Text label = T1.GetComponentInChildren<Text> ();
				label.text = "Yes";
				label = T2.GetComponentInChildren<Text> ();
				label.text = "No";
			} else if (count == 5) {
				response = response + ',';
				animals [4].SetActive (true);
				animals [5].SetActive (true);
				animals [3].SetActive (true);
				instructions.SetActive (false);
				count++;
			} else if (count == 6) {
				timer = 0;
				timetaken = 0;
				animals [5].SetActive (false);
				animals [4].SetActive (false);
				instructions.SetActive (true);
				count++;
			} else if (count == 7) {
				if (timetaken == 0)
					timetaken = timer;
				Debug.Log ("Time taken: " + timetaken + " Timer: " + timer);
				animals [0].SetActive (false);
				animals [1].SetActive (false);
				animals [2].SetActive (false);
				animals [3].SetActive (false);
				animals [4].SetActive (false);
				animals [5].SetActive (false);
				animals [3].SetActive (false);
				instructions.SetActive (false);

				//take input
				Vector3 temp = new Vector3 (0, 0, 0);
				Text qtext = (Text)Instantiate (Resources.Load ("Input Question", typeof(Text)), qPanel);
				qtext.name = "qtext";
				Toggle T1 = (Toggle)Instantiate (Resources.Load ("myToggle", typeof(Toggle)), myPanel);
				T1.transform.position = Tgroup.transform.position + temp;
				T1.group = Tgroup;
				T1.name = "Yes";
				temp = new Vector3 (200, 0, 0);
				Toggle T2 = (Toggle)Instantiate (Resources.Load ("myToggle", typeof(Toggle)), myPanel);
				T2.transform.position = Tgroup.transform.position + temp;
				T2.group = Tgroup;
				T2.name = "No";
				Text label = T1.GetComponentInChildren<Text> ();
				label.text = "Yes";
				label = T2.GetComponentInChildren<Text> ();
				label.text = "No";
			}


		} else {
			response = response + ']';
			Debug.Log ("Response: " + response);
			Application.LoadLevel ("5-results");
		
		}
	
	}
}
//Child_id, A5_screenAnimal, A5_correctResponse