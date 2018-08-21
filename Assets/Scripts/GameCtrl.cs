using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //access to UI, text, etc
using UnityEngine.SceneManagement;
using System.IO; //allows to work with other files
using System.Runtime.Serialization.Formatters.Binary; //allows to use Serialization more professional

/// <summary>
/// Manager of important thigs, like keeping the score, resterting levels, saving/loading data, updating HUD,etc
/// </summary>
public class GameCtrl : MonoBehaviour {
	
	public static GameCtrl instance;

	public Text txtCoinCount;
	public Text txtScore;

	public float restartDelay;
	public int coinValue;
	public int enemyValue;
	public float maxTime; //time
	public float coinsNeeded;
	public Vector3 bossCameraPos;

	public GameData data; //loading from Gamedata.cs

	public GameObject bossWall;
	public GameObject player;
	public GameObject firstBoss;

	public UI ui;
	public bool lost;

	string dataFilePath; //path to our save file
	//BinaryFormatter bf; //saving/loading bin files

	float timeLeft;
	float levelInfoTime;
	public bool timerOn;


	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake(){
		if (instance == null)
			instance = this;

		//bf = new BinaryFormatter ();

		dataFilePath = Application.persistentDataPath + "/save.dat";
		Debug.Log (dataFilePath);
	}

	// Use this for initialization
	void Start () {

		DataCtrl.instance.RefreshData ();
		data = DataCtrl.instance.data;
		RefreshUI ();
		ResetCoins ();

		timeLeft = maxTime;
		timerOn = true;

		HandleFirstBoot ();
		if(!lost)
			Invoke ("HUDVisibility", 2);
	}
	
	// Update is called once per frame
	void Update () {
		if (timeLeft > 0 && timerOn)
			UpdateTimer ();
	}

	/// <summary>
	/// Loads the data.
	/// </summary>
	public void RefreshUI()
	{
		txtCoinCount.text = " x " + data.coinCount;
		txtScore.text = "" + data.score;
	}

	/// <summary>
	/// Raises the enable event.
	/// </summary>
	void OnEnable()
	{
		Debug.Log("Loaded");
		RefreshUI();
	}

	/// <summary>
	/// Raises the disable event.
	/// </summary>
	void OnDisable()
	{
		Debug.Log("Saved");
		DataCtrl.instance.SaveData (data);
	}

	/// <summary>
	/// Resets the coins.
	/// </summary>
	public void ResetCoins()
	{
		data.coinCount = 0;
		txtCoinCount.text = " x 0";
		data.score = 0;
		txtScore.text = "";
	}


	/// <summary>
	/// Sets the stars awarded.
	/// </summary>
	public void SetStarsAwarded(int levelNumber, int numOfStars)
	{
		data.levelData [levelNumber].starsAwarded = numOfStars;

		Debug.Log ("Stars:" + data.levelData [levelNumber].starsAwarded);
	}

	public void UnlockLevel(int levelNumber){
		data.levelData [levelNumber].isUnlocked = true;
	}

	/// <summary>
	/// Gets the score.
	/// </summary>
	public int GetScore()
	{
		return data.score;
	}


	/// <summary>
	/// Bullets and the enemy collision.
	/// </summary>
	/// <param name="enemy">Enemy.</param>
	public void BulletEnemyCollision(Transform enemy)
	{
		//SFX
		Vector3 pos = enemy.position;
		pos.z = 20f;//because of parallax
		SFXCtrl.instance.ShowEnemyExplosion(pos);

		//enemy death
		Destroy(enemy.gameObject);

		UpdateScore(enemyValue);
	}

	/// <summary>
	/// restarts the lvl when player dies
	/// </summary>
	public void PlayerDied(GameObject player)
	{
	//	player.SetActive (false);
		Invoke("GameOver", restartDelay);
	}

