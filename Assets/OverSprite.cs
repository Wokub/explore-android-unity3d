﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverSprite : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Renderer>().sortingLayerName = "Foreground";
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
