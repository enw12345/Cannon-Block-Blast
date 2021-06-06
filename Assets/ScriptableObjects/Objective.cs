using UnityEngine;

[CreateAssetMenu(menuName = "Objective")]
public class Objective : ScriptableObject
{
    private string objectiveName;
    public int objectiveAmount;
    public ObjectiveTypes objectiveType;
    private bool objectiveComplete = false;
    
    public bool ObjectiveComplete
    {
        get { return objectiveComplete; }
    }
}