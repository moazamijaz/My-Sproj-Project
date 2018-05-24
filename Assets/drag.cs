using UnityEngine;
using System.Collections;

public class drag : MonoBehaviour {
	
	float distance = 30;


	void OnMouseDrag(){
		
		Vector3 mousePosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, distance);
		Vector3 objPosition = Camera.main.ScreenToWorldPoint (mousePosition);
		transform.position = objPosition;
	}

	void OnTriggerEnter2D(Collider2D other) {
		managerActivity9.count++;
		Debug.Log (managerActivity9.count);
		if (other.CompareTag (this.tag)) {
		
			managerActivity9.score++;
			other.GetComponent<SpriteRenderer> ().color = Color.green;
//			other.GetComponent<SpriteRenderer> ().sprite =;
			Destroy (this.gameObject);
		
		} else {
			other.GetComponent<SpriteRenderer> ().color = Color.red;
			//other.GetComponent<SpriteRenderer> ().sprite = red;
			//other.transform.localScale = new Vector3 (2.82f, 2.82f, 0f);
			Destroy (this.gameObject);
		
		}
	}
}