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
        if(levelManager.CurrentLevelState == LevelState.WAITING){
            if(Input.touchCount > 0){
                Touch touch = Input.GetTouch(0);
                Vector3 touchPosition = mainCamera.ScreenToWorldPoint(touch.position);
                touchPosition.y = Mathf.Clamp(touchPosition.y, -3.1f, 10f);
                trajectoryRenderer.SetTrajectoryPoints(touchPosition);
                trajectoryRenderer.DrawDots();
                if(touch.phase == TouchPhase.Ended){
                    ballLauncher.LaunchBalls(touchPosition);
                }

            }
        } 
    }
}
