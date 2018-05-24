using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class SignUpController : MonoBehaviour {
	public Text name;
	public Text email;
	public InputField password;
	public Text age;
	public Text telephone;
	public ToggleGroup gendergroup;
	public ToggleGroup rolegroup;
	public GameObject errorPanel;
	public Text infoText;
	public GameObject errorText;

	public static string role;
	public static string g;
	private static readonly string POSTAddUserURL = "https://autismdiagnosis.000webhostapp.com/signup.php";
	public static string myid;
	public static string userEmail;
	public static string userPass, userName, userAge, userTel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public void goBack () {
		SceneManager.LoadScene ("login");
	}

	void Update () {
		if (Application.platform == RuntimePlatform.Android) {
			if (Input.GetKey (KeyCode.Escape)) {
				// Insert Code Here (I.E. Load Scene, Etc)
				// OR Application.Quit();
				goBack();

				return;
			}
		}
	}

	public void getValues()
	{
		foreach (var item in rolegroup.ActiveToggles()) {
			role = item.name;
			break;
		}

		foreach (var item in rolegroup.ActiveToggles()) {
			g = item.name;
			break;
		}
		userName = name.text;
		userEmail = email.text;
		userPass = password.text;
		userAge = age.text;
		userTel = telephone.text;
		mySubmitForm ();
	}

	IEnumerator WaitForRequest(WWW data)
	{
		yield return data; // Wait until the download is done
		if (data.error != null)
		{
			Debug.Log("There was an error sending request: " + data.error);
			//StartCoroutine (WaitForRequest (data));
			
			errorPanel.SetActive (true);
			errorText.SetActive (true);
			infoText.text="There was an error sending request: " + data.error;
			Invoke ("close", 3.0f);

		}
		else
		{
			Debug.Log("WWW Request: " + data.text);
			//find id in text
			//myid = Get_id (data);
			//Debug.Log (myid);
			//change to next screen


			Debug.Log (role);
			if (data.text.EndsWith ("sent")) {
				errorPanel.SetActive (true);
				errorText.SetActive (false);
				infoText.text=data.text;
				infoText.text = infoText.text + ".Please check your email to verify and press "+'"'+"Proceed"+'"'+" when done.";
				Invoke ("close", 3.0f);
				SceneManager.LoadScene ("verifyScreen");

				
			} else if(data.text.EndsWith ("Please verify email")){
				errorPanel.SetActive (true);
				errorText.SetActive (false);

				infoText.text = "Verification email sent. Please check email to verify and press "+'"'+"Proceed"+'"'+" when done.";
				Invoke ("close", 3.0f);
				
			} else if(data.text.EndsWith ("confirmed")){
				errorPanel.SetActive (true);
				errorText.SetActive (true);
				infoText.text= "User already exists";
				Invoke ("close", 3.0f);
			} else if(data.text.StartsWith("id")){
				string[] temp = data.text.Split (':');
				myid = temp [1];
				if (role == "Teacher") {
					//load teacher screen
					SceneManager.LoadScene ("Teacher Signup");
				} else if (role == "Parent") {
					//load parent screen
					SceneManager.LoadScene ("Parent Signup");
				}
				if (role == "Medical Professional") {
					//load medical screen
				}
			} else {
				errorPanel.SetActive (true);
				errorText.SetActive (true);
				infoText.text= data.text;
				Invoke ("close", 3.0f);
			}

		}
	}

	public bool MyValidate_email(string strToValidate)
	{
		if (System.String.IsNullOrEmpty(strToValidate))
			return false;
	
		return Regex.IsMatch(strToValidate, @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
				@"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$");
		

	}

	private string Get_id(WWW data){
		//return id
		char temp;
		temp='\n';
		int i = 0;
		while (true) {
			temp = data.text [i];
			i++;
			if (temp.Equals(':')) {
				break;
			}
		}
		string rv = "";
		while (true) {
			temp = data.text [i];
			if (temp.Equals('.')) {
				break;
			}
			rv=rv+temp;
			i++;
		}
		return rv;
	}

	void close(){
		errorPanel.SetActive (false);
		errorText.SetActive (false);

	}

	public void verify(){
		Debug.Log ("signup pressed "+userEmail);
		WWW www;
		WWWForm form = new WWWForm ();
		form.AddField ("email", userEmail);
		www = new WWW ("https://autismdiagnosis.000webhostapp.com/verified.php", form);
		StartCoroutine (WaitForRequest (www));
	}

	public void mySubmitForm(){

		Debug.Log ("signup pressed "+name.text);
		Debug.Log ("signup pressed "+email.text);
		Debug.Log ("signup pressed "+password.text);
		Debug.Log ("signup pressed "+g);
		Debug.Log ("signup pressed "+age.text);
		Debug.Log ("signup pressed "+telephone.text);
		Debug.Log ("signup pressed "+role);

		//web request
		WWW www;
		WWWForm form = new WWWForm ();
		//validate input
		if (MyValidate_email (email.text) != false) {
			form.AddField ("name", userName);
			form.AddField ("email", userEmail);
			form.AddField ("password", userPass);
			form.AddField ("gender", g);
			form.AddField ("age", userAge);
			form.AddField ("telephone", userTel);
			form.AddField ("role", role);
			www = new WWW (POSTAddUserURL, form);
			StartCoroutine (WaitForRequest (www));

		} else {
			Debug.Log ("Invalid email");
			errorPanel.SetActive (true);
			errorText.SetActive (true);
			infoText.text="Invalid Email";
			Invoke ("close", 3.0f);
		}

	}
}
