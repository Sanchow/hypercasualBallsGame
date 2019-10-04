using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallLauncher : MonoBehaviour
{

    public GameObject ballPrefab;

    public Transform spawnTransform;

    private int numberOfBalls;

    [SerializeField]
    private float ballSpeed;

    [SerializeField]
    private float launchDelay;

    private Vector2 targetPoint;
    private List<Ball> instantiatedBalls;
    private LevelManager levelManager;

    void Start(){
        instantiatedBalls = new List<Ball>();
        levelManager = FindObjectOfType<LevelManager>();
        numberOfBalls = levelManager.levelData.numberOfBalls;
    }


    public void LaunchBalls(Vector2 _targetPoint){
        levelManager.ChangeState(LevelState.PLAYING);
        targetPoint = _targetPoint;
        StartCoroutine(LaunchBallsWithDelay());
    }

    private IEnumerator LaunchBallsWithDelay(){
        Vector2 direction = new Vector2(targetPoint.x - spawnTransform.position.x, targetPoint.y - spawnTransform.position.y);
        direction.Normalize();
        for (int i = 0; i < numberOfBalls; i++)
        {
            Ball instance = Instantiate(ballPrefab, spawnTransform.position, Quaternion.identity).GetComponent<Ball>();
            instance.Initialize(direction, ballSpeed);
            instance.Launch();
            
            instantiatedBalls.Add(instance);

            instance.OnBallDestroyed += () => RemoveBallFromList(instance);
            instance.OnBallDestroyed += CheckIfAnyBallsAreLeft;
            instance.OnBallTakingTooLong += HandleBallsTakingTooLong;
            yield return new WaitForSeconds(launchDelay);
        }
    }

    public void RemoveBallFromList(Ball ball){
        instantiatedBalls.Remove(ball);
    }

    public void CheckIfAnyBallsAreLeft(){
        if(instantiatedBalls.Count == 0){
            levelManager.ChangeState(LevelState.WAITING);
        }
    }

    public void HandleBallsTakingTooLong(){
        levelManager.ChangeState(LevelState.ACCELERATED);
    }

}
