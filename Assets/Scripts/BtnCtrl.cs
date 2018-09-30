using System.Collections;
using System.Collections.Generic;
//using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BtnCtrl : MonoBehaviour {
	public GameObject Loading;
	public GameObject UI;
    public GameObject Levels;

    int levelNumber;
	Button btn;
	Image btnImg;

	Transform star1;
	Transform star2;
	Transform star3;

	public string sceneName;
	Text btnTxt;

	public Sprite btnLocked;
	public Sprite btnUnlocked;

	// Use this for initialization
	void Start () {
		
		levelNumber = int.Parse (transform.gameObject.name); //transforms text into integer
		btn = transform.gameObject.GetComponent<Button>();
		btnImg = btn.GetComponent<Image> ();
		btnTxt = btn.gameObject.transform.GetChild (0).GetComponent<Text> ();
		star1 = btn.gameObject.transform.GetChild (1);
		star2 = btn.gameObject.transform.GetChild (2);
		star3 = btn.gameObject.transform.GetChild (3);
	
		BtnStatus ();
	}
	
	// Update is called once per frame
	void BtnStatus(){
	
		bool unlocked = DataCtrl.instance.isUnlocked (levelNumber);
		int starsAwarded = DataCtrl.instance.getStars (levelNumber);

		if (unlocked) {
			if (starsAwarded == 3) {
				star1.gameObject.SetActive (true);
				star2.gameObject.SetActive (true);
				star3.gameObject.SetActive (true);
			}
			if (starsAwarded == 2) {
				star1.gameObject.SetActive (true);
				star2.gameObject.SetActive (true);
				star3.gameObject.SetActive (false);
			}
			if (starsAwarded == 1) {
				star1.gameObject.SetActive (true);
				star2.gameObject.SetActive (false);
				star3.gameObject.SetActive (false);
			}
			if (starsAwarded == 0) {
				star1.gameObject.SetActive (false);
				star2.gameObject.SetActive (false);
				star3.gameObject.SetActive (false);
			}

			btn.onClick.AddListener(LoadScene);

		} else {
			btnImg.overrideSprite = btnLocked;
			btnTxt.text = "";
			star1.gameObject.SetActive (false);
			star2.gameObject.SetActive (false);
			star3.gameObject.SetActive (false);
		}
	}

	void LoadScene()
	{
		Loading.SetActive (true);
		UI.SetActive (false);
        Levels.SetActive(false);
		SceneManager.LoadScene(sceneName);
	}
}
