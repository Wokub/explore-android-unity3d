using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlyingEnemyAI : MonoBehaviour {

	public float destroyBatDelay;
	public float batSpeed;

	public void ActivateBat(Vector3 playerPos){
		transform.DOMove (playerPos, batSpeed, false);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.CompareTag ("Ground") || other.gameObject.CompareTag ("Player")) {
			SFXCtrl.instance.ShowEnemyExplosion (other.gameObject.transform.position);

			Destroy (gameObject, destroyBatDelay);
		}
	}
}
