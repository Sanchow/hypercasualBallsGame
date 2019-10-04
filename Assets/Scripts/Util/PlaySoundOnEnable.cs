using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundOnEnable : MonoBehaviour
{
    public AudioClip clipToPlay;

    private AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clipToPlay;
        audioSource.Play();
    }
}
