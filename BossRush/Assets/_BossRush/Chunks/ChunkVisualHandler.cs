using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

public class ChunkVisualHandler : MonoBehaviour, IInitializable<Chunk, ChunkDefinition>
{
    public UnityEvent<Chunk, ChunkDefinition> onInitialized { get; } = new UnityEvent<Chunk, ChunkDefinition>();
    [SerializeField] VisualEffect gridVisualEffect;
    Player player = null;
    
    public void OnInitialize(Chunk chunk, ChunkDefinition data2)
    {
        chunk.onPlayerEntered.AddListener(OnPlayerEnteredChunk);
        OnPlayerEnteredChunk(chunk.chunkHandler.player, chunk);
    }

    void OnPlayerEnteredChunk(Player player,Chunk chunk)
    {
        this.player = player;
    }

    void Update()
    {
        if (player != null)
        {
            gridVisualEffect.SetVector3("PlayerPosition", player.transform.position);
        }
    }
}

