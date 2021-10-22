using UnityEngine;

[CreateAssetMenu(fileName = "Bring Down Object Objective Type")]
public class BringDownObjectiveType : ObjectiveType
{
    public override void Initialize()
    {
        return;
    }

    public override bool HandleObjective(BlockType blockType)
    {
        if (this.blockType == blockType)
        {
            FallDownBlockType fallDownBlockType = (FallDownBlockType)blockType;

            if (fallDownBlockType.IsTouchingGround) return true;
        }

        return false;
    }
}