using UnityEngine;
[CreateAssetMenu(fileName = "Color Objective")]
public class ColorObjectiveType : ObjectiveType
{
    public int colorIndex;

    public override void Initialize()
    {
        colorIndex = Random.Range(0, ColorBlockBehavior.ColorDictionary.Count);
    }

    public override bool HandleObjective(BlockBehavior blockBehavior)
    {
        ColorBlockBehavior colorBlockBehavior = (ColorBlockBehavior)blockBehavior;
        if (colorBlockBehavior.ColorIndex == colorIndex)
        {
            return true;
        }
        return false;
    }
}