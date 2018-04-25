using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using System.Linq;
public class activity5 : MonoBehaviour {

	// Use this for initialization
	public InputField observations;
	private static readonly string POSTAddUserURL = "https://autismdiagnosis.000webhostapp.com/ActivityResults.php";
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void GoBack(){
		SceneManager.LoadScene (10);//do something about this
	}

	public void startactivity(){
		SceneManager.LoadScene (11);
	}

	public void scene2(){
		SceneManager.LoadScene (13);
	}

	public void results(){
		SceneManager.LoadScene (14);
	}

	public void submit(){
		WWW www;
		WWWForm form = new WWWForm ();
		form.AddField ("aid", 5);
		form.AddField ("cid", infoscript.cid);
		form.AddField ("timetaken", 0);
		form.AddField ("parentObservation", observations.text);
		form.AddField ("expertOpinion", "Null");
		form.AddField ("video", "Null");

		www = new WWW (POSTAddUserURL, form);
		StartCoroutine (WaitForRequest (www));
	}

	IEnumerator WaitForRequest(WWW data)
	{
		yield return data; // Wait until the download is done
		if (data.error != null) {
			Debug.Log ("There was an error sending request: " + data.error);
			//StartCoroutine (WaitForRequest (data));
		} else {
			Debug.Log ("WWW Request: " + data.text);
			SceneManager.LoadScene (10);
		}
	}
}
