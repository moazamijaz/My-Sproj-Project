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
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Notifications(){
		
	}

	public void AddChild(){
		SceneManager.LoadScene (5);
	}

	public void Children(){
		SceneManager.LoadScene (7);
	}

	public void AccountSettings(){

	}

	public void Settings(){

	}
}
