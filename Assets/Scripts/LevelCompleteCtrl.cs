using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// Level complete ctrl.
/// </summary>
public class LevelCompleteCtrl : MonoBehaviour
{

    public GameObject oneStar;
    public GameObject twoStars;
    public GameObject threeStars;
	public Button btnNext;
	public Sprite goldenStar;
	public Image Star1;
	public Image Star2;
	public Image Star3;
	public Text txtScore;

	public int levelNumber;
	[HideInInspector]
	public int score;

	public int ScoreForThreeStars;
	public int ScoreForTwoStars;
	public int ScoreForOneStar;
	public int ScoreForNextLevel;
	public float animStartDelay;
	public float animDelay;

	bool showTwoStars, showThreeStars;
		
	// Use this for initialization
	void Start () {
		
		score = GameCtrl.instance.GetScore ();
		Debug.Log (score);

		txtScore.text = "" + score;

		if (score >= ScoreForThreeStars) {
			showThreeStars = true;
			GameCtrl.instance.SetStarsAwarded (levelNumber, 3);
			Invoke ("ShowGoldenStars", animStartDelay);
		}

		if (score >= ScoreForTwoStars && score < ScoreForThreeStars) {
			showTwoStars = true;
			GameCtrl.instance.SetStarsAwarded (levelNumber, 2);
			Invoke ("ShowGoldenStars", animStartDelay);
		}
	
		if (score <= ScoreForOneStar && score != 0)
		{
		    GameCtrl.instance.SetStarsAwarded(levelNumber, 1);
            Invoke ("ShowGoldenStars", animStartDelay);
		}
	}

	void ShowGoldenStars(){
		StartCoroutine ("HandleFirstStarAnim", Star1);
	}

	IEnumerator HandleFirstStarAnim(Image starImg){
	    oneStar.SetActive(true);
        doAnim (starImg);
		yield return new WaitForSeconds (animDelay);

		if (showTwoStars || showThreeStars)
			StartCoroutine ("HandleSecondStarAnim", Star2);
		else
			Invoke ("CheckLevelStatus", 1.0f);
	}

	IEnumerator HandleSecondStarAnim(Image starImg){
	    twoStars.SetActive(true);
        doAnim (starImg);

		yield return new WaitForSeconds (animDelay);

		showTwoStars = false;
		Invoke ("CheckLevelStatus", 1.0f);
		if (showThreeStars)
			StartCoroutine ("HandleThirdStarAnim", Star3);
	}

	IEnumerator HandleThirdStarAnim(Image starImg){
	    threeStars.SetActive(true);
        doAnim (starImg);

		yield return new WaitForSeconds (animDelay);

		showThreeStars = false;
		Invoke ("CheckLevelStatus", 1.0f);
	}

	void CheckLevelStatus(){
	
		if (score >= ScoreForNextLevel) {
			btnNext.interactable = true;
			SFXCtrl.instance.ShowPlayerLanding (btnNext.gameObject.transform.position);
			AudioCtrl.instance.StompedHead (btnNext.gameObject.transform.position);
			GameCtrl.instance.UnlockLevel (levelNumber + 1);

		} else {
			btnNext.interactable = false;
		}
	}

	void doAnim(Image starImg){
		//increase star size
		starImg.rectTransform.sizeDelta = new Vector2(175f,175f);
		//image swap
		starImg.sprite = goldenStar;
		//anim
		RectTransform t = starImg.rectTransform;
		t.DOSizeDelta (new Vector2 (150f, 150f), 0.5f, false);
		//audio
		AudioCtrl.instance.EnemyArrowHit(starImg.gameObject.transform.position);
		//sfx
		SFXCtrl.instance.ShowStarSparkle (starImg.gameObject.transform.position);
		//return
	}

	// Update is called once per frame
	void Update () {
		
	}
}
