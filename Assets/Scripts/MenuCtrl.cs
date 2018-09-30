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
    public GameObject Levels;
    public GameObject Logo;
    public GameObject MainButton;

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

    public void LoadLevel()
    {
        MenuHUD.SetActive(false);
        CreditsHUD.SetActive(false);
        Levels.SetActive(true);
        Logo.SetActive(false);
        MainButton.SetActive(false);
    }

    public void LeaveLevels()
    {
        MenuHUD.SetActive(true);
        CreditsHUD.SetActive(false);
        Levels.SetActive(false);
        Logo.SetActive(true);
        MainButton.SetActive(true);
    }
}
