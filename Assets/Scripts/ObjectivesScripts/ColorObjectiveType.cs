using UnityEngine;
[CreateAssetMenu(fileName = "Color Objective")]
public class ColorObjectiveType : ObjectiveType
{
    public int colorIndex;
    public Color colorTarget;

    public override void Initialize()
    {
        colorIndex = Random.Range(0, ColorBlockBehavior.ColorDictionary.Count);
        Debug.Log(string.Format("The color to destroy is: {0}",
       colorTarget = ColorBlockBehavior.ColorDictionary[(ColorBlockBehavior.BlockColors)colorIndex]));
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