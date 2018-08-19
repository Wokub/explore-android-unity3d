using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour {

	Rigidbody2D rb;

	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("PlayerFeets")) {
			Invoke ("FallingDelay", 1.0f);
		}
	}

	void FallingDelay(){
		rb.isKinematic = false;
	}
}
