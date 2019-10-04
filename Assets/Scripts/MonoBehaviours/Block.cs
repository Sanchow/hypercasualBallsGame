using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
public class Block : MonoBehaviour
{

    public TextMeshPro healthDisplayText;

    public UnityAction OnBlockDestroyed;

    public AudioClip hitSound;

    public AudioClip deathSound;

    public GameObject deathParticleSystem;

    private AudioSource audioSource;

    private int blockHealth;

    public int currentHealth{
        get{
            return blockHealth;
        }
    }

    private void Start() {
        healthDisplayText.text = blockHealth.ToString();
        audioSource = GetComponent<AudioSource>();
    }

    #region Public Methods
    public void GetHit(Ball ball){
        PlayHitSound();
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
        PlayDeathSound();
        SpawnDeathParticles();
        Destroy(gameObject);
    }

    void PlayHitSound(){
        audioSource.clip = hitSound;
        audioSource.Play();
    }

    void PlayDeathSound(){
        GameObject soundPlayer = new GameObject();
        AudioSource _audioSource = soundPlayer.AddComponent<AudioSource>();
        _audioSource.clip = deathSound;
        _audioSource.volume = 0.2f;
        _audioSource.Play();
        Destroy(soundPlayer, 1f);
    }

    void SpawnDeathParticles(){
        GameObject particleSystem = Instantiate(deathParticleSystem, transform.position, Quaternion.identity);
        Destroy(particleSystem, 1f);
    }

    #endregion
}
