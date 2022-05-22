using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts
{
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
            objectiveCount = objective.objectiveAmountToComplete;

            objectiveDisplayText.text = objectiveCount.ToString();
            thisImage.sprite = objectiveImage;

            if (objective.objectiveType is ColorObjectiveType)
            {
                var colorObjective = (ColorObjectiveType) objective.objectiveType;
                thisImage.color = colorObjective.colorTarget;
            }
        }

        private void UpdateUI(object sender, Objective.OnObjectiveUpdatedEventArgs e)
        {
            var tempObjectiveCount = objectiveCount - e.objectiveAmountCompleted;
            if (tempObjectiveCount < 0)
                tempObjectiveCount = 0;

            objectiveDisplayText.text = tempObjectiveCount.ToString();
        }
    }
}