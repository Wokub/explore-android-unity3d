using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// Grouping all interface elements in one place
/// </summary>

[Serializable]
public class UI
{
	[Header("Text")]
	public Text txtTimer; //global timer

	//na prezentacje
	[Header("Images/ Sprites")]
	public GameObject panelGameOver; //panel after losing
	public GameObject playerKeyboard;
	public GameObject topHUD;
	public GameObject panelWon;
	public GameObject levelInfo;
	public GameObject bossHealthBar;
	//na prezentacje
}