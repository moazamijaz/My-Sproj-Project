using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changecolor : MonoBehaviour {

	// Use this for initialization
	public Sprite red;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		//managerActivity9.count++;
		//Debug.Log (managerActivity9.count);
		if (!this.CompareTag (other.tag)) {
			this.GetComponent<SpriteRenderer> ().sprite = red;
		}
			
	}
}
