using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class submitResult : MonoBehaviour {

	private string review;

	public InputField input;
	// Use this for initialization
	void Start () {
		review="xax";
	}
	
	// Update is called once per frame
	void Update () {


	}

	public void submitReview(){

		review = input.text;

		//post this review in the database
		//only for activity 5
	}
}
