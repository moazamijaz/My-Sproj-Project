﻿                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using System.IO;

public class managerActivity6 : MonoBehaviour {

	private int count;
	private int score;
	public GameObject[] set;
	public Transform myPanel;
	public static string cid=childrenscript.cid;
	public static string uid=LoginScript.userid;
	private static readonly string POSTAddUserURL = "https://autismdiagnosis.000webhostapp.com/A6b.php";

	private float timer, timetaken;
	private string animal="";
	public static string response = "[";
	// Use this for initialization
	void Start () {
		count = 0;
		score = 0;
		timer = 0;
		timetaken = 0;
		set [0].SetActive (true);
		set [1].SetActive (false);
		set [2].SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
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
		count++;
		Debug.Log ("Incorrect: " + count);
		if (timetaken==0)
			timetaken = timer;
		if (count < 3) {
			
			if (count == 1) {
				animal="pen";
			} else if (count == 2) {
				response=response+",";
				animal = "apple";
			} 
			Debug.Log ("InCorrect: "+count);

			response = response + "{" + @"""Child_id""" + ":" + '"' + cid + '"' + "," +
			@"""A6b_screenShape""" + ":" + '"' + animal + '"' + "," +
			@"""A6b_correctResponse""" + ":" + '"' + "False" + '"' + "," +
			@"""A6b_screenstarttime""" + ":" + @"""0""" + "," +
			@"""A6b_answertime""" + ":" + timetaken + "}";

			Invoke ("nextQuestion", 1.0f);

		} else {
			if (count == 3) {
				response=response+",";
				animal = "parrot";
			}
			response = response + "{" + @"""Child_id""" + ":" + '"' + cid + '"' + "," +
				@"""A6b_screenShape""" + ":" + '"' + animal + '"' + "," +
				@"""A6b_correctResponse""" + ":" + '"' + "True" + '"' + "," +
				@"""A6b_screenstarttime""" + ":" + @"""0""" + "," +
				@"""A6b_answertime""" + ":" + timetaken + "}";

			response=response+"]";
			//submit your score for activity 6
			WWW www;
			WWWForm form = new WWWForm ();
			form.AddField ("responseA6b", response);//work on this
			form.AddField ("Child_id", cid);
			form.AddField ("Popinion", "NULL");
			form.AddField ("Eopinion", "NULL");
			form.AddField ("video", "NULL");
			Debug.Log ("Response: " + response);

			www = new WWW (POSTAddUserURL, form);
			StartCoroutine (WaitForRequest (www));
			//and move the scene to menu
			Debug.Log("move now");
			set [0].SetActive (false);
			set [1].SetActive (false);
			set [2].SetActive (false);

			Text T = (Text)Instantiate (Resources.Load ("Game Over", typeof(Text)), myPanel);
			T.text = "Game Over" + '\n' + "Score: " + score;
		}
	}


	public void Correct(){
		count++;
		score++;
		if (timetaken==0)
			timetaken = timer;
		if (count < 3) {
			
			if (count == 1) {
				animal="pen";
				response = response + "{" + @"""Child_id""" + ":" + '"' + cid + '"' + "," +
					@"""A6b_screenShape""" + ":" + '"' + animal + '"' + "," +
					@"""A6b_correctResponse""" + ":" + '"' + "True" + '"' + "," +
					@"""A6b_screenstarttime""" + ":" + @"""0""" + "," +
					@"""A6b_answertime""" + ":" + timetaken + "}";

				Invoke ("nextQuestion", 1.0f);
			} else if (count == 2) {
				response=response+",";
				animal = "apple";
				response = response + "{" + @"""Child_id""" + ":" + '"' + cid + '"' + "," +
					@"""A6b_screenShape""" + ":" + '"' + animal + '"' + "," +
					@"""A6b_correctResponse""" + ":" + '"' + "True" + '"' + "," +
					@"""A6b_screenstarttime""" + ":" + @"""0""" + "," +
					@"""A6b_answertime""" + ":" + timetaken + "}";

				Invoke ("nextQuestion", 1.0f);
			} else if (count == 3) {
				response=response+",";
				animal = "parrot";
			}
			Debug.Log ("Correct: "+count);


		} else {
			//submit your score for activity 6
			if (count == 3) {
				response=response+",";
				animal = "parrot";
				response = response + "{" + @"""Child_id""" + ":" + '"' + cid + '"' + "," +
				@"""A6b_screenShape""" + ":" + '"' + animal + '"' + "," +
				@"""A6b_correctResponse""" + ":" + '"' + "True" + '"' + "," +
				@"""A6b_screenstarttime""" + ":" + @"""0""" + "," +
				@"""A6b_answertime""" + ":" + timetaken + "}";
			}
			Debug.Log ("Correct: "+count);
			
			response=response+"]";

			Debug.Log ("Response: " + response);

			WWW www;
			WWWForm form = new WWWForm ();
			form.AddField ("responseA6b", response);//work on this
			form.AddField ("Child_id", cid);
			form.AddField ("Popinion", "NULL");
			form.AddField ("Eopinion", "NULL");
			form.AddField ("video", "NULL");

			www = new WWW (POSTAddUserURL, form);
			StartCoroutine (WaitForRequest (www));
			//and move the scene to menu
			Debug.Log("move now");
			set [0].SetActive (false);
			set [1].SetActive (false);
			set [2].SetActive (false);
			Text T = (Text)Instantiate (Resources.Load ("Game Over", typeof(Text)), myPanel);
			T.text = "Game Over" + '\n' + "Score: " + score;

		}

	}

	void nextQuestion(){
		timer = 0;
		timetaken = 0;
		set [0].SetActive (false);
		set [1].SetActive (false);
		set [2].SetActive (false);
		set[count].SetActive (true);
	}

	public void nextscene(){
		SceneManager.LoadScene ("Activities");
	}

	IEnumerator WaitForRequest(WWW data)
	{
		yield return data; // Wait until the download is done
		if (data.error != null)
		{
			Debug.Log("There was an error sending request: " + data.error);
			Invoke ("dummy", 1.0f);
			StartCoroutine (WaitForRequest (data));
		}
		else
		{
			Debug.Log("WWW Request: " + data.text);
			//testdata = data.text;
			Invoke ("nextscene", 1.5f);
		}
	}

	public void dummy(){
		Debug.Log ("Time Wasted");
	}

}
