using UnityEngine;
[CreateAssetMenu(fileName = "Color Objective")]
public class ColorObjectiveType : ObjectiveType
{
    public int colorIndex;
    public Color colorTarget;

    public override void Initialize()
    {
        colorIndex = Random.Range(0, ColorBlockBehavior.ColorDictionary.Count);
        colorTarget = ColorBlockBehavior.ColorDictionary[(ColorBlockBehavior.BlockColors)colorIndex];
    }

    public override bool HandleObjective(BlockBehavior blockBehavior)
    {
        if (blockBehavior is ColorBlockBehavior)
        {
            ColorBlockBehavior colorBlockBehavior = (ColorBlockBehavior)blockBehavior;

            return (colorBlockBehavior.ColorIndex == colorIndex);
        }

        return false;
    }
}