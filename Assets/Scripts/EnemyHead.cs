using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHead : MonoBehaviour {

	public GameObject enemy;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("PlayerFeets")) {
			GameCtrl.instance.HeadDamaged (enemy);

			SFXCtrl.instance.ShowEnemyExplosion (enemy.transform.position);
			AudioCtrl.instance.StompedHead (gameObject.transform.position);
		}
	}

}
