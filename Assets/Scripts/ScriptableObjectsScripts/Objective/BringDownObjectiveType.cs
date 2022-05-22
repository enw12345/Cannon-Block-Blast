using UnityEngine;

[CreateAssetMenu(fileName = "Bring Down Object Objective Type")]
public class BringDownObjectiveType : ObjectiveType
{
    public override void Initialize()
    {
        var fallDownBlockType = (FallDownBlockType) blockType;
        fallDownBlockType.IsTouchingGround = false;
    }

    public override bool HandleObjective(BlockType blockType)
    {
        if (this.blockType == blockType)
        {
            var fallDownBlockType = (FallDownBlockType) blockType;
            return fallDownBlockType.IsTouchingGround;
        }

        return false;
    }
}