﻿                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using System.IO;


public class managerActivity10 : MonoBehaviour {

	private int count;
	private int score;
	public GameObject[] set;
	public static string cid=childrenscript.cid;
	public static string uid=LoginScript.userid;
	private static readonly string POSTAddUserURL = "https://autismdiagnosis.000webhostapp.com/A10.php";

	public Transform myPanel;
	public static string response = "[";

	// Use this for initialization
	void Start () {
		count = 0;
		score = 0;
		for (int i = 0; i < set.Length; i++) {
		
			set [i].SetActive (false);
		}
		set [0].SetActive (true);
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

	public void InCorrect(){
		Debug.Log ("InCorrect: "+set [count].name);
		
		response = response + "{" +
			@"""Child_id""" + ":" + '"' + cid + '"' + "," +
			@"""A10_screenEmotion""" + ":" + '"' + set[count].name + '"' + "," +
			@"""A10_correctResponse""" + ":" + '"' + "False" + '"' + "," +
			@"""A10_screenstarttime""" + ":" + @"""0""" + "," +
			@"""A10_answertime""" + ":" + @"""0""" + "}";
		count++;
		if (count < set.Length) {
			response = response + ",";
			Invoke ("nextQuestion", 1.0f);

		} else {
			//submit your score for activity 10
			response = response + "]";
			Debug.Log (response);
			WWW www;
			WWWForm form = new WWWForm ();
			form.AddField ("responseA10", response);//work on this
			form.AddField ("Child_id", cid);
			form.AddField ("Popinion", "NULL");
			form.AddField ("Eopinion", "NULL");
			form.AddField ("video", "NULL");

			www = new WWW (POSTAddUserURL, form);
			StartCoroutine (WaitForRequest (www));
			//and move the scene to menu
			Debug.Log("move now");
		}



	}

	public void Correct(){
		Debug.Log ("Correct: "+set [count].name);
		response = response + "{" +
			@"""Child_id""" + ":" + '"' + cid + '"' + "," +
			@"""A10_screenEmotion""" + ":" + '"' + set[count].name + '"' + "," +
			@"""A10_correctResponse""" + ":" + '"' + "True" + '"' + "," +
			@"""A10_screenstarttime""" + ":" + @"""0""" + "," +
			@"""A10_answertime""" + ":" + @"""0""" + "}";
		count++;
		score++;
		if (count < set.Length) {
			response = response + ",";
			Invoke ("nextQuestion", 1.0f);

		} else {
			//submit your score for activity 10
			response = response + "]";
			Debug.Log (response);
			WWW www;
			WWWForm form = new WWWForm ();
			form.AddField ("responseA10", response);//work on this
			form.AddField ("Child_id", cid);
			form.AddField ("Popinion", "NULL");
			form.AddField ("Eopinion", "NULL");
			form.AddField ("video", "NULL");

			www = new WWW (POSTAddUserURL, form);
			StartCoroutine (WaitForRequest (www));
			//and move the scene to menu
			Debug.Log("move now");
		}

	}

	void nextQuestion(){

		for (int i = 0; i < set.Length; i++) {

			set [i].SetActive (false);
		}

		set[count].SetActive (true);
	}

	public void nextscene(){
		SceneManager.LoadScene ("Activities");
	}

	IEnumerator WaitForRequest(WWW data)
	{
		for (int i = 0; i < set.Length; i++) {
			set [i].SetActive (false);
		}

		Text T = (Text)Instantiate (Resources.Load ("Game Over", typeof(Text)), myPanel);
		T.text = "Game Over" + '\n' + "Score: " + score;
		yield return data; // Wait until the download is done
		if (data.error != null)
		{
			Debug.Log("There was an error sending request: " + data.error);
			StartCoroutine (WaitForRequest (data));
		}
		else
		{
			Debug.Log("WWW Request: " + data.text);
			//testdata = data.text;
			Invoke ("nextscene", 1.5f);
		}
	}

}
