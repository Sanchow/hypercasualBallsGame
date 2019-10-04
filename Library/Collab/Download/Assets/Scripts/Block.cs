using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
public class Block : MonoBehaviour
{

    public TextMeshPro healthDisplayText;

    public UnityAction OnBlockDestroyed;

    [SerializeField]
    private int blockHealth;

    public int currentHealth{
        get{
            return blockHealth;
        }
    }

    private void Start() {
        healthDisplayText.text = blockHealth.ToString();
    }

    #region Public Methods
    public void GetHit(Ball ball){
        TakeDamage();
    }

    public void Initialize(int health){
        if(health >= 0){
            blockHealth = health;
        } else {
            Destroy(gameObject);
        }
    }

    #endregion
    
    #region Private Methods
    void TakeDamage(){
        blockHealth--;
        healthDisplayText.text = blockHealth.ToString();
        if(blockHealth <= 0){
            DestroyBlock();
        }
    }

    void DestroyBlock(){
        if(OnBlockDestroyed != null){
            OnBlockDestroyed.Invoke();
        }
        Destroy(gameObject);
    }

    #endregion
}
