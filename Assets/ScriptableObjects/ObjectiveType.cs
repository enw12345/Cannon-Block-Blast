using UnityEngine;

[CreateAssetMenu(menuName = "Objective Type")]
public abstract class ObjectiveType : ScriptableObject
{
    public BlockType blockType;
    public abstract void Initialize();
    public abstract bool HandleObjective(BlockBehavior blockBehavior);
}
