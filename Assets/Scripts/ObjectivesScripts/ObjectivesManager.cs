using System;
using UnityEngine;
using System.Collections.Generic;

public class ObjectivesManager : MonoBehaviour
{
    private int totalObjectives = 0;

    private int totalCompletedObjecives;

    // public Objective[] objectives;

    [NonSerialized]
    public List<Objective> currentObjectives = new List<Objective>();

    //public event EventHandler<OnInitEventArgs> OnInit;
    public event EventHandler OnInit;
    public event EventHandler OnReset;
    public static event EventHandler OnObjectivesComplete;

    private void Awake()
    {
        LevelManager.StartLevel += InitlaizeObjectivesFromLevelManager;
    }

    private void OnEnable()
    {
        BlockBehavior.OnBlockDestroyed += CheckObjectives;
    }

    public void CheckObjectives(object sender, BlockBehavior.OnBlockDestroyedEventArgs e)
    {
        for (int i = 0; i < currentObjectives.Count; i++)
        {
            currentObjectives[i].CheckObjectiveAmountCompleted(e.blockBehavior1.BlockType);
        }
    }

    public void InitlaizeObjectivesFromLevelManager(object sender, LevelManager.StartLevelEventArgs e)
    {
        ResetObjectives();
        InitializeObjectives(e.objectives, e.objectiveAmounts);
    }

    /// <summary>
    /// Initalize objectives from an array of objectives
    /// </summary>
    /// <param name="objectives">The array of objectives to initalize</param>
    private void InitializeObjectives(Objective[] objectives, int[] objectiveAmounts)
    {
        for (int i = 0; i < objectives.Length; i++)
        {
            currentObjectives.Add(objectives[i]);
            objectives[i].Init(objectiveAmounts[i]);
            objectives[i].OnObjectiveUpdated += CalculateTotalObjectivesCompleted;
            totalObjectives++;
        }

        OnInit?.Invoke(this, EventArgs.Empty);
    }

    private void ResetObjectives()
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
            OnObjectivesComplete?.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnDestroy()
    {
        ResetObjectives();
    }
}
