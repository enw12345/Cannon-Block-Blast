using System;
using System.Collections.Generic;
using ObjectivesScripts;
using UnityEngine;

namespace UIScripts
{
    public class ObjectivesUIManager : MonoBehaviour
    {
        public ObjectivesManager objectivesManager;
        public GameObject ObjectiveUIPrefab;
        private readonly List<GameObject> objectiveUIPrefabs = new List<GameObject>();

        private void Awake()
        {
            objectivesManager.OnInit += InitializeObjectiveUIManager;
            objectivesManager.OnReset += ClearObjectiveUI;
        }

        private void InitializeObjectiveUIManager(object sender, EventArgs e)
        {
            if (objectiveUIPrefabs.Count > 0)
                foreach (var prefab in objectiveUIPrefabs)
                    Destroy(prefab);

            foreach (var objective in objectivesManager.CurrentObjectives)
            {
                var objectiveUI = Instantiate(ObjectiveUIPrefab, transform.position, Quaternion.identity, transform);

                objectiveUI.GetComponent<ObjectiveUI>().Init(objective);

                objectiveUIPrefabs.Add(objectiveUI);
            }
        }

        private void ClearObjectiveUI(object sender, EventArgs e)
        {
            for (var i = 0; i < objectiveUIPrefabs.Count; i++) Destroy(objectiveUIPrefabs[i]);
            objectiveUIPrefabs.Clear();
        }
    }
}