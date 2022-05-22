using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Objective")]
[Serializable]
public class Objective : ScriptableObject
{
    public Sprite ObjectiveImage;
    public int objectiveAmountToComplete;
    public int objectiveAmountCompleted;

    public ObjectiveType objectiveType;

    public bool ObjectiveComplete => objectiveAmountToComplete == objectiveAmountCompleted;

    public event EventHandler<OnObjectiveUpdatedEventArgs> OnObjectiveUpdated;

    public void Init(int objectiveAmountForThisLevel)
    {
        objectiveAmountCompleted = 0;
        objectiveAmountToComplete = objectiveAmountForThisLevel;

        objectiveType.Initialize();
    }

    public void DeInitialize()
    {
        objectiveAmountCompleted = 0;
    }

    public void CheckObjectiveAmountCompleted(BlockType blockType)
    {
        objectiveType.HandleObjective(blockType);


        if (!objectiveType.HandleObjective(blockType)) return;
        objectiveAmountCompleted++;

        OnObjectiveUpdated?.Invoke(this,
            new OnObjectiveUpdatedEventArgs {objectiveAmountCompleted = objectiveAmountCompleted});
    }

    public class OnObjectiveUpdatedEventArgs : EventArgs
    {
        public int objectiveAmountCompleted;
    }
}