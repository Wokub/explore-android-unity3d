using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBulletOnCollision : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag ("Player")) {
			Destroy (gameObject);
			SFXCtrl.instance.ShowEnemyExplosion (gameObject.transform.position);
			AudioCtrl.instance.EnemyArrowHit(gameObject.transform.position);
		}
	}
}
