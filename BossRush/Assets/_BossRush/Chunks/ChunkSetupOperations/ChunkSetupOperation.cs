using UnityEngine;
using UnityEngine.Events;


public abstract class ChunkSetupOperation : ScriptableObject, IInitializable<Chunk, ChunkDefinition>
{
     public UnityEvent<Chunk, ChunkDefinition> onInitialized { get; } = new UnityEvent<Chunk, ChunkDefinition>();

    public abstract void OnInitialize(Chunk chunk, ChunkDefinition currentDefinition);
        

}

