﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using System.Linq;

public class tests : MonoBehaviour {

	public string b_id;
	public static string testdata;
	private static readonly string POSTAddUserURL = "https://autismdiagnosis.000webhostapp.com/ScreeningTest.php";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Application.platform == RuntimePlatform.Android) {
			if (Input.GetKey (KeyCode.Escape)) {
				// Insert Code Here (I.E. Load Scene, Etc)
				// OR Application.Quit();
				SceneManager.LoadScene("Child info");
				return;
			}
		}
	}



	public void GoBack(){
		SceneManager.LoadScene ("Child info");
	}
		

	public void testid(Button t_button){
		b_id = t_button.name;
		WWW www;
		WWWForm form = new WWWForm ();
		form.AddField ("St_id", b_id);
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
			testdata = data.text;
			SceneManager.LoadScene (6);
		}
	}
		
}
