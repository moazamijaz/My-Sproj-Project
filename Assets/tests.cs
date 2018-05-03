using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class tests : MonoBehaviour {

	public string b_id;
	public Button t_button;
	public static string testdata;
	private static readonly string POSTAddUserURL = "https://autismdiagnosis.000webhostapp.com/ScreeningTest.php";

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GoBack(){
		SceneManager.LoadScene (8);
	}

	public void testid(){
		b_id = t_button.name;
		WWW www;
		WWWForm form = new WWWForm ();
		form.AddField ("Stid", b_id);
		www = new WWW (POSTAddUserURL, form);
		StartCoroutine (WaitForRequest (www));
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
		}
	}

	public void mchats(){
		SceneManager.LoadScene (6);
	}

}
