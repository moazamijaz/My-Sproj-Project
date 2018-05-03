using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class managerActivity3 : MonoBehaviour {


	public static int score;
	public GameObject[] sets;
	public GameObject starter;
	public static string cid=infoscript.cid;
	public static string uid=LoginScript.userid;
	private static readonly string POSTAddUserURL = "https://autismdiagnosis.000webhostapp.com/returnAllChildern.php";


	private int curr, count;

	// Use this for initialization
	void Start () {
		score = 0;
		count = 0;

		sets [0].SetActive (false);
		sets [1].SetActive (false);
		sets [2].SetActive (false);
		sets [3].SetActive (false);
	
		
	}
	

	public void incScore(){

		score++;
		Debug.Log ("working");
		sets [curr].SetActive (false);
		Invoke ("GetRandomSet", 1.0f);
		//GetRandomSet ();
	
	}

	public void skip(){
		Debug.Log ("works fine");
		sets [curr].SetActive (false);
		Invoke ("GetRandomSet", 1.0f);

	}
	private void  GetRandomSet(){

		if (count < 20) {
			curr = Random.Range (0, 4);
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
		count = 0;

		sets [0].SetActive (false);
		sets [1].SetActive (false);
		sets [2].SetActive (false);
		sets [3].SetActive (false);

		GetRandomSet ();
		starter.SetActive (false);
	}

	public void endGame(){
		Debug.Log("Game Ended with score"+score);
	}


}
