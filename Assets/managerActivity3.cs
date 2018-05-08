﻿                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using System.IO;

public class managerActivity3 : MonoBehaviour {


	public static int score;
	public GameObject[] sets;
	public GameObject starter;
	public static string cid=infoscript.cid;
	public static string uid=LoginScript.userid;
	private static readonly string POSTAddUserURL = "https://autismdiagnosis.000webhostapp.com/A3.php";
	public Transform myPanel;


	private int curr, count;
	private string response = "{" + @"""Responses""" + ":[";

	// Use this for initialization
	void Start () {
		Debug.Log ("Response:"+response);
		score = 0;
		count = 0;

		sets [0].SetActive (false);
		sets [1].SetActive (false);
		sets [2].SetActive (false);
		sets [3].SetActive (false);
	
		
	}
	 

	public void incScore(){

		score++;
		response = response + "{" + @"""Child_id""" +":"+'"'+cid+'"' + "," +
			@"""A3_screenNumber"""+":" + count + "," +
			@"""A3_correctResponse"""+":" + @"""True""" + "," +
			@"""A3_screenstarttime"""+":" + @"""0""" + "," +
			@"""A3_answertime"""+":" + @"""4""" + "}";
		Debug.Log ("working");
		sets [curr].SetActive (false);
		Invoke ("GetRandomSet", 1.0f);
		//GetRandomSet ();
	
	}

	public void skip(){
		response = response + "{" + @"""Child_id""" + cid + "," +
			"A3_screenNumber" + count + "," +
			"A3_correctResponse:" + "False" + "," +
			"A3_screenstarttime:" + "0" + "," +
			"A3_answertime:" + "4" + "}";
		Debug.Log ("works fine");
		sets [curr].SetActive (false);
		Invoke ("GetRandomSet", 1.0f);

	}
	private void  GetRandomSet(){

		if (count < 20) {
			if (count != 0) {
				response = response + ",";
			}
			curr = UnityEngine.Random.Range (0, 4);
			sets[curr].SetActive (true);
			Debug.Log ("Your current Score is : " + score);
		} else {
			sets[curr].SetActive (false);
			//starter.SetActive (true);
			endGame();
		}

		count++;

	}

	public void startGame(){

		score = 0;

		sets [0].SetActive (false);
		sets [1].SetActive (false);
		sets [2].SetActive (false);
		sets [3].SetActive (false);

		GetRandomSet ();
		starter.SetActive (false);
	}

	public void endGame(){
		response = response + "]}";
		Debug.Log("Game Ended with score"+score);
		Debug.Log ("Response:" + response);
		Text T = (Text)Instantiate (Resources.Load ("Game Over", typeof(Text)), myPanel);
		WWW www;
		WWWForm form = new WWWForm ();
		form.AddField ("responseA3", response);//work on this
		form.AddField ("Child_id", cid);
		form.AddField ("Popinion", "NULL");
		form.AddField ("Eopinion", "NULL");
		form.AddField ("video", "NULL");

		www = new WWW (POSTAddUserURL, form);
		StartCoroutine (WaitForRequest (www));


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

/*
 * {"Responses":[
 * 	{"Child_id":cid,
 * 	"A3_screenNumber":count, 
 * 	"A3_correctResponse":"True", 
 * 	"A3_screenstarttime":"0",
 * 	"A3_answertime":"4"},
 * 
 *  {"Child_id":cid,
 * 	"A3_screenNumber":count, 
 * 	"A3_correctResponse":"True", 
 * 	"A3_screenstarttime":"0",
 * 	"A3_answertime":"4"}
 * 
 * 	{"Child_id":cid,
 * 	"A3_screenNumber":count, 
 * 	"A3_correctResponse":"True", 
 * 	"A3_screenstarttime":"0",
 * 	"A3_answertime":"4"}
 * ]} 
*/