using UnityEngine;

public class ColorBlockBehavior : BlockBehavior
{
    private int colorIndex;

    public int ColorIndex
    {
        get { return colorIndex; }
    }

    private MaterialPropertyBlock propertyBlock;

    protected override void Initialize()
    {
        Material mat = GetComponent<MeshRenderer>().material;
        colorIndex = Random.Range(0, ColorDictionary.colorDictionary.Count);

        mat.color = ColorDictionary.colorDictionary[(ColorDictionary.BlockColors)colorIndex];
    }

    public override void DestroyBlock()
    {
        ColorBlockType colorBlockType = (ColorBlockType)blockType;
        colorBlockType.colorIndex = colorIndex;

        base.DestroyBlock();
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
                BlocksToDestroy.Add(block);
            else
                break;
        }
        foreach (ColorBlockBehavior block in rightBlocks)
        {
            if (block.ColorIndex == ColorIndex)
                BlocksToDestroy.Add(block);
            else
                break;
        }
        foreach (ColorBlockBehavior block in downBlocks)
        {
            if (block.ColorIndex == ColorIndex)
                BlocksToDestroy.Add(block);
            else
                break;
        }
        foreach (ColorBlockBehavior block in upBlocks)
        {
            if (block.ColorIndex == ColorIndex)
                BlocksToDestroy.Add(block);
            else
                break;
        }

        BlocksToDestroy.Add(this);
    }

    public override void FindNeighborBlocksToDestroy()
    {
        IsSetToBeDestroyed = true;

        ColorBlockBehavior leftBlock = (ColorBlockBehavior)FindBlockThroughRay(-transform.forward);
        ColorBlockBehavior rightBlock = (ColorBlockBehavior)FindBlockThroughRay(transform.forward);
        ColorBlockBehavior upBlock = (ColorBlockBehavior)FindBlockThroughRay(transform.up);
        ColorBlockBehavior downBlock = (ColorBlockBehavior)FindBlockThroughRay(-transform.up);

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

        BlocksToDestroy.Add(this);
    }
}