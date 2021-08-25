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
    
    /// <summary>
    /// Initalize objectives from an array of objectives
    /// </summary>
    /// <param name="objectives">The array of objectives to initalize</param>
    public void InitializeObjectives(Objective[] objectives, int[] objectiveAmounts)
    {
        for (int i = 0; i < objectives.Length; i++)
        {
            currentObjectives.Add(objectives[i]);
            objectives[i].Init(objectiveAmounts[i]);
            objectives[i].OnObjectiveUpdated += CalculateTotalObjectivesCompleted;
            totalObjectives++;
        }

        OnInit?.Invoke(this, EventArgs.Empty);

        objectivesComplete = false;
    }


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
