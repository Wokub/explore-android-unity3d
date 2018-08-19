using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System; //access to serializable attr, you dont need to use system.serializable at every verse

/// <summary>
/// Script that makes data to save into binary file
/// </summary>

[Serializable]
public class GameData
{
	public int coinCount; //saving coins
	public int score; //tracking score
	public LevelData[] levelData; //tracking lvls,stars,points
	public bool isFirstBoot; // for initializing data when game is started
}

