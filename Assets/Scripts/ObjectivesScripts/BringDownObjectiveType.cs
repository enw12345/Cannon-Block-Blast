using UnityEngine;

[CreateAssetMenu(fileName = "Bring Down Object Objective Type")]
public class BringDownObjectiveType : ObjectiveType
{
    public override void Initialize()
    {
        return;
    }

    public override bool HandleObjective(BlockBehavior blockBehavior)
    {
        FallDownBlockBehavior fallDownBlock = (FallDownBlockBehavior)blockBehavior;

        if (fallDownBlock.IsTouchingGround) return true;

        return false;
    }
}