using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class managerActivity9 : MonoBehaviour {


	public static int score;
	public static int count;
	public static string cid=infoscript.cid;
	public static string uid=LoginScript.userid;
	private static readonly string POSTAddUserURL = "https://autismdiagnosis.000webhostapp.com/A9.php";

	// Use this for initialization
	void Start () {
		score = 0;
		count = 0;
	}

	// Update is called once per frame
	void Update () {
		if (count == 5) {
		//submit the score here
			WWW www;
			WWWForm form = new WWWForm ();
			form.AddField ("responseA6", "NULL");//work on this
			form.AddField ("Child_id", cid);
			form.AddField ("Popinion", "NULL");
			form.AddField ("Eopinion", "NULL");
			form.AddField ("video", "NULL");

			www = new WWW (POSTAddUserURL, form);
			StartCoroutine (WaitForRequest (www));
			//and move the scene to menu
			Debug.Log("move now");
		
		}
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
