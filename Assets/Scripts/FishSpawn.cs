﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawn : MonoBehaviour {
	public GameObject fish;
	public float spawnDelay;

	bool canSpawn;

	// Use this for initialization
	void Start () {
		canSpawn = true;	
	}
	
	// Update is called once per frame
	void Update () {
		if (canSpawn) {
			StartCoroutine ("SpawnFish");
		}
	}

	IEnumerator SpawnFish(){
		Instantiate (fish, transform.position, Quaternion.identity);
		canSpawn = false;
		yield return new WaitForSeconds (spawnDelay);
		canSpawn = true;
	}
}
