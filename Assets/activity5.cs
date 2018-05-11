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
	private static readonly string POSTAddUserURL = "https://autismdiagnosis.000webhostapp.com/A5.php";
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Application.platform == RuntimePlatform.Android) {
			if (Input.GetKey (KeyCode.Escape)) {
				// Insert Code Here (I.E. Load Scene, Etc)
				// OR Application.Quit();
				SceneManager.LoadScene ("Activity instructions");

				return;
			}
		}
	}
	public void GoBack(){
		SceneManager.LoadScene (10);//do something about this
	}


	public void submit(){
		WWW www;
		WWWForm form = new WWWForm ();
		form.AddField ("responseA5", managerActivity5.response);
		form.AddField ("Child_id", infoscript.cid);//Child_id, A5_screenAnimal, A5_correctResponse
		form.AddField ("Popinion", observations.text);
		form.AddField ("Eopinion", "Null");
		form.AddField ("video", "Null");

		www = new WWW (POSTAddUserURL, form);
		StartCoroutine (WaitForRequest (www));
	}

	IEnumerator WaitForRequest(WWW data)
	{
		yield return data; // Wait until the download is done
		if (data.error != null) {
			Debug.Log ("There was an error sending request: " + data.error);
			StartCoroutine (WaitForRequest (data));
		} else {
			Debug.Log ("WWW Request: " + data.text);
			SceneManager.LoadScene (10);
		}
	}
}
