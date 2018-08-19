using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tempSceneChanger : MonoBehaviour {

	public GameObject Loading;
	public GameObject UI;
//	public GameObject ComingSoonSign;
//	public GameObject CSBackground;

	public void LoadScene(string sceneName)
	{
		Loading.SetActive (true);
		UI.SetActive (false);
		SceneManager.LoadScene (sceneName);
//		ComingSoonSign.SetActive (false);
//		CSBackground.SetActive (false);
	}
}
