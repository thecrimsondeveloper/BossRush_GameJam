using UnityEngine;
using UnityEngine.VFX;

public class Bomb : MonoBehaviour
{
    public float repulsorForce = 200f;
    public int damage = 1;
    public VisualEffect explodeEffect;
    public AudioClip audioClip;


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
        if (other.TryGetComponent(out Player playerMovement))
        {
            Debug.Log("Player detected");
            Vector3 repulsorDirection = (playerMovement.transform.position - transform.position).normalized;
            playerMovement.playerMovement.Push(repulsorDirection * repulsorForce);

            playerMovement.TakeDamage(damage);

            explodeEffect.Play();
            Destroy(gameObject,1f);
            if (audioClip != null)
            {
                AudioSource.PlayClipAtPoint(audioClip, transform.position);
            }
        }
    }
}
