using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Objective")]
public class Objective : ScriptableObject
{
    public Sprite ObjectiveImage;
    public int objectiveAmount;
    public int objectiveAmountCompleted;

    public ObjectiveType objectiveType;

    protected static int maxObjectiveAmount = 15;
    protected static int minObjectiveAmount = 10;

    private bool objectiveComplete = false;

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

    public void Init()
    {
        objectiveAmountCompleted = 0;

        objectiveType.Initialize();

        objectiveAmount = UnityEngine.Random.Range(minObjectiveAmount, maxObjectiveAmount);

        Grid.OnBlockDestroyed += CheckObjectiveAmountCompleted;
    }

    protected virtual void CheckObjectiveAmountCompleted(object sender, Grid.OnBlockDestroyedEventArgs e)
    {
        Debug.Log("Checking Objective");
        if (objectiveType.HandleObjective(e.blockBehavior1))
        {
            Debug.Log(objectiveAmountCompleted);
            objectiveAmountCompleted++;

            OnObjectiveUpdated?.Invoke(this, new OnObjectiveUpdatedEventArgs { objectiveAmountCompleted = objectiveAmountCompleted });
        }
    }
}