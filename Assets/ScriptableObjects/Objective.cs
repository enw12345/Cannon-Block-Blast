using UnityEngine;

[CreateAssetMenu(menuName = "Objective")]
public class Objective : ScriptableObject
{
    public int objectiveAmount;
    public int objectiveAmountCompleted;

    public ObjectiveType objectiveType;
    private bool objectiveComplete = false;

    public bool ObjectiveComplete
    {
        get { return objectiveAmount == objectiveAmountCompleted; }
    }

    private void OnEnable()
    {
        objectiveAmountCompleted = 0;
    }

    public void Init()
    {
        objectiveAmountCompleted = 0;
        Grid.OnBlockDestroyed += IncreaseObjectiveAmountCompleted;
        objectiveType.Initialize();
    }

    protected virtual void IncreaseObjectiveAmountCompleted(object sender, Grid.OnBlockDestroyedEventArgs e)
    {
        Debug.Log("Checking Objective");
        if (e.blockType == objectiveType.blockType)
        {
            Debug.Log("Correct Objective");

            if (objectiveType.HandleObjective(e.blockBehavior1))
            {
                Debug.Log(objectiveAmountCompleted);
                objectiveAmountCompleted++;
            }
        }
    }
}