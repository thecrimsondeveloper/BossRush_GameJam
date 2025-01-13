using UnityEngine;

[CreateAssetMenu(fileName = "SpawnRandomPrefab", menuName = "Boss Rush/Chunks/Chunk Setup Operations/Spawn Random Prefab")]
public class SpawnRandomPrefab : ChunkSetupOperation
{
    public GameObject ObjectToSpawn;
    public int amountToSpawn;

    public override void OnInitialize(Chunk chunk, ChunkDefinition definition)
    {
        for (int i = 0; i < amountToSpawn; i++)
        {
            GameObject spawnedObject = Instantiate(ObjectToSpawn, chunk.transform);
            spawnedObject.transform.localPosition = chunk.GetRandomPointInChunk();
        }
    }
}
