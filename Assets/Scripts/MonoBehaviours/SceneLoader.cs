using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{

    void Awake(){
        if(GameManager.instance == null){
            Debug.LogWarning("This scene does not contain a Game Manager. Boot scene from main menu!");
        }
    }

    public void MainMenu(){
        if(GameManager.instance == null){
            Debug.LogWarning("This scene does not contain a Game Manager. Boot scene from main menu!");
        }
        GameManager.instance.LoadMainMenu();
    }

    public void NextLevel(){
        if(GameManager.instance == null){
            Debug.LogWarning("This scene does not contain a Game Manager. Boot scene from main menu!");
        }
        GameManager.instance.NextLevel();
    }

    public void RestartLevel(){
        if(GameManager.instance == null){
            Debug.LogWarning("This scene does not contain a Game Manager. Boot scene from main menu!");
        }
        GameManager.instance.ReloadLevel();
    }

    public void StartGame(){
        if(GameManager.instance == null){
            Debug.LogWarning("This scene does not contain a Game Manager. Boot scene from main menu!");
        }
        GameManager.instance.StartGame();
    }
}
