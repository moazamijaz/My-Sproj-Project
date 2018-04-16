using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class AddChild : MonoBehaviour {
	public Text cnic;
	public Text name;
	public Text age;
	public ToggleGroup gendergroup;
	public Text num_siblings;
	public Text medical_info;

	public static string g;
	private static readonly string POSTAddUserURL = "https://autismdiagnosis.000webhostapp.com/ChildInfo.php";


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GetValues(){
		foreach (var item in gendergroup.ActiveToggles()) {
			g = item.name;
			break;
		}

		Debug.Log ("signup pressed "+cnic.text);
		Debug.Log ("signup pressed "+name.text);
		Debug.Log ("signup pressed "+g);
		Debug.Log ("signup pressed "+age.text);
		Debug.Log ("signup pressed "+num_siblings.text);
		Debug.Log ("signup pressed "+medical_info.text);

		WWW www;
		WWWForm form = new WWWForm ();

		form.AddField ("id", cnic.text);
		form.AddField ("uid", LoginScript.userid); //add login id later
		form.AddField ("name", name.text);
		form.AddField ("gender", g);
		form.AddField ("age", age.text);
		form.AddField ("medicondition", medical_info.text);
		form.AddField ("sib", num_siblings.text);

		www = new WWW (POSTAddUserURL, form);
		StartCoroutine (WaitForRequest (www));
	}

	IEnumerator WaitForRequest(WWW data)
	{
		yield return data; // Wait until the download is done
		if (data.error != null)
		{
			Debug.Log("There was an error sending request: " + data.error);
		}
		else
		{
			Debug.Log("WWW Request: " + data.text);
			//find id in text

		}
	}
}
