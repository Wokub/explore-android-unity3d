using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// checks if player is stuck
/// </summary>

public class PlayerStuck : MonoBehaviour {

	public GameObject player; //access to playerctrl script

	PlayerCtrl playerCtrl; //reference playerctrl script

	// Use this for initialization
	void Start () {
		playerCtrl = player.GetComponent<PlayerCtrl> ();
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		playerCtrl.isStuck = true;
	}

	void OnTriggerExit2D(Collider2D other)
	{
		playerCtrl.isStuck = false;
	}


}