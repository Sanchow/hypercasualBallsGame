using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{

    public TextMeshProUGUI currentLevelText;

    public GameObject ballPrefab;


    // Start is called before the first frame update
    void Start()
    {
        currentLevelText.text = GameManager.instance.CurrentLevel == 0 ? "1" : GameManager.instance.CurrentLevel.ToString();
        LaunchBalls(1);
    }


    void LaunchBalls(int amount){

        Vector2 direction = new Vector2(1,1);
        direction.Normalize();

        for (int i = 0; i < amount; i++)
        {
            Ball ball = Instantiate(ballPrefab, transform.position, Quaternion.identity).GetComponent<Ball>();
            ball.Initialize(direction, 500);
            ball.Launch();
        }
    }

    public void OpenContactWebsite(){
        Application.OpenURL("http://sanchow.itch.io");
    }

    public void ResetGame(){
        GameManager.instance.ResetGame();
    }


}
