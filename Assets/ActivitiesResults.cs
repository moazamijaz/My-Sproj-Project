using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using System.Linq;
public class ActivitiesResults : MonoBehaviour {

	// Use this for initialization

	public static string cid = Attempts.cid;
	public Transform myPanel;
	private string[] results;

	void Start () {
		//Text T = (Text)Instantiate (Resources.Load ("Answers", typeof(Text)), myPanel);
		results = Attempts.Attemptstring.Split(new[]{"<br>"},StringSplitOptions.None);
		foreach (string row in results) {
			//T.text = T.text + '\n' + row;
			Text T = (Text)Instantiate (Resources.Load ("Answers", typeof(Text)), myPanel);
			T.text = row;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Aresults(){
		//infoscript.act = false;
		SceneManager.LoadScene("Activities results");

	}
}
