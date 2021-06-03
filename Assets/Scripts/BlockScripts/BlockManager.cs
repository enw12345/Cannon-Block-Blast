using UnityEngine;
using System.Collections.Generic;

public class BlockManager : MonoBehaviour
{
    private static BlockManager instance;

    public static BlockManager Instance { get { return instance; } }

    public static List<BlockBehavior> Blocks = new List<BlockBehavior>();

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

    public static BlockColors blockColors;

    public Dictionary<BlockColors, Color> ColorDictionary = new Dictionary<BlockColors, Color>();

    public static List<MeshDestroy> BlocksToDestroy = new List<MeshDestroy>();

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        ColorDictionary.Add(BlockColors.Red, Color.red);
        ColorDictionary.Add(BlockColors.Blue, Color.blue);
        ColorDictionary.Add(BlockColors.Green, Color.green);
        ColorDictionary.Add(BlockColors.Yellow, Color.yellow);

        Color purple = new Color(0.5f, 0, .5f);
        Color orange = new Color(1f, .3f, 0);
        Color pink = new Color(1f, 0.3f, 1f);

        ColorDictionary.Add(BlockColors.Purple, purple);
        ColorDictionary.Add(BlockColors.Orange, orange);
        ColorDictionary.Add(BlockColors.Pink, pink);
    }

    private void FixedUpdate()
    {
        if (BlocksToDestroy.Count != 0)
        {
            foreach (MeshDestroy block in BlocksToDestroy)
            {
                if (block != null)
                    block.DestroyMesh(2);
            }

            BlocksToDestroy.Clear();
        }
    }

}