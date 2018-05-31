using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using System.Linq;

public class childrenscript : MonoBehaviour {

	// Use this for initialization
	public Transform myPanel;
	public static string cid;
	public static string uid=LoginScript.userid;
	private static readonly string POSTAddUserURL = "https://autismdiagnosis.000webhostapp.com/returnAllChildern.php";
	void Start () {
		loadchildren ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Application.platform == RuntimePlatform.Android) {
			if (Input.GetKey (KeyCode.Escape)) {
				// Insert Code Here (I.E. Load Scene, Etc)
				// OR Application.Quit();
				SceneManager.LoadScene("home");
				return;
			}
		}
	}

	public void loadchildren(){
		WWW www;
		WWWForm form = new WWWForm ();
		form.AddField ("uid", uid); //LoginScript.userid
		www = new WWW (POSTAddUserURL, form);
		StartCoroutine (WaitForRequest (www));
	}

	public void buttonclick(object sender){
		Button button = (Button)sender;
		cid = button.name;
		Debug.Log ("button id: " + cid);
		SceneManager.LoadScene ("Child info");
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
			string[] children = data.text.Split(new[]{"<br>"},StringSplitOptions.None);
			children = children.Take (children.Count() - 1).ToArray();
			Debug.Log ("Children count: "+children.Count ());
			foreach (string child in children) {
				Button T = (Button)Instantiate (Resources.Load ("Child Button", typeof(Button)), myPanel);
				string[] Details = child.Split ('-');
				foreach (string detail in Details) {
					string[] kaam = detail.Split (':');
					string temp=kaam[0];
					if (temp.EndsWith("cid")) {
						T.name = kaam [1];
						cid = kaam [1];
					}
					else if (temp.EndsWith("name")) {
						GameObject name = GameObject.Find(cid);
						Text[] textlist = name.GetComponentsInChildren<Text> ();
						textlist [0].text = kaam [1];
						Debug.Log ("name: "+textlist[0].text);
					}
					else if (temp.EndsWith("age")) {
						GameObject name = GameObject.Find (cid);
						Text[] textlist = name.GetComponentsInChildren<Text> ();
						textlist [1].text = kaam [1]+" years";
						Debug.Log ("age: "+textlist[1].text);
					}
				}
				T.onClick.AddListener (()=>buttonclick(T));
			}
		}
	}

	public void GoBack(){
		SceneManager.LoadScene ("home");
	}
}
