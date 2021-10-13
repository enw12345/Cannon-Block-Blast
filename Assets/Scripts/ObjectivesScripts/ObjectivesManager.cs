using System;
using UnityEngine;
using System.Collections.Generic;

public class ObjectivesManager : MonoBehaviour
{
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
    public event EventHandler OnReset;

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

    public void ResetObjectives()
    {
        totalObjectives = 0;

        for (int i = 0; i < currentObjectives.Count; i++)
        {
            currentObjectives[i].OnObjectiveUpdated -= CalculateTotalObjectivesCompleted;
            currentObjectives[i].DeInitialize();
        }

        currentObjectives.Clear();
        OnReset?.Invoke(this, EventArgs.Empty);
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

        if (totalCompletedObjecives == totalObjectives)
        {
            objectivesComplete = true;
        }
    }

    private void OnDestroy()
    {
        ResetObjectives();
    }
}
