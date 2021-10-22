using UnityEngine;
[CreateAssetMenu(fileName = "Color Objective")]
public class ColorObjectiveType : ObjectiveType
{
    public int colorIndex;
    public Color colorTarget;

    public override void Initialize()
    {
        colorIndex = Random.Range(0, ColorDictionary.colorDictionary.Count);
        colorTarget = ColorDictionary.colorDictionary[(ColorDictionary.BlockColors)colorIndex];
    }

    public override bool HandleObjective(BlockType blockType)
    {
        if (this.blockType == blockType)
        {
            ColorBlockType colorBlockType = (ColorBlockType)blockType;
            // ColorBlockType objectiveColorBlock = (ColorBlockType)this.blockType;
            Debug.Log((colorBlockType.colorIndex == colorIndex) + " " + " " + colorBlockType.colorIndex + " " + colorIndex);

            return (colorBlockType.colorIndex == colorIndex);
        }

        return false;
    }
}