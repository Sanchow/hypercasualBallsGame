using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    public GameObject victoryPanel;

    public TextMeshProUGUI victoryText;

    public GameObject defeatPanel;

    public Image progressBar;

    public TextMeshPro ballCounter;

    public LevelManager levelManager;

    private void Start() {
        levelManager.OnStateChanged.AddListener(HandleLevelStateChanged);
        InitializeTextBoxes();
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

    void InitializeTextBoxes(){
        ballCounter.text = "x" + levelManager.levelData.numberOfBalls;
        victoryText.text = "Level " + GameManager.instance.CurrentLevel;
    }

    public void HandleLevelStateChanged(LevelState oldState, LevelState newState){
        if(newState == LevelState.FINISHED){
            UpdateLevelProgress();
            ShowVictoryPanel();
        }
        if(newState == LevelState.LOST){
            UpdateLevelProgress();
            ShowDefeatPanel();
        }
        if(newState == LevelState.WAITING){
            UpdateLevelProgress();
        }
    }
}
