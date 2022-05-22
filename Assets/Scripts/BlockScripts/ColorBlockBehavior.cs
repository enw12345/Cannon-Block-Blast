using UnityEngine;

namespace BlockScripts
{
    public class ColorBlockBehavior : BlockBehavior
    {
        private MaterialPropertyBlock propertyBlock;

        private int ColorIndex { get; set; }

        protected override void Initialize()
        {
            var mat = GetComponent<MeshRenderer>().material;
            ColorIndex = Random.Range(0, ColorDictionary.ColorDictionary.colorDictionary.Count);
            mat.color = ColorDictionary.ColorDictionary.colorDictionary[(ColorDictionary.ColorDictionary.BlockColors) ColorIndex];
        }

        public override void DestroyBlock()
        {
            var colorBlockType = (ColorBlockType) blockType;
            colorBlockType.colorIndex = ColorIndex;

            base.DestroyBlock();
        }

        public override void DestroySelfAndNeighborBlocks()
        {
            var transformUp = BlockTransform.up;
            var transformForward = BlockTransform.forward;
            
            var leftBlocks = FindBlocksThroughRay(-transformForward);
            var rightBlocks = FindBlocksThroughRay(transformForward);
            var downBlocks = FindBlocksThroughRay(-transformUp);
            var upBlocks = FindBlocksThroughRay(transformUp);

            foreach (var blockBehavior in leftBlocks)
            {
                var block = (ColorBlockBehavior) blockBehavior;
                if (block.ColorIndex == ColorIndex)
                    BlocksToDestroy.Add(block);
                else
                    break;
            }

            foreach (var blockBehavior in rightBlocks)
            {
                var block = (ColorBlockBehavior) blockBehavior;
                if (block.ColorIndex == ColorIndex)
                    BlocksToDestroy.Add(block);
                else
                    break;
            }

            foreach (var blockBehavior in downBlocks)
            {
                var block = (ColorBlockBehavior) blockBehavior;
                if (block.ColorIndex == ColorIndex)
                    BlocksToDestroy.Add(block);
                else
                    break;
            }

            foreach (var blockBehavior in upBlocks)
            {
                var block = (ColorBlockBehavior) blockBehavior;
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
            var transformUp = BlockTransform.up;
            var transformForward = BlockTransform.forward;
            
            var leftBlock = (ColorBlockBehavior) FindBlockThroughRay(-transformForward);
            var rightBlock = (ColorBlockBehavior) FindBlockThroughRay(transformForward);
            var upBlock = (ColorBlockBehavior) FindBlockThroughRay(transformUp);
            var downBlock = (ColorBlockBehavior) FindBlockThroughRay(-transformUp);

            if (leftBlock != null && leftBlock.ColorIndex == ColorIndex && !leftBlock.IsSetToBeDestroyed)
                leftBlock.FindNeighborBlocksToDestroy();

            if (rightBlock != null && rightBlock.ColorIndex == ColorIndex && !rightBlock.IsSetToBeDestroyed)
                rightBlock.FindNeighborBlocksToDestroy();

            if (upBlock != null && upBlock.ColorIndex == ColorIndex && !upBlock.IsSetToBeDestroyed)
                upBlock.FindNeighborBlocksToDestroy();

            if (downBlock != null && downBlock.ColorIndex == ColorIndex && !downBlock.IsSetToBeDestroyed)
                downBlock.FindNeighborBlocksToDestroy();

            BlocksToDestroy.Add(this);
        }
    }
}