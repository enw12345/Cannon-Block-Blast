using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveTypes : ScriptableObject
{
    public enum objectiveType
    {
        DestroyColorBlocks,
        BringDownObject,
        MakeCertainAmountOfMatches,
    }

    public int objectiveAmount;
    public objectiveType type;
}
