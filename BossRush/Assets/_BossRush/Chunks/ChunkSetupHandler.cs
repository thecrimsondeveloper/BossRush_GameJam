using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChunkSetupHandler : MonoBehaviour, IInitializable<Chunk, ChunkDefinition>
{
    public UnityEvent<Chunk, ChunkDefinition> OnInitialized = new UnityEvent<Chunk, ChunkDefinition>();
    public UnityEvent<Chunk, ChunkDefinition> onInitialized => OnInitialized;

    

    public void OnInitialize(Chunk data1, ChunkDefinition data2)
    {
        foreach(ChunkSetupOperation operation in data2.setupOperations)
        {
            (operation as IInitializable<Chunk, ChunkDefinition>).Initialize(data1, data2);        
        }
    }

}
