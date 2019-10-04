using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    private BallLauncher ballLauncher;
    private LevelManager levelManager;

    private TrajectoryRenderer trajectoryRenderer;
    private Camera mainCamera;

    private void Awake() {
        ballLauncher = GetComponent<BallLauncher>();
        mainCamera = Camera.main;
        levelManager = FindObjectOfType<LevelManager>();
        trajectoryRenderer = GetComponent<TrajectoryRenderer>();
    }

    void Update()
    {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        if(levelManager.CurrentLevelState == LevelState.WAITING){

            trajectoryRenderer.SetTrajectoryPoints(mousePosition);
            trajectoryRenderer.DrawDots();

            if(Input.GetKeyDown(KeyCode.Space)){
                ballLauncher.LaunchBalls(mousePosition);
            }
        } 
    }
}
