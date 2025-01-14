using Unity.VisualScripting;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    [SerializeField] private Player trackedPlayer;

    [SerializeField] private float maxDistance = 20f; // Maximum distance for the pull to have an effect
    [SerializeField] private float pullForce = 10f;

    [Header("Pull Interval")]
    [SerializeField] private float pullIntervalMin = 0.25f;
    [SerializeField] private float pullIntervalMax = 0.5f;

    private float timeUntilNextPull = 0f;

    void FixedUpdate()
    {
        if (timeUntilNextPull <= 0)
        {
            Pull();
            timeUntilNextPull = Random.Range(pullIntervalMin, pullIntervalMax);
        }
        else
        {
            timeUntilNextPull -= Time.fixedDeltaTime;
        }
    }

    void Pull()
    {
        if (trackedPlayer != null)
        {
            // Calculate the distance between the player and the black hole
            float distance = Vector3.Distance(trackedPlayer.transform.position, transform.position);

            // Only pull the player if they are within the maximum distance
            if (distance <= maxDistance)
            {
                // Calculate the pull strength based on distance (linear falloff)
                float adjustedForce = pullForce * (1 - (distance / maxDistance));

                // Apply the pull
                trackedPlayer.playerMovement.PushTowards(transform.position, adjustedForce);
            }
        }
    }
}
