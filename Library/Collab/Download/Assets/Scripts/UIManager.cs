using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public GameObject victoryPanel;

    public GameObject defeatPanel;

    public Image progressBar;

    public LevelManager levelManager;

    private void Start() {
        levelManager.OnStateChanged.AddListener(HandleLevelStateChanged);
    }

    void ShowDefeatPanel(){
        defeatPanel.SetActive(true);
    }

    void ShowVictoryPanel(){
        victoryPanel.SetActive(true);
    }

    void UpdateLevelProgress(){
        progressBar.fillAmount = levelManager.LevelProgress;
    }

    public void HandleLevelStateChanged(LevelState oldState, LevelState newState){
        if(newState == LevelState.FINISHED){
            ShowVictoryPanel();
        }
        if(newState == LevelState.LOST){
            ShowDefeatPanel();
        }
        if(newState == LevelState.WAITING){
            UpdateLevelProgress();
        }
    }
}
