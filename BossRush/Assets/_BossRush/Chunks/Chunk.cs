using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

public class Chunk : MonoBehaviour, IInitializable<ChunkHandler, ChunkDefinition>
{
    public enum PlayerInteractionState
    {
        None,
        Entered,
        Exited
    }

    public UnityEvent<ChunkHandler, ChunkDefinition> onInitialized { get; } = new UnityEvent<ChunkHandler, ChunkDefinition>();

    public UnityEvent<Player, Chunk> onPlayerEntered = new UnityEvent<Player, Chunk>();
    public ChunkHandler chunkHandler;
    public ChunkSetupHandler chunkSetupHandler;
    public ChunkVisualHandler chunkVisualHandler;

    public void OnInitialize(ChunkHandler data1, ChunkDefinition data2)
    {
        chunkHandler = data1;
        (chunkSetupHandler as IInitializable<Chunk, ChunkDefinition>).Initialize(this, data2);
        (chunkVisualHandler as IInitializable<Chunk, ChunkDefinition>).Initialize(this, data2);

        Debug.Log("Chunk Initialized");

    }

    public Vector3 GetRandomPointInChunk()
    {
        int randomX = UnityEngine.Random.Range(0, chunkHandler.chunkSize) - chunkHandler.chunkSize / 2;
        int randomY = UnityEngine.Random.Range(0, chunkHandler.chunkSize) - chunkHandler.chunkSize / 2;
        Vector2Int randomPoint = new Vector2Int(randomX, randomY);


        
        return new Vector3(randomPoint.x, randomPoint.y,0);

    

    }
 

}
