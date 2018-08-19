using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// handles the SFX
/// </summary>
public class SFXCtrl : MonoBehaviour {

	public static SFXCtrl instance; //allows to access public methods without creating object of this class
	public GameObject sfx_coin_pickup; //the particle effect to show when player pick ups the coin
	public GameObject sfx_bullet_pickup; //bullet pickup
	public GameObject sfx_playerLands; //player landing
	public GameObject sfx_splash; //splash effect
	public GameObject sfx_enemyDeath;
	public GameObject sfx_star;
	void Awake ()
	{
		if (instance == null)
			instance = this;
	}
		
	/// <summary>
	/// Shows the bullet sparkle effect when the player collects him.
	/// </summary>
	public void ShowCoinSparkle(Vector3 pos)
	{
		Instantiate (sfx_coin_pickup, pos, Quaternion.identity);
	}

	public void ShowBulletSparkle(Vector3 pos)
	{
		Instantiate (sfx_bullet_pickup, pos, Quaternion.identity);
	}

	public void ShowStarSparkle(Vector3 pos)
	{
		Instantiate (sfx_star, pos, Quaternion.identity);
	}

	public void ShowPlayerLanding(Vector3 pos)
	{
		Instantiate (sfx_playerLands, pos, Quaternion.identity);
	}

	/// <summary>
	/// shows the splash effect when player lands in water
	/// </summary>
	/// <param name="pos">Position.</param>
	public void ShowSplash(Vector3 pos)
	{
		Instantiate (sfx_splash, pos, Quaternion.identity);
	}
		
	/// <summary>
	/// Shows the enemy explosion.
	/// </summary>
	/// <param name="pos">Position.</param>
	public void ShowEnemyExplosion(Vector3 pos)
	{
		Instantiate (sfx_enemyDeath, pos, Quaternion.identity);
	}
}
