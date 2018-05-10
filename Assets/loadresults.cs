using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using System.Linq;
public class loadresults : MonoBehaviour {

	// Use this for initialization
	private int length=Attempts.A_number;
	private int A_id;
	public static string cid = Attempts.cid;
	public Transform myPanel;
	private static readonly string POSTAddUserURL = "https://autismdiagnosis.000webhostapp.com/testResults.php";

	void Start () {
		if (length < 1) {
			Text T = (Text)Instantiate (Resources.Load ("Answers", typeof(Text)), myPanel);
			T.text = "No results found.";
		} else {
			for (int i = 1; i <= length; i++) {
				A_id = i;
				WWW www;
				WWWForm form = new WWWForm ();
				form.AddField ("childId",cid);
				form.AddField ("Stid", Attempts.b_id);
				form.AddField ("attempt", i);
				www = new WWW (POSTAddUserURL, form);
				StartCoroutine (WaitForRequest (www));
				
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void testresults(){
		//infoscript.act = false;
		SceneManager.LoadScene("Test results");

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
			Text T1 = (Text)Instantiate (Resources.Load ("Attempt number", typeof(Text)), myPanel);
			T1.text = "Attempt " + A_id;
			Text T2 = (Text)Instantiate (Resources.Load ("Answers", typeof(Text)), myPanel);
			T2.text ="";
			string[] rows = data.text.Split (new[]{"<br>"},StringSplitOptions.None);
			foreach (string row in rows) {
				T2.text = T2.text + '\n' + row;
			}
		}
	}

}
