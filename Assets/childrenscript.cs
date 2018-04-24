using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class childrenscript : MonoBehaviour {

	// Use this for initialization
	public Transform myPanel;
	private static readonly string POSTAddUserURL = "https://autismdiagnosis.000webhostapp.com/returnAllChildern.php";
	void Start () {
		loadchildren ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void loadchildren(){
		WWW www;
		WWWForm form = new WWWForm ();
		form.AddField ("uid", 37); //LoginScript.userid
		www = new WWW (POSTAddUserURL, form);
		StartCoroutine (WaitForRequest (www));
		/*
		for (int i = 0; i < 50; i++) {
			Button T = (Button)Instantiate (Resources.Load ("Child Button", typeof(Button)), myPanel);
		}
		*/

	}

	IEnumerator WaitForRequest(WWW data)
	{
		yield return data; // Wait until the download is done
		if (data.error != null)
		{
			Debug.Log("There was an error sending request: " + data.error);
		}
		else
		{
			Debug.Log("WWW Request: " + data.text);
			string[] children = data.text.Split(new[]{"<br>"},StringSplitOptions.None);
			foreach (string child in children) {
				string[] Details = child.Split ('-');
				foreach (string detail in Details) {
					string[] kaam = detail.Split (':');
					Debug.Log (kaam [1]);
				}
			}

		}
	}
}
