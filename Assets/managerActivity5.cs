using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class managerActivity5 : MonoBehaviour {

	public GameObject[] animals;
	public GameObject instructions;
	public ToggleGroup Tgroup;
	public Transform myPanel;
	private int count;
	// Use this for initialization
	void Start () {

		count = 0;
		instructions.SetActive(false);
		animals [0].SetActive (true);
		animals [1].SetActive (false);
		//animals [2].SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void next (){
		Debug.Log ("count: "+count);
		if (count < 5) {
			if (count == 0) {
				animals [0].SetActive (false);
				instructions.SetActive(true);
			}
			if (count == 1) {
				animals [0].SetActive (false);
				animals [1].SetActive (false);
				animals [2].SetActive (false);
				animals [3].SetActive (false);
				instructions.SetActive(false);
				//take input
				Vector3 temp=new Vector3(0,0,0);
				Toggle T1 = (Toggle) Instantiate (Resources.Load ("myToggle", typeof(Toggle)), myPanel);
				T1.transform.position = Tgroup.transform.position + temp;
				T1.group = Tgroup;
				T1.name = "Yes";
				temp=new Vector3(0,-30,0);
				Toggle T2 = (Toggle) Instantiate (Resources.Load ("myToggle", typeof(Toggle)), myPanel);
				T2.transform.position = Tgroup.transform.position + temp;
				T2.group = Tgroup;
				T2.name = "No";
				Text label=T1.GetComponentInChildren<Text> ();
				label.text = "Yes";
				label=T2.GetComponentInChildren<Text> ();
				label.text = "No";
			}
			if (count == 2) {
				foreach (var item in Tgroup.ActiveToggles()) {
					//var t = item.GetComponentInChildren<Text>();
					//toggleResponses.Add (t.text);
					Debug.Log("Response: "+item.name);
					break;
				}
				GameObject.Destroy (GameObject.Find("Yes"));
				GameObject.Destroy (GameObject.Find("No"));
				animals [1].SetActive (true);
				animals [2].SetActive (true);
				animals [3].SetActive (true);
				instructions.SetActive(false);
			}
			if (count == 3) {
				animals [1].SetActive (false);
				instructions.SetActive(true);
			}
			if (count == 4) {
				animals [0].SetActive (false);
				animals [1].SetActive (false);
				animals [2].SetActive (false);
				animals [3].SetActive (false);
				instructions.SetActive(false);
				//take input
				Vector3 temp=new Vector3(0,0,0);
				Toggle T1 = (Toggle) Instantiate (Resources.Load ("myToggle", typeof(Toggle)), myPanel);
				T1.transform.position = Tgroup.transform.position + temp;
				T1.group = Tgroup;
				T1.name = "Yes";
				temp=new Vector3(0,-30,0);
				Toggle T2 = (Toggle) Instantiate (Resources.Load ("myToggle", typeof(Toggle)), myPanel);
				T2.transform.position = Tgroup.transform.position + temp;
				T2.group = Tgroup;
				T2.name = "No";
				Text label=T1.GetComponentInChildren<Text> ();
				label.text = "Yes";
				label=T2.GetComponentInChildren<Text> ();
				label.text = "No";
			}
			//animals [1].SetActive (true);
			//animals [2].SetActive (false);
			count++;
			//animals [count].SetActive (false);

		} else {
			foreach (var item in Tgroup.ActiveToggles()) {
				//var t = item.GetComponentInChildren<Text>();
				//toggleResponses.Add (t.text);
				Debug.Log("Response: "+item.name);
				break;
			}
			GameObject.Destroy (GameObject.Find("Yes"));
			GameObject.Destroy (GameObject.Find("No"));

			Application.LoadLevel ("5-results");
		
		}
	
	}
}
//Child_id, A5_screenanimal, A5_correctResponse