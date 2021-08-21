using UnityEngine;
using System.Collections.Generic;

public class ColorBlockBehavior : BlockBehavior
{
    public enum BlockColors
    {
        Red,
        Blue,
        Green,
        Yellow,
        Purple,
        Orange,
        Pink
    }

    private static Color purple = new Color(0.5f, 0, .5f);
    private static Color orange = new Color(1f, .3f, 0);
    private static Color pink = new Color(1f, 0.3f, 1f);

    public static Dictionary<BlockColors, Color> ColorDictionary = new Dictionary<BlockColors, Color>()
    {
       {BlockColors.Red, Color.red},
       {BlockColors.Blue, Color.blue},
       {BlockColors.Green, Color.green},
       {BlockColors.Yellow, Color.yellow},
       {BlockColors.Purple, purple},
       {BlockColors.Orange, orange},
       {BlockColors.Pink, pink}
    };

    private MaterialPropertyBlock propertyBlock;
    private int colorIndex;

    public int ColorIndex
    {
        get { return colorIndex; }
    }

    protected override void Initialize()
    {
        Material mat = GetComponent<MeshRenderer>().material;
        colorIndex = Random.Range(0, ColorDictionary.Count);

        mat.color = ColorDictionary[(BlockColors)colorIndex];
    }

    public override void DestroySelfAndNeighborBlocks()
    {
        BlockBehavior[] leftBlocks = FindBlocksThroughRay(-transform.forward);
        BlockBehavior[] rightBlocks = FindBlocksThroughRay(transform.forward);
        BlockBehavior[] downBlocks = FindBlocksThroughRay(-transform.up);
        BlockBehavior[] upBlocks = FindBlocksThroughRay(transform.up);

        foreach (ColorBlockBehavior block in leftBlocks)
        {
            if (block.ColorIndex == ColorIndex)
                BlocksToDestroy.Add(block.gameObject.GetComponent<MeshDestroy>());
            else
                break;
        }
        foreach (ColorBlockBehavior block in rightBlocks)
        {
            if (block.ColorIndex == ColorIndex)
                BlocksToDestroy.Add(block.gameObject.GetComponent<MeshDestroy>());
            else
                break;
        }
        foreach (ColorBlockBehavior block in downBlocks)
        {
            if (block.ColorIndex == ColorIndex)
                BlocksToDestroy.Add(block.gameObject.GetComponent<MeshDestroy>());
            else
                break;
        }
        foreach (ColorBlockBehavior block in upBlocks)
        {
            if (block.ColorIndex == ColorIndex)
                BlocksToDestroy.Add(block.gameObject.GetComponent<MeshDestroy>());
            else
                break;
        }

        BlocksToDestroy.Add(this.GetComponent<MeshDestroy>());
    }

    public override void FindNeighborBlocksToDestroy()
    {
        IsSetToBeDestroyed = true;

        ColorBlockBehavior leftBlock = (ColorBlockBehavior)FindBlockThroughRay(-transform.forward, this);
        ColorBlockBehavior rightBlock = (ColorBlockBehavior)FindBlockThroughRay(transform.forward, this);
        ColorBlockBehavior upBlock = (ColorBlockBehavior)FindBlockThroughRay(transform.up, this);
        ColorBlockBehavior downBlock = (ColorBlockBehavior)FindBlockThroughRay(-transform.up, this);

        if (leftBlock != null && leftBlock.ColorIndex == ColorIndex && !leftBlock.IsSetToBeDestroyed)
        {
            leftBlock.FindNeighborBlocksToDestroy();
        }

        if (rightBlock != null && rightBlock.ColorIndex == ColorIndex && !rightBlock.IsSetToBeDestroyed)
        {
            rightBlock.FindNeighborBlocksToDestroy();
        }

        if (upBlock != null && upBlock.ColorIndex == ColorIndex && !upBlock.IsSetToBeDestroyed)
        {
            upBlock.FindNeighborBlocksToDestroy();
        }

        if (downBlock != null && downBlock.ColorIndex == ColorIndex && !downBlock.IsSetToBeDestroyed)
        {
            downBlock.FindNeighborBlocksToDestroy();
        }

        BlocksToDestroy.Add(GetComponent<MeshDestroy>());
    }
}