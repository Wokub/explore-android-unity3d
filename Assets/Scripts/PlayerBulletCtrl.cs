using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletCtrl : MonoBehaviour {

	public Vector2 velocity;
	Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();	
	}
	
	// Update is called once per frame
	void Update () {
		rb.velocity = velocity;
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag ("Enemy")) {
			GameCtrl.instance.BulletEnemyCollision (other.gameObject.transform);
			AudioCtrl.instance.EnemyArrowHit(gameObject.transform.position);
			Destroy (gameObject);
		}
		else if (!other.gameObject.CompareTag ("Player")) //Destroy bullet if tag isnt player or enemy
		{
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Enemy")) {
			GameCtrl.instance.BulletEnemyCollision (other.gameObject.transform);
			AudioCtrl.instance.EnemyArrowHit(gameObject.transform.position);
			Destroy (gameObject);
		}
	}
}
