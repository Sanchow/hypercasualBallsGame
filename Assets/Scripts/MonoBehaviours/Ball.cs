using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ball : MonoBehaviour
{
        
    public UnityAction OnBallDestroyed;

    public UnityAction OnBallTakingTooLong;

    private Rigidbody2D rb;

    private Vector2 direction;

    private float speed;

    private int timesHit = 0;


    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(Vector2 _direction, float _speed){
        direction = _direction;
        speed = _speed;
    }

    public void Launch(){
        rb.AddForce(speed * direction);
    }

    public void Die(){
        if(OnBallDestroyed != null){
            OnBallDestroyed.Invoke();
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other) {


        if(other.gameObject.CompareTag("Block")){
            Block block = other.gameObject.GetComponent<Block>();
            if(block != null){
                block.GetHit(this);
            }
        }
        timesHit ++;
        //Prevent balls from getting stuck forever
        if(timesHit >= 50){
            rb.AddForce(new Vector2(1, -1));
        }
        if(timesHit >= 40){
            if(OnBallTakingTooLong != null){
                OnBallTakingTooLong.Invoke();
            }
        }
    }
}
