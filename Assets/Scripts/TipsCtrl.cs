using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipsCtrl : MonoBehaviour {

	public float tipDelay;
	public GameObject tip;
	bool showTip;


	void Start () {
		showTip = true;	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (showTip) {
			if (other.gameObject.CompareTag ("Player")) {
				tip.SetActive (true);
				Invoke ("TipDissapear", tipDelay);
			}
		}
	}

	void TipDissapear()
	{
		showTip = false;
		Destroy (tip);
	}
}
