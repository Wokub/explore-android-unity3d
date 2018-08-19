using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles in game audio
/// </summary>
public class AudioCtrl : MonoBehaviour {

	public static AudioCtrl instance;
	public PlayerAudio playerAudio;
	public AudioFX audioFX;
	public Transform player;

	public bool soundOn; //sound On/Off

	// Use this for initialization
	void Start () {
		if (instance == null)
			instance = this;
	}

	public void EnemyArrowHit(Vector3 playerPos)
	{
		if (soundOn) 
		{
			AudioSource.PlayClipAtPoint (playerAudio.enemyArrowHit, playerPos);
		}
	}


	public void PlayerJump(Vector3 playerPos)
	{
		if (soundOn) 
		{
			AudioSource.PlayClipAtPoint (playerAudio.playerJump, playerPos);
		}
	}

	public void StompedHead(Vector3 playerPos)
	{
		if (soundOn) 
		{
			AudioSource.PlayClipAtPoint (playerAudio.enemyExplosion, playerPos);
		}
	}

	public void CoinPickup(Vector3 playerPos)
	{
		if (soundOn) 
		{
			AudioSource.PlayClipAtPoint (playerAudio.coinPickup, playerPos);
		}
	}

	public void EnemySteps(Vector3 playerPos)
	{
		if (soundOn) 
		{
			AudioSource.PlayClipAtPoint (playerAudio.enemySteps, playerPos);
		}
	}

	public void FireBullets(Vector3 playerPos)
	{
		if (soundOn) 
		{
			AudioSource.PlayClipAtPoint (playerAudio.fireBullets, playerPos);
		}
	}

	public void OutOfAmmo(Vector3 playerPos)
	{
		if (soundOn) 
		{
			AudioSource.PlayClipAtPoint (playerAudio.outOfAmmo, playerPos);
		}
	}

	public void WaterSplash(Vector3 playerPos)
	{
		if (soundOn) 
		{
			AudioSource.PlayClipAtPoint (playerAudio.waterSplash, playerPos);
		}
	}

}
