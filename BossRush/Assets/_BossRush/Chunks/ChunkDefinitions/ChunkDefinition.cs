using UnityEngine;

[CreateAssetMenu(fileName = "ChunkDefinition", menuName = "Boss Rush/Chunks/ChunkDefinition")]
public class ChunkDefinition : ScriptableObject
{
    public ChunkSetupOperation[] setupOperations;
}
