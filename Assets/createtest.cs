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
	//private int curr;
	public List<string> toggleResponses;
	public List<string> textResponses;
	public string webresponse;
	public string q_id; 
	public string test_id;
	public string q_type;
	public JSONNode vals;
	public static string cid=infoscript.cid;
	private static readonly string POSTAddUserURL = "https://autismdiagnosis.000webhostapp.com/response.php";

	public GameObject prev,next;
	float sValue, jump, timer;
	public GameObject S;
	public Slider slider;
	// Use this for initialization
	void Start () {
		readJSON ();
	}

	// Update is called once per frame
	void Update () {
		while (slider.value < sValue && sValue<=1.0f) {
			slider.value +=Time.deltaTime*0.005f;
		}
	}

	public void GoBack(){
		SceneManager.LoadScene (9);
	}

	public void readJSON()
	{
		//string path=@"C:\Users\HP\Documents\GitHub\My-Sproj-Project\Assets\Resources\questions.json";
		//string json = File.ReadAllText (path);
		string json=tests.testdata;
		S.SetActive (true);
		slider.value = 0f;
		sValue = 0;
		Debug.Log (json);
		var N = JSON.Parse (json);
		questions = N ["Questions"];
		jump = 1.0f / questions.Count;
		Debug.Log ("jump" + jump);
		index=-1;
		loadnext ();
	}

	public void loadnext(){
		//record response from prev
		sValue=sValue+jump;
		timer = 0;
		index++;
		if (index <= 0) {
			prev.SetActive (false);
		} else {
			prev.SetActive (true);
		}
		foreach (var item in Tgroup.ActiveToggles()) {
			var t = item.GetComponentInChildren<Text>();
			toggleResponses.Add (t.text);
			break;
		}
		foreach (var item in myPanel.GetComponentsInChildren<InputField>()) {
			textResponses.Add (item.text);
		}

		Debug.Log ("index: "+ index);
		//Delete them
		foreach (var item in myPanel.GetComponentsInChildren<Toggle>()) {
			Destroy (GameObject.Find(item.name));
		}
		foreach (var item in myPanel.GetComponentsInChildren<InputField>()) {
			Destroy (GameObject.Find(item.name));
		}

		if (index == questions.Count) {
			Debug.Log ("index too large");
			Question.text="Test Completed!";
			//Destroy (GameObject.Find("Next"));
			S.SetActive (false);

			next.SetActive(false);
			Button T = (Button) Instantiate (Resources.Load ("Submit", typeof(Button)), myPanel);
			T.name="Submit";
			T.transform.position = next.transform.position;
			T.onClick.AddListener (() => submitbutton ());
			return;
		}

		Question.text = questions [index] ["Q_text"];
		q_id = questions [index] ["Q_id"]; 
		test_id = questions [index] ["Q_Stid"];
		q_type = questions [index] ["Q_type"];
		vals = questions [index] ["Q_values"];
		//Debug.Log ("Nae Waray"+ vals.Count);
		for (int i = 0; i < vals.Count; i++) {
			Debug.Log ("Warr gye");
			if (q_type == "radio") {
				//create radio 
				Vector3 temp=new Vector3(i*200,0,0);
				Toggle T = (Toggle) Instantiate (Resources.Load ("myToggle", typeof(Toggle)), myPanel);
				T.transform.position = Tgroup.transform.position + temp;
				T.group = Tgroup;
				T.name = "radio"+i;
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
		//curr=index;
		//index++;
	}

	public void submitbutton(){
		
		webresponse="[";
		for(int i=0; i<toggleResponses.Count; i++){
			//Debug.Log ("Loop entered");
			string temp = @"{""Child_id"""+':' + cid +',' 
				+ @"""Q_id""" +':'+ '"'+(i + 1) +'"'+ ',' 
				+ @"""Q_Stid"""+':' +'"'+ test_id +'"'+ "," 
				+ @"""Q_response"""+':' +'"'+ toggleResponses [i] +'"'+ "}";
			//Debug.Log (temp);
			webresponse = webresponse + temp;
			if (i != (toggleResponses.Count - 1)) {
				webresponse = webresponse + ",";
			}
		}
		webresponse = webresponse + "]";
		Debug.Log (webresponse);
		WWW www;
		WWWForm form = new WWWForm ();
		form.AddField ("response", webresponse);
		form.AddField ("Child_id", cid);
		form.AddField ("Q_Stid", test_id);
		www = new WWW (POSTAddUserURL, form);
		StartCoroutine (WaitForRequest (www));
	}


	IEnumerator WaitForRequest(WWW data)
	{
		yield return data; // Wait until the download is done
		if (data.error != null)
		{
			Debug.Log("There was an error sending request: " + data.error);
			StartCoroutine (WaitForRequest (data));

		}
		else
		{
			Debug.Log("WWW Request: " + data.text);
			SceneManager.LoadScene (9);

		}
	}

	public void loadprev(){
		S.SetActive (true);
		sValue=sValue-jump;
		slider.value = sValue;
		//remove
		if (index >= questions.Count) {
			Debug.Log ("index too large");
			Question.text="Test Completed!";
			Destroy (GameObject.Find("Submit"));
			next.SetActive(true);
		}
			//curr--;
		index--;
		Debug.Log ("index prev: "+index);
		if (index <= 0) {
			prev.SetActive (false);
		} else {
			prev.SetActive (true);
		}
			
			foreach (var item in myPanel.GetComponentsInChildren<Toggle>()) {
				Destroy (GameObject.Find (item.name));
			}
			foreach (var item in myPanel.GetComponentsInChildren<InputField>()) {
				Destroy (GameObject.Find (item.name));
			}

			toggleResponses.RemoveAt (toggleResponses.Count - 1);
		Question.text = questions [index] ["Q_text"];
			//load
		q_id = questions [index] ["Q_id"];
		test_id = questions [index] ["Q_Stid"];
		q_type = questions [index] ["Q_type"];
		vals = questions [index] ["Q_values"];
			for (int i = 0; i < vals.Count; i++) {
				if (q_type == "radio") {
					//create radio 
				Vector3 temp = new Vector3 (i*150, 0, 0);
					Toggle T = (Toggle)Instantiate (Resources.Load ("myToggle", typeof(Toggle)), myPanel);
					T.transform.position = Tgroup.transform.position + temp;
					T.group = Tgroup;
					T.name = "radio" + i;
					if (i == 0) {
						T.isOn = true;
					}
					Text label = T.GetComponentInChildren<Text> ();
					string opt = vals [i];
					label.text = opt;

				} else {
					//create text
					string opt = vals [i];
					InputField T = (InputField)Instantiate (Resources.Load ("Answerbox", typeof(InputField)), myPanel);
					T.transform.position = Tgroup.transform.position;
					Text[] list = T.GetComponentsInChildren<Text> ();
					for (int j = 0; j < list.Length; j++) {
						if (list [j].name == "Placeholder") {
							list [j].text = opt;
						}
					}
				}

			}
		}
			
}
	