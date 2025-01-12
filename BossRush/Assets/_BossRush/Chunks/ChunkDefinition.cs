using UnityEngine;

[CreateAssetMenu(fileName = "ChunkDefinition", menuName = "BossRush/Chunks/ChunkDefinition")]
public class ChunkDefinition : ScriptableObject
{
    public ChunkSetupOperation[] setupOperations;
}
