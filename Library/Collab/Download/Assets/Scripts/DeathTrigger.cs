using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{

    private LevelManager levelManager;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Block")){
            levelManager.ChangeState(LevelState.LOST);
            Debug.Log("You have lost the game");
        }
    }

    
}
