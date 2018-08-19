using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Refresh DataCtrl.
/// </summary>
public class RefreshData : MonoBehaviour {
	
	void Start () {
		DataCtrl.instance.RefreshData ();
	}
}
