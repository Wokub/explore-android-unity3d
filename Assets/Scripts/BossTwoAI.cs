using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossTwoAI : MonoBehaviour {


	public Slider bossHealth;
	public GameObject bossBullet;
	public GameObject boss;
	public GameObject bossHPBar;
	public GameObject nextLevelSign;
	public GameObject cameraSign;
	public int health;
	public float bulletDelay;

	//Rigidbody2D rb;
	SpriteRenderer sr;

	Vector3 bulletSpawnPos;

	bool canFire;

	// Use this for initialization
	void Start () {
	//	rb = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();

		canFire = false;

		bulletSpawnPos = gameObject.transform.Find ("BulletSpawnPos").transform.position;

		Invoke("Reload", Random.Range(10f, bulletDelay));
	}
	
	// Update is called once per frame
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
		Instantiate (bossBullet, bulletSpawnPos, Quaternion.identity);
		Invoke("Reload", bulletDelay);
	}

	void RestoreColor()
	{
		sr.color = Color.white;
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag ("PlayerBullet")) {
			if (health == 1){

				Destroy (boss);
				bossHPBar.SetActive (false);
				nextLevelSign.SetActive (true);
				cameraSign.SetActive (false);
				SFXCtrl.instance.ShowEnemyExplosion (gameObject.transform.position);
				AudioCtrl.instance.EnemyArrowHit(gameObject.transform.position);
			}

			if (health > 0) {
				health--;
				bossHealth.value = (float)health;
				sr.color = Color.red;

				Invoke ("RestoreColor", 0.1f);
			}
		}
	}

}
