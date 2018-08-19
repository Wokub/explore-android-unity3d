using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyActiv : MonoBehaviour {

	public GameObject bat;
	FlyingEnemyAI bai;

	void Start(){
		bai = bat.GetComponent<FlyingEnemyAI> ();
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("Player")) {
			bai.ActivateBat (other.gameObject.transform.position);
		}
	}
}
