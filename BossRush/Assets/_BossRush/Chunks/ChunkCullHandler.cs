using System.Collections.Generic;
using UnityEngine;

public class ChunkCullHandler : MonoBehaviour
{
    public ChunkHandler chunkHandler;
    public Transform player; // Reference to the player
    public float cullingDistance = 200f; // Distance beyond which chunks are culled
    public float cullInterval = 0.5f; // Interval at which chunks are culled

    float lastTimeChecked = 0f;
    void Update()
    {
        if (Time.time - lastTimeChecked >= cullInterval)
        {
            CullChunks();
            lastTimeChecked = Time.time;
        }
    }

    private void CullChunks()
    {
        foreach (Chunk chunk in chunkHandler.spawnedChunks)
        {
            if (chunk == null) continue;

            float distanceToPlayer = Vector2.Distance(
                new Vector2(player.position.x, player.position.y),
                new Vector2(chunk.transform.position.x, chunk.transform.position.y)
            );

            chunk.gameObject.SetActive(distanceToPlayer <= cullingDistance);
        }
    }
}
