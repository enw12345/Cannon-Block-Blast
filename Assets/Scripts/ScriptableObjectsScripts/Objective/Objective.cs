using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Objective")]
[Serializable]
public class Objective : ScriptableObject
{
    public Sprite ObjectiveImage;
    public int objectiveAmount;
    public int objectiveAmountCompleted;

    public ObjectiveType objectiveType;

    public bool ObjectiveComplete
    {
        get { return objectiveAmount == objectiveAmountCompleted; }
    }

    public event EventHandler<OnObjectiveUpdatedEventArgs> OnObjectiveUpdated;
    public class OnObjectiveUpdatedEventArgs : EventArgs
    {
        public int objectiveAmountCompleted;
    }

    private void OnDestroy()
    {

    }

    public void Init(int objectiveAmountForThisLevel)
    {
        objectiveAmountCompleted = 0;
        objectiveAmount = objectiveAmountForThisLevel;

        objectiveType.Initialize();
    }

    public void DeInitialize()
    {
        objectiveAmountCompleted = 0;
    }

    public virtual void CheckObjectiveAmountCompleted(BlockType blockType)
    {
        if (objectiveType.HandleObjective(blockType))
        {
            objectiveAmountCompleted++;

            OnObjectiveUpdated?.Invoke(this, new OnObjectiveUpdatedEventArgs { objectiveAmountCompleted = objectiveAmountCompleted });
        }
    }
}