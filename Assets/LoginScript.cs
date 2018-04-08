﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginScript : MonoBehaviour {

	public Text email;
	public InputField password;

	private static readonly string POSTAddUserURL = "https://autismdiagnosis.000webhostapp.com/login.php";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void login(){
		Debug.Log ("signin pressed "+email.text);
		Debug.Log ("signin pressed "+password.text);

		WWW www;
		WWWForm form = new WWWForm ();
		//validate input
		if (MyValidate_email (email.text) != false) {
			form.AddField ("username", email.text);
			form.AddField ("password", password.text);

			www = new WWW (POSTAddUserURL, form);
			StartCoroutine (WaitForRequest (www));

		} else {
			Debug.Log ("Invalid email");
		}
	}

	public void createAccount(){
		//load create account screen
		SceneManager.LoadScene(0);
	}
	//helper functions
	IEnumerator WaitForRequest(WWW data)
	{
		yield return data; // Wait until the download is done
		Debug.Log("entered function");
		if (data.error != null)
		{
			Debug.Log("There was an error sending request: " + data.error);
		}
		else
		{
			Debug.Log("WWW Request: " + data.text);
		}
	}

	private bool MyValidate_email(string strToValidate)
	{
		if (System.String.IsNullOrEmpty(strToValidate))
			return false;

		return Regex.IsMatch(strToValidate, @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
			@"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$");


	}

}