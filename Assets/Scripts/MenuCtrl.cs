using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;



public class MenuCtrl : MonoBehaviour 
{
	public GameObject MenuHUD;
	public GameObject CreditsHUD;
	public GameObject LoadingPanel;

	public void LoadScene(string sceneName)
	{
		SceneManager.LoadScene (sceneName);
	}

	/// <summary>
	/// Tymczasowy loading
	/// </summary>
	public void TemporaryLoadScene(string sceneName)
	{
		MenuHUD.SetActive (false);
		LoadingPanel.SetActive (true);
		SceneManager.LoadScene (sceneName);
	}

	public void LoadCredits()
	{
		MenuHUD.SetActive (false);
		CreditsHUD.SetActive (true);
	}

	public void LoadMenu()
	{
		MenuHUD.SetActive (true);
		CreditsHUD.SetActive (false);
	}
}
