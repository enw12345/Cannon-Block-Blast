using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorDictionary
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

    public static Dictionary<BlockColors, Color> colorDictionary = new Dictionary<BlockColors, Color>()
    {
       {BlockColors.Red, Color.red},
       {BlockColors.Blue, Color.blue},
       {BlockColors.Green, Color.green},
       {BlockColors.Yellow, Color.yellow},
       {BlockColors.Purple, purple},
       {BlockColors.Orange, orange},
       {BlockColors.Pink, pink}
    };

}
