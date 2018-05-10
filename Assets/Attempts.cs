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
	public static string b_id;
	public static string cid=infoscript.cid;
	public static string uid=homescreen.userid;
	public static string Attemptstring;
	public static int A_number;
	private static string POSTAddUserURL = "https://autismdiagnosis.000webhostapp.com/attemptReturn.php";


	void Start () {
		Debug.Log ("button pressed!");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void info(){
		//infoscript.act = false;
		SceneManager.LoadScene("Child info");

	}

	public void res(){
		//infoscript.act = false;
		SceneManager.LoadScene("results");

	}

	public void testresults(){
		infoscript.act = false;
		SceneManager.LoadScene("Test results");

	}

	public void Aresults(){
		infoscript.act = true;
		Debug.Log (infoscript.act);
		SceneManager.LoadScene("Activities results");
		infoscript.act = true;
	}
	public void testid(Button t_button){
		Debug.Log ("button pressed! "+t_button.name);
		b_id = t_button.name;
		Debug.Log ("button "+infoscript.act);
		string temp=b_id+"Result.php";
		Debug.Log (temp);
		if (infoscript.act==true) {
			POSTAddUserURL = "https://autismdiagnosis.000webhostapp.com/" + "A" + t_button.name + "Result" + ".php";
		} else {
			POSTAddUserURL = "https://autismdiagnosis.000webhostapp.com/attemptReturn.php";
		}
		Debug.Log (POSTAddUserURL);
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
			StartCoroutine (WaitForRequest (data));

		}
		else
		{
			Debug.Log("WWW Request: " + data.text);
			Attemptstring = data.text;
			string[] temp= Attemptstring.Split (':');
			//Debug.Log (temp [1]);
			if (infoscript.act==true) {
				SceneManager.LoadScene ("Activities results display");

			} else {
				SceneManager.LoadScene ("Test results display");
				A_number = Convert.ToInt32 (temp [1]);

			}
				
		}
	}
		
}
