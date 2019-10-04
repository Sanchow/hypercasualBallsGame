using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;

    void Awake(){
        if(instance == null){
            instance = this;
        } else if (instance != this){
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    #endregion


    public int CurrentLevel{ get; private set;}

    void Start()
    {
        LoadGame();
    }

    public void SaveGame(){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/gameData.xxx";
        FileStream stream = new FileStream(path, FileMode.Create);
        GameData gameData = new GameData(CurrentLevel);
        
        var json = JsonUtility.ToJson(gameData);
        formatter.Serialize(stream, json);
        stream.Close();
    }

    public bool LoadGame(){
        string path = Application.persistentDataPath + "/gameData.xxx";
        if(File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            GameData gameData = new GameData(0);
            JsonUtility.FromJsonOverwrite((string)formatter.Deserialize(stream), gameData);
            
            stream.Close();

            CurrentLevel = gameData.currentLevel;

            return true;

        } else {
            Debug.LogError("Save file not found in " + path);
            CurrentLevel = 0;

            return false;
        }
    }

    public void NextLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        CurrentLevel = SceneManager.GetActiveScene().buildIndex + 1;
        SaveGame();
    }

    public void ReloadLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame(){
        if(CurrentLevel == 0){
            NextLevel();
        } else {
            SceneManager.LoadScene(CurrentLevel);
        }
    }

    public void LoadMainMenu(){
        SceneManager.LoadScene(0);
    }

    public void ResetGame(){
        CurrentLevel = 0;
        SaveGame();
        LoadMainMenu();
    }

}


