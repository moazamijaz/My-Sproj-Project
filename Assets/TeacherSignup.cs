using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Text.RegularExpressions;

public class TeacherSignup : MonoBehaviour {
	public string userid;
	public Text education;
	public Text experience;
	public Text expAutism;
	public Text numChild;
	public Text schoolName;
	public Text occupation;
	public Text income;
	private static readonly string POSTAddUserURL = "https://autismdiagnosis.000webhostapp.com/TeacherInfo.php";
	private static readonly string POSTAddUserURL2 = "https://autismdiagnosis.000webhostapp.com/ParentsInfo.php";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GetValues(){
		userid = SignUpController.myid;
		Debug.Log (SignUpController.myid);
		Debug.Log ("signup pressed "+education.text);
		Debug.Log ("signup pressed "+experience.text);
		Debug.Log ("signup pressed "+expAutism.text);
		Debug.Log ("signup pressed "+numChild.text);
		Debug.Log ("signup pressed "+schoolName.text);


		//create ww
		WWW www;
		WWWForm form = new WWWForm ();
		form.AddField ("id", userid);
		form.AddField ("education", education.text);
		form.AddField ("experience", experience.text);
		form.AddField ("expAutism", expAutism.text);
		form.AddField ("numChild", numChild.text);
		form.AddField ("schoolName", schoolName.text);
		www = new WWW (POSTAddUserURL, form);
		StartCoroutine (WaitForRequest (www));
	}

	public void ParentSignup()
	{
		userid = SignUpController.myid;
		Debug.Log (SignUpController.myid);
		Debug.Log (SignUpController.g);
		Debug.Log ("signup pressed "+education.text);
		Debug.Log ("signup pressed "+occupation.text);
		Debug.Log ("signup pressed "+income.text);

		WWW www;
		WWWForm form = new WWWForm ();
		form.AddField ("id", userid);
		form.AddField ("gender", SignUpController.g);
		form.AddField ("education", education.text);
		form.AddField ("occupation", occupation.text);
		form.AddField ("income", income.text);
		www = new WWW (POSTAddUserURL2, form);
		StartCoroutine (WaitForRequest (www));

	}

	IEnumerator WaitForRequest(WWW data)
	{
		yield return data; // Wait until the download is done
		Debug.Log("entered function");
		if (data.error != null)
		{
			Debug.Log("There was an error sending request: " + data.error);
			StartCoroutine (WaitForRequest (data));

		}
		else
		{
			Debug.Log("WWW Request: " + data.text);
		}
	}
}
