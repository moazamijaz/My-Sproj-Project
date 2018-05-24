using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using System.IO;


public class AddChild : MonoBehaviour {
	//public Text cnic;
	public Text name;
	public Text age;
	public ToggleGroup gendergroup;
	public Text num_siblings;
	public Text medical_info;
	public GameObject errorPanel;
	public GameObject errorText;
	public Text infoText;

	public static string g;
	[SerializeField]
	private RawImage m_image;
	private static readonly string POSTAddUserURL = "https://autismdiagnosis.000webhostapp.com/ChildInfo.php";


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Application.platform == RuntimePlatform.Android) {
			if (Input.GetKey (KeyCode.Escape)) {
				// Insert Code Here (I.E. Load Scene, Etc)
				// OR Application.Quit();
				SceneManager.LoadScene("home");
				return;
			}
		}
	}

	public void GetValues(){
		foreach (var item in gendergroup.ActiveToggles()) {
			g = item.name;
			break;
		}

		//Debug.Log ("signup pressed "+cnic.text);
		Debug.Log ("signup pressed "+name.text);
		Debug.Log ("signup pressed "+g);
		Debug.Log ("signup pressed "+age.text);
		Debug.Log ("signup pressed "+num_siblings.text);
		Debug.Log ("signup pressed "+medical_info.text);

		WWW www;
		WWWForm form = new WWWForm ();

		//form.AddField ("id", cnic.text);
		form.AddField ("uid",LoginScript.userid ); //add login id later LoginScript.userid
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
			StartCoroutine (WaitForRequest (data));
			errorPanel.SetActive (true);
			errorText.SetActive (true);
			infoText.text= data.error;
			Invoke ("close", 3.0f);

		}
		else
		{
			Debug.Log("WWW Request: " + data.text);
			//find id in text
			errorPanel.SetActive (true);
			errorText.SetActive (false);
			infoText.text= data.text;
			Invoke ("close", 3.0f);
			Debug.Log (data.text[2]);
			if (data.text[2]=='s') {
				SceneManager.LoadScene ("home");
			}
		}
	}

	public void GoBack(){
		SceneManager.LoadScene ("home");
	}

	private List<string> GetAllGalleryImagePaths()
	{
		List<string> results = new List<string>();
		HashSet<string> allowedExtesions = new HashSet<string>() { ".png", ".jpg",  ".jpeg"  };

		try
		{
			AndroidJavaClass mediaClass = new AndroidJavaClass("android.provider.MediaStore$Images$Media");

			// Set the tags for the data we want about each image.  This should really be done by calling; 
			//string dataTag = mediaClass.GetStatic<string>("DATA");
			// but I couldn't get that to work...

			const string dataTag = "_data";

			string[] projection = new string[] { dataTag };
			AndroidJavaClass player = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject currentActivity = player.GetStatic<AndroidJavaObject>("currentActivity");

			string[] urisToSearch = new string[] { "EXTERNAL_CONTENT_URI", "INTERNAL_CONTENT_URI" };
			foreach (string uriToSearch in urisToSearch)
			{
				AndroidJavaObject externalUri = mediaClass.GetStatic<AndroidJavaObject>(uriToSearch);
				AndroidJavaObject finder = currentActivity.Call<AndroidJavaObject>("managedQuery", externalUri, projection, null, null, null);
				bool foundOne = finder.Call<bool>("moveToFirst");
				while (foundOne)
				{
					int dataIndex = finder.Call<int>("getColumnIndex", dataTag);
					string data = finder.Call<string>("getString", dataIndex);
					if (allowedExtesions.Contains(Path.GetExtension(data).ToLower()))
					{
						string path = @"file:///" + data;
						results.Add(path);
					}

					foundOne = finder.Call<bool>("moveToNext");
				}
			}
		}
		catch (System.Exception e)
		{
			// do something with error...
		}

		return results;
	}
	void close(){
		errorPanel.SetActive (false);
		errorText.SetActive (false);

	}

	public void GetImage()
	{
		//ask permission

		//gallery 
		List<string> galleryImages = GetAllGalleryImagePaths();
		Texture2D t = new Texture2D(2, 2);
		(new WWW(galleryImages[0])).LoadImageIntoTexture(t);
		m_image.texture = t;
	}
}
