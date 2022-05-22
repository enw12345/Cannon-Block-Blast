using System.Collections.Generic;
using UnityEngine;

namespace ColorDictionary
{
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

        private static readonly Color Purple = new Color(0.5f, 0, .5f);
        private static readonly Color Orange = new Color(1f, .3f, 0);
        private static readonly Color Pink = new Color(1f, 0.3f, 1f);

        public static readonly Dictionary<BlockColors, Color> colorDictionary = new Dictionary<BlockColors, Color>
        {
            {BlockColors.Red, Color.red},
            {BlockColors.Blue, Color.blue},
            {BlockColors.Green, Color.green},
            {BlockColors.Yellow, Color.yellow},
            {BlockColors.Purple, Purple},
            {BlockColors.Orange, Orange},
            {BlockColors.Pink, Pink}
        };
    }
}