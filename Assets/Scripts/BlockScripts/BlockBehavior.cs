using System;
using System.Collections.Generic;
using System.Linq;
using DestructionScripts;
using UnityEngine;

namespace BlockScripts
{
    public abstract class BlockBehavior : MonoBehaviour
    {
        public static readonly List<BlockBehavior> BlocksToDestroy = new List<BlockBehavior>();
        [SerializeField] private LayerMask layerMask = 0;

        [SerializeField] protected BlockType blockType;

        protected bool IsSetToBeDestroyed { get; set; }

        protected Transform BlockTransform;
        private static int SpaceBetweenEachBlock
        {
            set => SpaceBetweenEachBlock = value;
        }

        public BlockType BlockType => blockType;

        public void InitializeBlock()
        {
            Initialize();
            BlockTransform = transform;
        }

        public static event EventHandler<OnBlockDestroyedEventArgs> OnBlockDestroyed;

        public virtual void DestroyBlock()
        {
            OnBlockDestroyed?.Invoke(this, new OnBlockDestroyedEventArgs {blockBehavior1 = this});
            GetComponent<MeshDestroy>().DestroyMesh(2);
        }

        protected abstract void Initialize();

        public abstract void DestroySelfAndNeighborBlocks();

        public abstract void FindNeighborBlocksToDestroy();

        public virtual void FindNeighborBlocksToDestroyRowsAndColumns()
        {
            var transformUp = BlockTransform.up;
            var transformForward = BlockTransform.forward;
        
            var leftBlocks = FindBlocksThroughRay(-transformForward);
            var rightBlocks = FindBlocksThroughRay(transformForward);
            var downBlocks = FindBlocksThroughRay(-transformUp);
            var upBlocks = FindBlocksThroughRay(transformUp);

            foreach (var block in leftBlocks) BlocksToDestroy.Add(block);
            foreach (var block in rightBlocks) BlocksToDestroy.Add(block);
            foreach (var block in downBlocks) BlocksToDestroy.Add(block);
            foreach (var block in upBlocks) BlocksToDestroy.Add(block);

            BlocksToDestroy.Add(this);
        }

        protected BlockBehavior FindBlockThroughRay(Vector3 castDirection)
        {
            if (!Physics.Raycast(transform.position, castDirection, out var hit, Mathf.Infinity, layerMask)) return null;
            if (hit.collider.gameObject.GetComponent<BlockBehavior>() &&
                VerifyBlockType(hit.collider.gameObject.GetComponent<BlockBehavior>().BlockType))
                return hit.collider.gameObject.GetComponent<BlockBehavior>();

            return null;
        }

        protected IEnumerable<BlockBehavior> FindBlocksThroughRay(Vector3 castDirection)
        {
            var hits = Physics.RaycastAll(transform.position, castDirection, Mathf.Infinity, layerMask).OrderBy(h => h.distance)
                .ToArray();

            var blocks = new List<BlockBehavior>(hits.Length);

            for (var i = 0; i < hits.Length; i++)
            {
                var potentialBlock = hits[i].collider.gameObject.GetComponent<BlockBehavior>();

                if (i == 0 && hits[i].distance > 2.5f || !VerifyBlockType(potentialBlock.BlockType))
                    break;
                blocks.Add(hits[i].collider.gameObject.GetComponent<BlockBehavior>());
            }

            return blocks;
        }

        private bool VerifyBlockType(BlockType blockTypeToCheck)
        {
            return blockType == blockTypeToCheck;
        }

        public class OnBlockDestroyedEventArgs : EventArgs
        {
            public BlockBehavior blockBehavior1;
        }
    }
}