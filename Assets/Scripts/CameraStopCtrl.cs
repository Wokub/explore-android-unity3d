using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraStopCtrl : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Player"))
		{
			GameCtrl.instance.StopCameraFollow();
			GameCtrl.instance.bossBars ();
		}
	}
}
