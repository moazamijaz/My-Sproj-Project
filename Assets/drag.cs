using UnityEngine;
using System.Collections;

public class drag : MonoBehaviour {
	
	float distance = 10;

	void OnMouseDrag(){
		
		Vector3 mousePosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, distance);
		Vector3 objPosition = Camera.main.ScreenToWorldPoint (mousePosition);
		transform.position = objPosition;
	}

	void OnTriggerEnter2D(Collider2D other) {
		
		managerActivity9.count++;
		Debug.Log (managerActivity9.count);
		Debug.Log ("score: "+managerActivity9.score);

		if (other.CompareTag (this.tag)) {
			Debug.Log (this.tag);
			managerActivity9.score++;
			managerActivity9.response = managerActivity9.response + "{" +
			@"""Child_id""" + ":" + '"' + managerActivity9.cid + '"' + "," +
			@"""A9_Shape""" + ":" + '"' + this.tag + '"' + "," +
			@"""A9_correctResponse""" + ":" + '"' + "True" + '"' + "," +
			@"""A9_screenstarttime""" + ":" + @"""0""" + "," +
			@"""A9_answertime""" + ":" + @"""0""" + "}";
			other.GetComponent<SpriteRenderer> ().color = Color.white;
			Destroy (this.gameObject);
		
		} else {
			managerActivity9.response = managerActivity9.response + "{" +
			@"""Child_id""" + ":" + '"' + managerActivity9.cid + '"' + "," +
			@"""A9_Shape""" + ":" + '"' + this.tag + '"' + "," +
			@"""A9_correctResponse""" + ":" + '"' + "False" + '"' + "," +
			@"""A9_screenstarttime""" + ":" + @"""0""" + "," +
			@"""A9_answertime""" + ":" + @"""0""" + "}";
			other.GetComponent<SpriteRenderer> ().color = Color.black;
			Destroy (this.gameObject);
		
		}

		if (managerActivity9.count == 5) {
			managerActivity9.response = managerActivity9.response + "]";
		} else {
			managerActivity9.response = managerActivity9.response + ",";

		}
	}
}