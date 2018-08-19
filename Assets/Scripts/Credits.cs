using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// makes the platform to move between two positions
/// </summary>
public class Credits : MonoBehaviour {

	public Transform pos1;
	public float speed;
	public Transform startPos;

	Vector3 nextPos;

	// Use this for initialization
	void Start () {
		nextPos = startPos.position;
	}

	// Update is called once per frame
	void Update () {
		transform.position = Vector3.MoveTowards (transform.position, nextPos, speed * Time.deltaTime);
	}
}
