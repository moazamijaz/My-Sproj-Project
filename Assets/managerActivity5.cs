﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class managerActivity5 : MonoBehaviour {

	public GameObject[] animals;
	public GameObject instructions;
	private int count;
	// Use this for initialization
	void Start () {

		count = -1;
		instructions.SetActive(false);
		animals [0].SetActive (true);
		animals [1].SetActive (true);
		animals [2].SetActive (true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void next (){

		if (count < 3) {
			animals [0].SetActive (true);
			animals [1].SetActive (true);
			animals [2].SetActive (true);
			count++;
			animals [count].SetActive (false);
			instructions.SetActive(true);
		} else {

			Application.LoadLevel ("5-results");
		
		}
	
	}
}