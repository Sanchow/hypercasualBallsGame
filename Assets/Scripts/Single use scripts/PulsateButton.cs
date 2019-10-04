using UnityEngine;

public class PulsateButton : MonoBehaviour
{
    public Animator buttonAnimator;

    private void OnCollisionEnter2D(Collision2D other) {
        buttonAnimator.SetTrigger("Hit");
    }
}
