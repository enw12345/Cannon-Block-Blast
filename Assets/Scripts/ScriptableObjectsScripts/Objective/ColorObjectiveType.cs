using UnityEngine;

[CreateAssetMenu(fileName = "Color Objective")]
public class ColorObjectiveType : ObjectiveType
{
    public int colorIndex;
    public Color colorTarget;

    public override void Initialize()
    {
        colorIndex = Random.Range(0, ColorDictionary.ColorDictionary.colorDictionary.Count);
        colorTarget = ColorDictionary.ColorDictionary.colorDictionary[(ColorDictionary.ColorDictionary.BlockColors) colorIndex];
    }

    public override bool HandleObjective(BlockType blockType)
    {
        if (this.blockType == blockType)
        {
            var colorBlockType = (ColorBlockType) blockType;

            return colorBlockType.colorIndex == colorIndex;
        }

        return false;
    }
}