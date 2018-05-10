using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using System.Linq;

public class activities : MonoBehaviour {

	// Use this for initialization
	public static string id;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		changeText ();
	}

	public void GoBack(){
		SceneManager.LoadScene (10);
	}

	public void backHome(){
		SceneManager.LoadScene (8);
	}

	public void buttonclick(Button sender){
		Button button = (Button)sender;
		id = button.name;
		Debug.Log ("button id: " + id);
		SceneManager.LoadScene (18);
	}
	public void startActivity(){
		if (id == "3") {
			SceneManager.LoadScene (12);
		} else if (id == "5") {
			SceneManager.LoadScene (13);
		} else if (id == "6") {
			SceneManager.LoadScene (14);
		} else if (id == "7") {
			SceneManager.LoadScene (15);
		}  else if (id == "9") {
			SceneManager.LoadScene ("drag test");
		} else if (id == "10") {
			SceneManager.LoadScene (17);
		}
	}
	public void changeText(){

		GameObject actname = GameObject.Find ("Activity Name");
		Text name = actname.GetComponentInChildren<Text> ();
		actname = GameObject.Find ("Description");
		Text des = actname.GetComponentInChildren<Text> ();
		actname = GameObject.Find ("Instructions");
		Text ins = actname.GetComponentInChildren<Text> ();

		if (id == "3") {
			name.text = "Activity 3 - Dots Tasks";
			des.text = "Target Functionality: To Assess Shifting  \n Age: 3 years and above";
			ins.text= "Instructions \n" + "1- A heart or flower appears on the right or left of the screen.\n" +
				"2- A child must press on the same side as heart.\n" +
				"3- A child must press on opposite side of flower.\n4\tHeart and flower were intermixed in the test. \n";
		} else if (id == "5") {
			name.text = "Activity 5 - Working Memory Tasks";
			des.text = "Target Functionality: Holding in mind two pieces of information simultaneously / working memory   \n Age: 3 years and above";
			ins.text= "Instructions \n" + "1- Outline of a house is shown in screen \n2- There is an animal and colored dot in the house on the screen\n3- Examiner make sure that child know both color and animal\n4- Examiner ask the child to name animal and color\n5- Then next screen is shown with only outline of the house from previous screen \n6\tExaminer ask the child which animal was in the house\n";
		} else if (id == "6") {
			name.text = "Activity 6 - Dots TasksWorking Memoery Tasks";
			des.text = "Target Functionality: Attention Shifting/ Knowledge of color shape and size  \n Age: 3 years and above";
			ins.text= "Instructions \n" + "1- Two line drawn items appears on screen that are similar in terms of shape size or color. \n2- Examiner draws child’s attention to aspects along which the items are similar. E.g. “See there two pictures, they are same (big, blue, cats etc. ) ”\n3- Next screen again shows two previous items along with another third item which is similar to one of first two items along second dimension. E.g. if first two items are similar in terms of shape, then third item is similar to any one of them in term of size and color. \n4- Examiner say, “See here is the new picture. Show me which of the previous pictures is similar to this new picture ”.\n \n";
		} else if (id == "7") {
			name.text = "Activity 7 - Follow instructions";
			des.text = "Target Functionality: Receptive Language  \n Age: 3 years";
			ins.text= "Instructions \n" + "1- Will the child follow an instruction to do an enjoyable action under the conditions when activity usually occur\n2- E.g. jump on trampoline, swing etc\n";
		} else if (id == "9") {
			name.text = "Activity 9 - Puzzle";
			des.text = "Target Functionality: Visual performance  \n Age: 3 years and above";
			ins.text= "Instructions \n" + "1- Can a Student put single, uniquely shaped puzzle pieces into a frame board?";
		} else if (id == "10") {
			name.text = "Activity 10";
			des.text = " ";
			ins.text= " ";
		}

	}
}
