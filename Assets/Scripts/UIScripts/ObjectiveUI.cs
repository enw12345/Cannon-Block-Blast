using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectiveUI : MonoBehaviour
{
    public Sprite objectiveImage;
    public int objectiveCount;
    public TMP_Text objectiveDisplayText;

    public void Init(Objective objective)
    {
        objective.OnObjectiveUpdated += UpdateUI;
        objectiveImage = objective.ObjectiveImage;
        objectiveCount = objective.objectiveAmount;

        objectiveDisplayText.text = objectiveCount.ToString();
    }

    private void UpdateUI(object sender, Objective.OnObjectiveUpdatedEventArgs e)
    {
        int tempObjectiveCount = objectiveCount - e.objectiveAmountCompleted;

        objectiveDisplayText.text = tempObjectiveCount.ToString();
    }
}
