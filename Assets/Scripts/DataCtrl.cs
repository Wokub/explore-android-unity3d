using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/// <summary>
/// Centralized game database, that follows your movement 
/// </summary>
public class DataCtrl : MonoBehaviour {

	public bool devm;
	public static DataCtrl instance = null;
	public GameData data;

	string dataFilePath;
	BinaryFormatter bf;

	void Awake(){
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (gameObject);
		} else {
			Destroy (gameObject);
		}

		bf = new BinaryFormatter ();
		dataFilePath = Application.persistentDataPath + "/save.dat";
		Debug.Log (dataFilePath);
	}

	public void RefreshData(){
	
		if (File.Exists (dataFilePath)) {
			FileStream fs = new FileStream (dataFilePath, FileMode.Open);
			data = (GameData)bf.Deserialize (fs);
			fs.Close ();

			Debug.Log ("Data refreshed");
		}
	}

	public void SaveData(){
		FileStream fs = new FileStream (dataFilePath, FileMode.Create);
		bf.Serialize (fs, data);
		fs.Close();
		Debug.Log ("Testing: Data Saved");
	}

	public void SaveData(GameData data){
		FileStream fs = new FileStream (dataFilePath, FileMode.Create);
		bf.Serialize (fs, data);
		fs.Close();
		Debug.Log ("Testing: Data Saved");
	}

	public bool isUnlocked(int levelNumber){
		return data.levelData [levelNumber].isUnlocked;
	}

	public int getStars(int levelNumber){
		return data.levelData [levelNumber].starsAwarded;
	}
	void OnEnable(){
		//RefreshData ();
		CheckDB();
	}


	void CheckDB(){
		if (!File.Exists (dataFilePath)) {
			#if UNITY_ANDROID
			CopyDB();
			#endif
		} else {

			if (SystemInfo.deviceType == DeviceType.Desktop) {
				string fileDest = System.IO.Path.Combine (Application.streamingAssetsPath, "save.dat");
				File.Delete (fileDest);
				File.Copy (dataFilePath, fileDest);
			}
			if (devm) {
				if (SystemInfo.deviceType == DeviceType.Handheld) {
					File.Delete (dataFilePath);
					CopyDB ();
				}
			}
			RefreshData ();
		}
	}

	void CopyDB(){
		string srcFile = System.IO.Path.Combine (Application.streamingAssetsPath, "save.dat");
		WWW downloader = new WWW (srcFile);

		while (!downloader.isDone) {
		}

		File.WriteAllBytes (dataFilePath, downloader.bytes);
		RefreshData ();
	}
}
