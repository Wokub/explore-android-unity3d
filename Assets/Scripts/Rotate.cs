using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    Transform trans; //giving Transform a reference

    void Awake() //When game starts...
    {
        trans = GetComponent<Transform>(); //return a component to our "trans:
    }

    void Update()
    {
        trans.Rotate(0, 0, -2); //if he is, then rotate clockwise in the x axis
    }
}
