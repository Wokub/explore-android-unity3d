using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages:
/// 1. Player movement and flipping
/// 2. Player animations
/// </summary>
public class PlayerCtrl : MonoBehaviour 
{

	[Tooltip("This makes character move faster")]
	public int speedBoost = 5;
	public float jumpSpeed = 600;
	public float boxWidth;
	public float boxHeight;
	public int availableBullets;

    	public bool isGrounded;
	public Transform feet;
	public float feetRadius;
	public LayerMask whatIsGround;
	public Text arrows;
	public Transform leftBulletSpawnPosition, rightBulletSpawnPosition;
	public GameObject leftBullet, rightBullet;
	public bool leftPressed, rightPressed;
	public bool SFXOn;
	public bool canFire;
	public bool isStuck;
	public bool isFiring;

	GameCtrl gameCtrl;
	Animator anim;
	Rigidbody2D rb;
	SpriteRenderer sr;
	public bool isJumping;


	void Start () 
	{
		rb = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
		anim = GetComponent<Animator> ();
	}


	void Update () 
	{
		isGrounded = Physics2D.OverlapBox (new Vector2 (feet.position.x, feet.position.y), new Vector2 (boxWidth, boxHeight), 360.0f, whatIsGround);

		float playerSpeed = Input.GetAxisRaw ("Horizontal"); //value 0, -1 or 1	
		playerSpeed *= speedBoost;

		if (playerSpeed != 0)
			MoveHorizontal (playerSpeed);
		else
			StopMoving ();
		
		showFalling ();

		if (leftPressed)
			MoveHorizontal (-speedBoost);

		if (rightPressed)
			MoveHorizontal (speedBoost);

		if (transform.position.y <= -2.8)
			DisableCameraFollow ();

		if(Input.GetButtonDown("Jump"))
		{
			Jump();
		}

		if(Input.GetButtonDown("Fire1"))
		{
			FireBullets ();
		}
	
		arrows.text = "" + availableBullets;
	}

	void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(feet.position, new Vector3(boxWidth,boxHeight,0));
	}

	void MoveHorizontal(float playerSpeed) 
	{
		rb.velocity = new Vector2 (playerSpeed, rb.velocity.y);

		if (playerSpeed < 0)
			sr.flipX = true;
		else if(playerSpeed > 0)
			sr.flipX = false;

	    if (!isJumping)
	    {
	        anim.SetInteger("State", 1); 
	    }
			

	}

    void StopMoving()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);

        if (!isJumping && !isFiring)
        {
            anim.SetInteger("State", 0);
        }
    }

    void showFalling()
	{
		if (rb.velocity.y < 0) 
		{
			anim.SetInteger ("State", 3);
		}
	}

	void Jump() 
	{
		if (isGrounded) 
		{
			isJumping = true;
			rb.AddForce (new Vector2 (0, jumpSpeed));
			anim.SetInteger ("State", 2);

            AudioCtrl.instance.PlayerJump (gameObject.transform.position);
		}
	}

	void ShowIdle()
	{
		anim.SetInteger ("State", 0);
	}

	void FireBullets()
	{
		if (canFire && isGrounded) 
		{
			isFiring = true;///problem
			Invoke ("ShowIdle", 0.2f);

			if (sr.flipX) 
			{
				Instantiate (leftBullet, leftBulletSpawnPosition.position, Quaternion.identity);
			}

			if (!sr.flipX) 
			{
				Instantiate (rightBullet, rightBulletSpawnPosition.position, Quaternion.identity);
			}

			availableBullets -= 1;
			anim.SetInteger ("State", 4);
			AudioCtrl.instance.FireBullets (gameObject.transform.position);
		} 
		else if (!canFire && !isGrounded) 
		{
			isFiring = false;
			AudioCtrl.instance.OutOfAmmo (gameObject.transform.position);
		}

		if (availableBullets == 0)
			canFire = false;
	}

	public void MobileMoveLeft()
	{
		leftPressed = true;
	}

	public void IsntShooting()
	{
		isFiring = false;
	}

	public void MobileMoveRight()
	{
		rightPressed = true;
	}

	public void MobileStop()
	{
		leftPressed = false;
		rightPressed = false;

		StopMoving ();
	}

	public void MobileFireBullets()
	{
		isFiring = true;
		FireBullets ();
	}

	public void MobileJump()
	{
		Jump ();
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag ("Enemy")) {
			anim.SetInteger ("State", -1);
			GameCtrl.instance.PlayerDiedAnimation (gameObject);
		}
	}

	void DisableCameraFollow()
	{
		Camera.main.GetComponent<CameraCtrl> ().enabled = false;	
	}


	void OnTriggerEnter2D(Collider2D other)
	{
		switch (other.gameObject.tag) 
		{
		case "Coin":
			if (SFXOn) 
			{
				SFXCtrl.instance.ShowCoinSparkle (other.gameObject.transform.position);
				GameCtrl.instance.UpdateCoinCount ();//update hud with coins
				AudioCtrl.instance.CoinPickup(gameObject.transform.position);
			}
			break;
		case "Powerup_Bullet":
			canFire = true;
			availableBullets = 10;
			Vector3 powerupPos = other.gameObject.transform.position;
			Destroy (other.gameObject);
			if(SFXOn)
				SFXCtrl.instance.ShowBulletSparkle(powerupPos);
			break;
		case "Water":
			// splash effect
			SFXCtrl.instance.ShowSplash(other.gameObject.transform.position);
			AudioCtrl.instance.WaterSplash(gameObject.transform.position);
			break;
		case "Enemy":
			GameCtrl.instance.PlayerDiedAnimation (gameObject);
			anim.SetInteger ("State", -1);
			GameCtrl.instance.PlayerDiedAnimation (gameObject);
			break;
		default:
			break;
		}
	}
}