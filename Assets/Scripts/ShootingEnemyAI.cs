using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemyAI : MonoBehaviour {


	public GameObject shootingEnemyBullet;
	public GameObject shootingEnemy;
	public float bulletDelay;

	//Rigidbody2D rb;
	SpriteRenderer sr;

	Vector3 bulletSpawnPos;

	bool canFire;

	void Start () {
		sr = GetComponent<SpriteRenderer> ();
		canFire = false;
		bulletSpawnPos = gameObject.transform.Find ("BulletSpawnPos").transform.position;
		Invoke("Reload", Random.Range(1f, bulletDelay));
	}

	void Update () {
		if (canFire) 
		{
			FireBullets ();
			canFire = false;
		}
	}

	void Reload()
	{
		canFire = true;
	}

	public void FireBullets()
	{
		Instantiate (shootingEnemyBullet, bulletSpawnPos, Quaternion.identity);
		Invoke("Reload", bulletDelay);
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag ("PlayerBullet")) {
				Destroy (shootingEnemy);
				SFXCtrl.instance.ShowEnemyExplosion (gameObject.transform.position);
				AudioCtrl.instance.EnemyArrowHit(gameObject.transform.position);
			}

	}
}