	/// <summary>
	/// Players death animation.
	/// </summary>
	/// <param name="player">Player.</param>
	public void PlayerDiedAnimation(GameObject player)
	{
		Rigidbody2D rb = player.GetComponent<Rigidbody2D> ();

		player.GetComponent<PlayerCtrl>().enabled = false;
		Camera.main.GetComponent<CameraCtrl>().enabled = false;

		rb.velocity = Vector2.zero;

		StartCoroutine ("PauseBefore", player);
	}


	IEnumerator PauseBefore(GameObject player)
	{
		yield return new WaitForSeconds (1.0f);
		PlayerDied(player);
	}


	/// <summary>
	/// Players the drowned.
	/// </summary>
	/// <param name="player">Player.</param>
	public void PlayerDrowned(GameObject player)
	{
		//nq prezentacje
		Invoke("GameOver", restartDelay);
		//na prezentacje

		//Invoke("RestartLevel", restartDelay);
	}


	public void HeadDamaged (GameObject enemy)
	{
		enemy.tag = "Untagged";
		Destroy (enemy);
		UpdateScore(enemyValue);
	}


	/// <summary>
	/// Updates the coin count.
	/// </summary>
	public void UpdateCoinCount()
	{
		data.coinCount += 1;

		txtCoinCount.text = " x " + data.coinCount;//adding string so it will work as string

		UpdateScore(coinValue);
	}

	/// <summary>
	/// Updates the score.
	/// </summary>
	public void UpdateScore(int value)
	{
		data.score += value;
		txtScore.text = "" + data.score;
	}

	/// <summary>
	/// Restarts the level.
	/// </summary>
	void RestartLevel()
	{
		ResetCoins ();
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}

	/// <summary>
	/// Updates the timer.
	/// </summary>
	void UpdateTimer()
	{
		timeLeft -= Time.deltaTime;

		ui.txtTimer.text = "" + (int)timeLeft;

		if (timeLeft <= 0) {
			ui.txtTimer.text = "0";

			//na prezentacje
			Invoke("GameOver", restartDelay);
			//na prezentacje

			//Invoke("RestartLevel", restartDelay);
		}
	}

	/// <summary>
	/// Handles the first boot.
	/// </summary>
	void HandleFirstBoot ()
	{
		if(data.isFirstBoot)
		{
			ui.bossHealthBar.SetActive (false);
			//set number of coins to 0
			data.coinCount = 0;
			//set score to 0
			data.score = 0;
			//set isFirstBoot to false
			data.isFirstBoot = false;
		}
	}
		
	void GameOver()
	{
		timerOn = false;
		lost = true;
		ResetCoins ();
		ui.panelGameOver.SetActive (true);
		ui.playerKeyboard.SetActive (false);
		ui.topHUD.SetActive (false);
	}

	public void Won()
	{
		ui.panelWon.SetActive (true);
		ui.playerKeyboard.SetActive (false);
		ui.topHUD.SetActive (false);
	}

	void HUDVisibility()
	{
		ui.panelWon.SetActive (false);
		ui.playerKeyboard.SetActive (true);
		ui.topHUD.SetActive (true);
		ui.levelInfo.SetActive (false);
	}

	public void bossBars()
	{
		ui.bossHealthBar.SetActive (true);
		bossWall.SetActive (true);
		firstBoss.SetActive (true);
	}

	public void StopCameraFollow()
	{

		//getting isStuck bool from PlayerCtrl script
		player.GetComponent<PlayerCtrl> ().isStuck = true;
		//getting player child components and turning them off
		player.transform.Find ("Left_Check").gameObject.SetActive (false);
		player.transform.Find ("Right_Check").gameObject.SetActive (false);

		//moving and locking camera on one spot
		Camera.main.GetComponent<CameraCtrl> ().transform.position = new Vector3 (bossCameraPos.x, bossCameraPos.y, bossCameraPos.z);
		//43.6f, 3.55f, -10f
		Camera.main.GetComponent<CameraCtrl> ().enabled = false;
	}
}
