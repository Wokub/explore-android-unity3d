using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides the parallax effect
/// </summary>
public class menuParallax : MonoBehaviour {

	public float speed; //speed at which bg moves. set this to 0.001
	float offSetX;
	Material mat;


	// Use this for initialization
	void Start () {
		mat = GetComponent<Renderer> ().material;	
	}

	// Update is called once per frame
	void Update () {
		offSetX += speed;

		mat.SetTextureOffset ("_MainTex", new Vector2 (offSetX, 0));
	}
}
