using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class Repulsor : MonoBehaviour
{
    public float repulsorForce = 200f;
    public VisualEffect pullEffect;
    public AudioClip audioClip;


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
        if (other.TryGetComponent(out PlayerMovement playerMovement))
        {
            Debug.Log("Player detected");
            Vector3 repulsorDirection = (playerMovement.transform.position - transform.position).normalized;
            playerMovement.Push(repulsorDirection * repulsorForce);

            pullEffect.Play();
            if (audioClip != null)
            {
                AudioSource.PlayClipAtPoint(audioClip, transform.position);
            }
        }
    }
}
