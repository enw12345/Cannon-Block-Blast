using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectivesUIManager : MonoBehaviour
{
    public ObjectivesManager objectivesManager;
    public GameObject ObjectiveUIPrefab;
    private List<GameObject> ObjectiveUIPrefabs = new List<GameObject>();

    private void Awake()
    {
        objectivesManager.OnInit += InitalizeObjectiveUIManager;
        objectivesManager.OnReset += ClearObjectiveUI;
    }

    public void InitalizeObjectiveUIManager(object sender, EventArgs e)
    {
        if (ObjectiveUIPrefabs.Count > 0)
        {
            foreach (GameObject prefab in ObjectiveUIPrefabs)
            {
                GameObject.Destroy(prefab);
            }
        }

        foreach (Objective objective in objectivesManager.currentObjectives)
        {
            GameObject objectiveUI = Instantiate(ObjectiveUIPrefab, transform.position, Quaternion.identity, transform);

            objectiveUI.GetComponent<ObjectiveUI>().Init(objective);

            ObjectiveUIPrefabs.Add(objectiveUI);
        }
    }

    public void ClearObjectiveUI(object sender, EventArgs e)
    {
        for (int i = 0; i < ObjectiveUIPrefabs.Count; i++)
        {
            GameObject.Destroy(ObjectiveUIPrefabs[i]);
        }
        ObjectiveUIPrefabs.Clear();
    }
}
