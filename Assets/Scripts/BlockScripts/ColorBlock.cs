using UnityEngine;

public class ColorBlock : BlockBehavior
{
    private MaterialPropertyBlock propertyBlock;
    private int colorIndex;

    public int ColorIndex
    {
        get { return colorIndex; }
    }

    public override void Initialize()
    {
        Material mat = GetComponent<MeshRenderer>().material;
        colorIndex = Random.Range(0,
        BlockManager.Instance.ColorDictionary.Count);

        mat.color = BlockManager.Instance.ColorDictionary[(BlockManager.BlockColors)colorIndex];
    }

    public override void DestroySelfAndNeighborBlocks()
    {
        BlockBehavior[] leftBlocks = FindBlocksThroughRay(-transform.forward);
        BlockBehavior[] rightBlocks = FindBlocksThroughRay(transform.forward);
        BlockBehavior[] downBlocks = FindBlocksThroughRay(-transform.up);
        BlockBehavior[] upBlocks = FindBlocksThroughRay(transform.up);

        foreach (ColorBlock block in leftBlocks)
        {
            if (block.ColorIndex == ColorIndex)
                BlockManager.BlocksToDestroy.Add(block.gameObject.GetComponent<MeshDestroy>());
            else
                break;
        }
        foreach (ColorBlock block in rightBlocks)
        {
            if (block.ColorIndex == ColorIndex)
                BlockManager.BlocksToDestroy.Add(block.gameObject.GetComponent<MeshDestroy>());
            else
                break;
        }
        foreach (ColorBlock block in downBlocks)
        {
            if (block.ColorIndex == ColorIndex)
                BlockManager.BlocksToDestroy.Add(block.gameObject.GetComponent<MeshDestroy>());
            else
                break;
        }
        foreach (ColorBlock block in upBlocks)
        {
            if (block.ColorIndex == ColorIndex)
                BlockManager.BlocksToDestroy.Add(block.gameObject.GetComponent<MeshDestroy>());
            else
                break;
        }

        BlockManager.BlocksToDestroy.Add(this.GetComponent<MeshDestroy>());
    }

    public override void FindNeighborBlocksToDestroy()
    {
         IsSetToBeDestroyed = true;

        ColorBlock leftBlock = (ColorBlock)FindBlockThroughRay(-transform.forward);
        ColorBlock rightBlock = (ColorBlock)FindBlockThroughRay(transform.forward);
        ColorBlock upBlock = (ColorBlock)FindBlockThroughRay(transform.up);
        ColorBlock downBlock = (ColorBlock)FindBlockThroughRay(-transform.up);

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

        BlockManager.BlocksToDestroy.Add(GetComponent<MeshDestroy>());
    }
}