using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using System.Linq;

public class Attempts : MonoBehaviour {

	// Use this for initialization
	private string b_id;
	public static string cid=infoscript.cid;
	public static string uid=homescreen.userid;
	public static string Attemptnumber;
	private static readonly string POSTAddUserURL = "https://autismdiagnosis.000webhostapp.com/attemptReturn.php";
	void Start () {
		Debug.Log ("button pressed!");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void testresults(){
		SceneManager.LoadScene("Test results");
	}

	public void testid(Button t_button){
		Debug.Log ("button pressed!");
		b_id = t_button.name;
		WWW www;
		WWWForm form = new WWWForm ();
		form.AddField ("childId",cid);
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
			Attemptnumber = data.text;
			string[] temp= Attemptnumber.Split (':');
			Debug.Log (temp [1]);
			//SceneManager.LoadScene ("");
		}
	}

	public void test(){
		Debug.Log ("tarara");
	}
}
