using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectiveUI : MonoBehaviour
{
    public Sprite objectiveImage;
    public int objectiveCount;
    public TMP_Text objectiveDisplayText;
    private Image thisImage;

    public void Init(Objective objective)
    {
        thisImage = GetComponent<Image>();

        objective.OnObjectiveUpdated += UpdateUI;
        objectiveImage = objective.ObjectiveImage;
        objectiveCount = objective.objectiveAmount;

        objectiveDisplayText.text = objectiveCount.ToString();
        thisImage.sprite = objectiveImage;
        if ((ColorObjectiveType)objective.objectiveType)
        {
            ColorObjectiveType colorObjective = (ColorObjectiveType)objective.objectiveType;
            int colorIndex = colorObjective.colorIndex;
            thisImage.color = colorObjective.colorTarget;
        }
    }

    private void UpdateUI(object sender, Objective.OnObjectiveUpdatedEventArgs e)
    {
        int tempObjectiveCount = objectiveCount - e.objectiveAmountCompleted;

        objectiveDisplayText.text = tempObjectiveCount.ToString();
    }
}
