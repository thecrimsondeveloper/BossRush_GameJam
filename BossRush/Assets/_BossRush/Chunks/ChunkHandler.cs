using System.Collections.Generic;
using UnityEngine;

public class ChunkHandler : MonoBehaviour
{
    public Chunk chunkPrefab; // Reference to the chunk prefab
    public List<ChunkDefinition> chunkDefinitions; // List of chunk definitions
    public Player player; // Reference to the player
    public int chunkSize = 10; // Size of each chunk (in world units)
    public int activationDistance = 200; // Distance within which new chunks are spawned
    public List<Chunk> spawnedChunks = new List<Chunk>(); // Public list of all spawned chunks

    private HashSet<Vector2Int> spawnedChunkCoords = new HashSet<Vector2Int>();
    



    void Update()
    {
        SpawnChunksAroundPlayer();
    }

    private void SpawnChunksAroundPlayer()
    {
        Vector2Int playerChunkCoord = GetPlayerChunkCoord();

        // Calculate the radius in chunks based on the activation distance
        int chunkRadius = Mathf.CeilToInt(activationDistance / (float)chunkSize);

        for (int x = -chunkRadius; x <= chunkRadius; x++)
        {
            for (int y = -chunkRadius; y <= chunkRadius; y++)
            {
                Vector2Int chunkCoord = new Vector2Int(playerChunkCoord.x + x, playerChunkCoord.y + y);

                // Only spawn chunks that haven't already been spawned
                if (!spawnedChunkCoords.Contains(chunkCoord))
                {
                    SpawnChunk(chunkCoord);
                }
            }
        }
    }

    private Vector2Int GetPlayerChunkCoord()
    {
        return new Vector2Int(
            Mathf.FloorToInt(player.transform.position.x / chunkSize),
            Mathf.FloorToInt(player.transform.position.y / chunkSize)
        );
    }

    private void SpawnChunk(Vector2Int chunkCoord)
    {
        if (chunkDefinitions == null || chunkDefinitions.Count == 0)
        {
            Debug.LogError("Chunk definitions are not set!");
            return;
        }

        ChunkDefinition chunkDefinition = chunkDefinitions[Random.Range(0, chunkDefinitions.Count)];
        Chunk chunk = Instantiate(
            chunkPrefab,
            new Vector3(chunkCoord.x * chunkSize, chunkCoord.y * chunkSize, 0),
            Quaternion.identity
        );
        (chunk as IInitializable<ChunkHandler, ChunkDefinition>)?.Initialize(this, chunkDefinition);

        spawnedChunks.Add(chunk);
        spawnedChunkCoords.Add(chunkCoord);
    }
}
