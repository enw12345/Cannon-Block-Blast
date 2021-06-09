﻿using System;
using UnityEngine;
using System.Collections.Generic;

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

    private int totalCompletedObjecives;

    public Objective[] objectives;

    [NonSerialized]
    public List<Objective> currentObjectives = new List<Objective>();

    private void OnEnable()
    {
        InitializeObjectives();
    }

    public void InitializeObjectives()
    {
        foreach (Objective objective in objectives)
        {
            objective.Init();
        }
        SelectRandomObjectives();
        CalculateTotalObjectiveAmount();
    }

    private void SelectRandomObjectives()
    {
        int o = 0;

        if (objectives.Length < maxObjectives)
            o = UnityEngine.Random.Range(1, objectives.Length);
        else
            o = UnityEngine.Random.Range(1, maxObjectives);

        currentObjectives.Capacity = o;

        for (int i = 0; i < o - 1; i++)
        {
            objectives[i].objectiveAmount = UnityEngine.Random.Range(5, 20);
            currentObjectives[i] = objectives[i];
        }
    }

    private void CalculateTotalObjectiveAmount()
    {
        totalObjectiveAmount = 0;

        foreach (Objective objective in currentObjectives)
        {
            totalObjectiveAmount += objective.objectiveAmount;
        }
    }

    private void CalculateTotalObjectivesCompleted()
    {
        totalCompletedObjecives = 0;

        foreach (Objective objective in currentObjectives)
        {
            if (objective.ObjectiveComplete)
            {
                totalCompletedObjecives++;
            }
        }
    }
}
