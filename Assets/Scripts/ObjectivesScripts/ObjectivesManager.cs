using System;
using System.Collections.Generic;
using System.Linq;
using BlockScripts;
using Managers;
using UnityEngine;

namespace ObjectivesScripts
{
    public class ObjectivesManager : MonoBehaviour
    {
        [NonSerialized] public readonly List<Objective> CurrentObjectives = new List<Objective>();

        private int totalCompletedObjectives;
        private int totalObjectives;

        private void Awake()
        {
            LevelManager.StartLevel += InitializeObjectivesFromLevelManager;
        }

        private void OnEnable()
        {
            BlockBehavior.OnBlockDestroyed += CheckObjectives;
        }

        private void OnDestroy()
        {
            ResetObjectives();
        }
        
        public event EventHandler OnInit;
        public event EventHandler OnReset;
        public static event EventHandler OnObjectivesComplete;

        private void CheckObjectives(object sender, BlockBehavior.OnBlockDestroyedEventArgs e)
        {
            for (var i = 0; i < CurrentObjectives.Count; i++)
                CurrentObjectives[i].CheckObjectiveAmountCompleted(e.blockBehavior1.BlockType);
        }

        private void InitializeObjectivesFromLevelManager(object sender, LevelManager.StartLevelEventArgs e)
        {
            ResetObjectives();
            InitializeObjectives(e.objectives, e.objectiveAmounts);
        }
        
        private void InitializeObjectives(IReadOnlyList<Objective> objectives, IReadOnlyList<int> objectiveAmounts)
        {
            for (var i = 0; i < objectives.Count; i++)
            {
                CurrentObjectives.Add(objectives[i]);
                objectives[i].Init(objectiveAmounts[i]);
                objectives[i].OnObjectiveUpdated += CalculateTotalObjectivesCompleted;
                totalObjectives++;
            }

            OnInit?.Invoke(this, EventArgs.Empty);
        }

        private void ResetObjectives()
        {
            totalObjectives = 0;

            for (var i = 0; i < CurrentObjectives.Count; i++)
            {
                CurrentObjectives[i].OnObjectiveUpdated -= CalculateTotalObjectivesCompleted;
                CurrentObjectives[i].DeInitialize();
            }

            CurrentObjectives.Clear();
            OnReset?.Invoke(this, EventArgs.Empty);
        }

        private void CalculateTotalObjectivesCompleted(object sender, Objective.OnObjectiveUpdatedEventArgs e)
        {
            totalCompletedObjectives = 0;

            foreach (var objective in CurrentObjectives.Where(objective => objective.ObjectiveComplete))
                totalCompletedObjectives++;

            if (totalCompletedObjectives == totalObjectives) OnObjectivesComplete?.Invoke(this, EventArgs.Empty);
        }
    }
}