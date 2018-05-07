using System.Collections;
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
	public static string userid;

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
			form.AddField ("password", password.text);
			form.AddField ("email", email.text);
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
			StartCoroutine (WaitForRequest (data));

		}
		else
		{
			Debug.Log("WWW Request: " + data.text);
			if (data.text == "please fill all values") {//take care
			
			} else if (data.text == "wrong credentials") {//take care
				
			} else if (data.text[0] =='v') {
				Debug.Log ("data: " + data.text);
				string msg = data.text;
				int i = 0;
				while (msg [i] != ':') {
					i++;
				}
				//Debug.Log (msg [i]);
				i++;
				string temp="";
				while (msg [i] != '.') {
					temp = temp + msg [i];
					i++;
					if (i >= msg.Length) {
						break;
					}
				}
				Debug.Log (temp);
				userid = temp;
				SceneManager.LoadScene (4);
			}
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
