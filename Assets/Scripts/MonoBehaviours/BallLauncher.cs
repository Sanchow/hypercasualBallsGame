using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallLauncher : MonoBehaviour
{

    public GameObject ballPrefab;

    public Transform spawnTransform;

    [SerializeField, Range(0, 2.5f)]
    private float ballSpawnPositionRange;

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

    private void RemoveBallFromList(Ball ball){
        instantiatedBalls.Remove(ball);
    }

    private void CheckIfAnyBallsAreLeft(){
        if(instantiatedBalls.Count == 0){
            ChangeLaunchPosition();
            levelManager.ChangeState(LevelState.WAITING);
        }
    }

    private void HandleBallsTakingTooLong(){
        levelManager.ChangeState(LevelState.ACCELERATED);
    }

    private void ChangeLaunchPosition(){
        spawnTransform.position = new Vector3(Random.Range(-ballSpawnPositionRange, ballSpawnPositionRange), spawnTransform.position.y, spawnTransform.position.z);
    }

}
