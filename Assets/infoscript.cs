using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using System.Linq;

public class infoscript : MonoBehaviour {

	// Use this for initialization
	public Transform myPanel;
	public static string cid = childrenscript.cid;
	private static readonly string POSTAddUserURL = "https://autismdiagnosis.000webhostapp.com/returnChildInfo.php";
	void Start () {
		loadchild ();
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void GoBack(){
		SceneManager.LoadScene (7);
	}

	public void loadTests(){
		SceneManager.LoadScene (9);
	}

	public void loadActivities(){
		SceneManager.LoadScene (10);
	}

	public void loadchild(){
		WWW www;
		WWWForm form = new WWWForm ();
		form.AddField ("id",cid); //LoginScript.userid
		www = new WWW (POSTAddUserURL, form);
		StartCoroutine (WaitForRequest (www));
	}

	IEnumerator WaitForRequest(WWW data)
	{
		yield return data; // Wait until the download is done
		if (data.error != null)
		{
			Debug.Log("There was an error sending request: " + data.error);
			//StartCoroutine (WaitForRequest (data));
		}
		else
		{
			Debug.Log("WWW Request: " + data.text);
			string[] details = data.text.Split('-');
			GameObject name = GameObject.Find ("Name");
			GameObject age = GameObject.Find ("Age");
			GameObject gender = GameObject.Find ("Gender");
			GameObject sib = GameObject.Find ("Siblings");
			GameObject med = GameObject.Find ("Medical");

			foreach (string d in details) {
				string[] temp = d.Split (':');
				if (temp[0].EndsWith("name")) {
					name.GetComponent<Text>().text = temp [1];
				}
				else if (temp[0].EndsWith("gender")) {
					gender.GetComponent<Text>().text = temp [1];
				}
				else if (temp[0].EndsWith("age")) {
					age.GetComponent<Text>().text = temp [1]+" years";
				}
				else if (temp[0].EndsWith("Siblings")) {
					sib.GetComponent<Text>().text = temp [1];
				}
				else if (temp[0].EndsWith("Medical")) {
					med.GetComponent<Text>().text = temp [1];
				}
			}

		}
	}
}
