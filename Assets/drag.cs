﻿using UnityEngine;
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
		if (other.CompareTag (this.tag)) {
		
			managerActivity9.score++;
			other.GetComponent<SpriteRenderer> ().color = Color.white;
			Destroy (this.gameObject);
		
		} else {
			other.GetComponent<SpriteRenderer> ().color = Color.black;
			Destroy (this.gameObject);
		
		}
	}
}