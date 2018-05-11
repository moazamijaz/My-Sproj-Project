using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class managerActivity9 : MonoBehaviour {


	public static int score;
	public static int count;
	public static string cid=childrenscript.cid;
	public static string uid=LoginScript.userid;
	private static readonly string POSTAddUserURL = "https://autismdiagnosis.000webhostapp.com/A9.php";

	public Transform myPanel;
	public static string response = "[";

	// Use this for initialization
	void Start () {
		score = 0;
		count = 0;
	}

	// Update is called once per frame
	void Update () {
		if (count == 5) {
			//submit the score here
			Debug.Log ("Response: " + response);
			WWW www;
			WWWForm form = new WWWForm ();
			form.AddField ("responseA9", response);//work on this
			form.AddField ("Child_id", cid);
			form.AddField ("Popinion", "NULL");
			form.AddField ("Eopinion", "NULL");
			form.AddField ("video", "NULL");

			www = new WWW (POSTAddUserURL, form);
			StartCoroutine (WaitForRequest (www));
			count = 0;
			//and move the scene to menu
			Debug.Log ("move now");
		
		}

		if (Application.platform == RuntimePlatform.Android) {
			if (Input.GetKey (KeyCode.Escape)) {
				// Insert Code Here (I.E. Load Scene, Etc)
				// OR Application.Quit();
				SceneManager.LoadScene ("Activity instructions");

				return;
			}
		}
	}

	IEnumerator WaitForRequest(WWW data)
	{
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
			Invoke ("GoBack", 1.5f);
		}
	}

	public void GoBack(){
		SceneManager.LoadScene ("Activities");
	}
}
