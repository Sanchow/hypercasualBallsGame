using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallKiller : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Ball")){
            Ball ball = other.gameObject.GetComponent<Ball>();
            if(ball != null){
                ball.Die();
            }
        }
    }
}
