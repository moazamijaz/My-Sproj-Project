using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
public class homescreen : MonoBehaviour {

	// Use this for initialization
	public static string userid=LoginScript.userid;
	public GameObject infoPanel;
	public Text info;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Application.platform == RuntimePlatform.Android) {
			if (Input.GetKey (KeyCode.Escape)) {
				// Insert Code Here (I.E. Load Scene, Etc)
				// OR Application.Quit();
				Application.Quit();
				return;
			}
		}
	}

	void close(){
		infoPanel.SetActive (false);
	}

	public void Notifications(){
		infoPanel.SetActive (true);
		info.text = "Coming Soon!";
		Invoke("close",2.0f);
	}

	public void AddChild(){
		SceneManager.LoadScene ("Add Child");
	}

	public void Children(){
		SceneManager.LoadScene ("View Children");
	}

	public void AccountSettings(){

	}

	public void Settings(){

	}
}
