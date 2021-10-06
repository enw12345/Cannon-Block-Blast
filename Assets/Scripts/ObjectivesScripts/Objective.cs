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

    private void OnEnable()
    {
        objectiveAmountCompleted = 0;
    }

    public void Init(int objectiveAmountForThisLevel)
    {
        objectiveAmountCompleted = 0;
        objectiveAmount = objectiveAmountForThisLevel;

        objectiveType.Initialize();

        Grid.OnBlockDestroyed += CheckObjectiveAmountCompleted;
    }

    protected virtual void CheckObjectiveAmountCompleted(object sender, Grid.OnBlockDestroyedEventArgs e)
    {
        if (objectiveType.HandleObjective(e.blockBehavior1))
        {
            objectiveAmountCompleted++;

            OnObjectiveUpdated?.Invoke(this, new OnObjectiveUpdatedEventArgs { objectiveAmountCompleted = objectiveAmountCompleted });
        }
    }
}