using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ChunkSetupOperation", menuName = "BossRush/Chunks/ChunkSetupOperation")]
public class ChunkSetupOperation : ScriptableObject, IInitializable<Chunk, ChunkDefinition>
{
    public UnityEvent<Chunk, ChunkDefinition> onInitialized { get; } = new UnityEvent<Chunk, ChunkDefinition>();

    public GameObject ObjectToSpawn;
    public int amountToSpawn;

    public void OnInitialize(Chunk chunk, ChunkDefinition definition)
    {
        for (int i = 0; i < amountToSpawn; i++)
        {
            GameObject spawnedObject = Instantiate(ObjectToSpawn, chunk.transform);
            spawnedObject.transform.localPosition = chunk.GetRandomPointInChunk();
       }
    }
}

