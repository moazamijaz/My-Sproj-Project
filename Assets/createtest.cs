                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using SimpleJSON;
using System.IO;

public class createtest : MonoBehaviour {
	public Text Question;
	public Transform myPanel;
	public ToggleGroup Tgroup;
	JSONNode questions;
	private int index;
	public List<Toggle> togglelist;
	public List<string> toggleResponses;
	public List<string> textResponses;
	public string webresponse;
	public string q_id; 
	public string test_id;
	public string q_type;
	public JSONNode vals;

	// Use this for initialization
	void Start () {
		readJSON ();
	}

	// Update is called once per frame
	void Update () {

	}

	public void readJSON()
	{
		string path=@"C:\Users\HP\Documents\GitHub\My-Sproj-Project\Assets\Resources\questions.json";
		string json = File.ReadAllText (path);
		var N = JSON.Parse (json);
		questions = N ["Questions"];
		index=0;
		loadnext ();
	}

	public void loadnext(){
		//record response from prev
		foreach (var item in Tgroup.ActiveToggles()) {
			var t = item.GetComponentInChildren<Text>();
			toggleResponses.Add (t.text);
			break;
		}
		foreach (var item in myPanel.GetComponentsInChildren<InputField>()) {
			textResponses.Add (item.text);
		}

		Debug.Log (toggleResponses.Count);
		//Delete them
		foreach (var item in myPanel.GetComponentsInChildren<Toggle>()) {
			Destroy (GameObject.Find(item.name));
		}
		foreach (var item in myPanel.GetComponentsInChildren<InputField>()) {
			Destroy (GameObject.Find(item.name));
		}

		if (index == questions.Count) {
			Debug.Log ("index too large");
			//load submit screen
			//childid, screeningtest id, qid, answer
			Question.text="Test Completed!";
			Destroy (GameObject.Find("Next"));
			Button T = (Button) Instantiate (Resources.Load ("Submit", typeof(Button)), myPanel);
			T.onClick.AddListener (() => submitbutton ());
			return;
		}

		Question.text = questions [index] ["Q_text"];
		q_id = questions [index] ["Q_id"]; 
		test_id = questions [index] ["Q_Stid"];
		q_type = questions [index] ["Q_type"];
		vals = questions [index] ["Q_values"];

		for (int i = 0; i < vals.Count; i++) {
			if (q_type == "radio") {
				//create radio 
				Vector3 temp=new Vector3(0,i*-30,0);
				Toggle T = (Toggle) Instantiate (Resources.Load ("myToggle", typeof(Toggle)), myPanel);
				T.transform.position = Tgroup.transform.position + temp;
				T.group = Tgroup;
				T.name = "radio"+i;
				togglelist.Add (T);
				if (i == 0) {
					T.isOn = true;
				}
				Text label=T.GetComponentInChildren<Text> ();
				string opt = vals [i];
				label.text = opt;

			} else {
				//create text
				string opt = vals [i];
				InputField T = (InputField) Instantiate(Resources.Load("Answerbox",typeof(InputField)), myPanel);
				T.transform.position = Tgroup.transform.position;
				Text[] list = T.GetComponentsInChildren<Text> ();
				for(int j=0; j<list.Length;j++){
					if (list [j].name == "Placeholder") {
						list [j].text = opt;
					}
				}
			}
		}
		//increment index
		index++;
	}

	public void submitbutton(){
		
		webresponse="[";
		for(int i=0; i<toggleResponses.Count; i++){
			Debug.Log ("Loop entered");
			string temp = @"{""Child_id"""+':' + @"""1"""+',' + @"""Q_id""" +':'+ '"'+(i + 1) +'"'+ ',' + @"""Q_Stid"""+':' +'"'+ test_id +'"'+ "," + @"""Q_response"""+':' +'"'+ toggleResponses [i] +'"'+ "}";
			Debug.Log (temp);
			webresponse = webresponse + temp;
			if (i != (toggleResponses.Count - 1)) {
				webresponse = webresponse + ",";
			}
		}
		webresponse = webresponse + "]";
		Debug.Log (webresponse);
	}
}
	