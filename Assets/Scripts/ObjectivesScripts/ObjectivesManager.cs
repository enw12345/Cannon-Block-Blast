using UnityEngine;

public class ObjectivesManager : MonoBehaviour
{
    public int maxObjectives = 4;

    private int totalObjectiveAmount = 0;
    public int TotalObjectiveAmount
    {
        get
        {
            if (totalObjectiveAmount == 0)
            {
                CalculateTotalObjectiveAmount();
                return totalObjectiveAmount;
            }
            else { return totalObjectiveAmount; }
        }
    }

    public ObjectiveTypes[] objectiveTypes;
    public ObjectiveTypes[] cuurrentObjectives;

    private void SelectRandomObjectives()
    {
        int objectives = 0;

        if (objectiveTypes.Length < maxObjectives)
            objectives = Random.Range(0, objectiveTypes.Length);
        else
            objectives = Random.Range(0, maxObjectives);

        for (int i = 0; i < objectives; i++)
        {
            cuurrentObjectives[i] = objectiveTypes[i];
        }
    }
    
    private void CalculateTotalObjectiveAmount()
    {
        totalObjectiveAmount = 0;

        foreach (ObjectiveTypes objective in cuurrentObjectives)
        {
            totalObjectiveAmount += objective.objectiveAmount;
        }
    }

    private void CalculateTotalObjectivesCompleted()
    {

    }

}
