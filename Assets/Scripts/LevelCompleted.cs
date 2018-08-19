using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleted : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			GameCtrl.instance.Won ();
			GameCtrl.instance.timerOn = false;
		}
	}
}
