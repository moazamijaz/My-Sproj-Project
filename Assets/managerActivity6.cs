using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class managerActivity6 : MonoBehaviour {

	private int count;
	private int score;
	public GameObject[] set;

	// Use this for initialization
	void Start () {
		count = 0;
		score = 0;
		set [0].SetActive (true);
		set [1].SetActive (false);
		set [2].SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {


	}

	public void InCorrect(){
		count++;

		if (count < 3) {

			Invoke ("nextQuestion", 1.0f);

		} else {
			//submit your score for activity 6
			//and move the scene to menu
			Debug.Log("move now");
		}



	}

	public void Correct(){
		count++;
		score++;

		if (count < 3) {

			Invoke ("nextQuestion", 1.0f);

		} else {
			//submit your score for activity 6
			//and move the scene to menu
			Debug.Log("move now");
		}

	}

	void nextQuestion(){

		set [0].SetActive (false);
		set [1].SetActive (false);
		set [2].SetActive (false);

		set[count].SetActive (true);
	}
}
