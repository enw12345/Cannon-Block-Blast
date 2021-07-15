using System;
using UnityEngine;
using System.Collections.Generic;

public class ObjectivesManager : MonoBehaviour
{
    public int maxObjectives = 4;

    private int totalObjectives = 0;

    private int totalCompletedObjecives;

    public Objective[] objectives;

    [NonSerialized]
    public List<Objective> currentObjectives = new List<Objective>();

    private bool objectivesComplete = false;
    public bool ObjectivesComplete
    {
        get { return objectivesComplete; }
    }

    //public event EventHandler<OnInitEventArgs> OnInit;
    public event EventHandler OnInit;

    public void InitializeObjectives()
    {
        SelectRandomObjectives();
        foreach (Objective objective in currentObjectives)
        {
            objective.Init();
            objective.OnObjectiveUpdated += CalculateTotalObjectivesCompleted;
            totalObjectives++;
        }

        OnInit?.Invoke(this, EventArgs.Empty);

        Debug.Log(currentObjectives.Count);

        objectivesComplete = false;
    }

    public void InitializeObjectives(Objective[] objectives)
    {
        foreach (Objective objective in objectives)
        {
            currentObjectives.Add(objective);
            objective.Init();
            objective.OnObjectiveUpdated += CalculateTotalObjectivesCompleted;
            totalObjectives++;
        }

        OnInit?.Invoke(this, EventArgs.Empty);

        //        Debug.Log(currentObjectives.Count);

        objectivesComplete = false;
    }

    // private void SelectRandomObjectives()
    // {
    //     int o = 0;

    //     if (objectives.Length < maxObjectives)
    //         o = UnityEngine.Random.Range(1, objectives.Length);
    //     else
    //         o = UnityEngine.Random.Range(1, maxObjectives);

    //     currentObjectives.Capacity = o;

    //     for (int i = 0; i < o - 1; i++)
    //     {
    //         objectives[i].objectiveAmount = UnityEngine.Random.Range(5, 20);
    //         currentObjectives[i] = objectives[i];
    //     }
    // }

    private void SelectRandomObjectives()
    {
        currentObjectives.Add(objectives[0]);
    }

    private void CalculateTotalObjectivesCompleted(object sender, Objective.OnObjectiveUpdatedEventArgs e)
    {
        totalCompletedObjecives = 0;

        foreach (Objective objective in currentObjectives)
        {
            if (objective.ObjectiveComplete)
            {
                totalCompletedObjecives++;
            }
        }

        //        Debug.Log($"Total Objectives Completed: {totalCompletedObjecives}, Total Objectives: {totalObjectives}");
        if (totalCompletedObjecives == totalObjectives)
        {
            objectivesComplete = true;
        }
    }
}
