using UnityEngine;

public abstract class ObjectiveType : ScriptableObject
{
    public BlockType blockType;
    public abstract void Initialize();
    public abstract bool HandleObjective(BlockType blockType);
}
