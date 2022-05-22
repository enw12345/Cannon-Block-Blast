using UnityEngine;

namespace Managers
{
    public class CanvasController : MonoBehaviour
    {
        public delegate void CallClosingTweens();

        public delegate void CallOpeningTweens();

        public CanvasType canvasType;
        public CallClosingTweens callClosingTweens;
        public CallOpeningTweens callOpeningTweens;

        public void TurnOnCanvas()
        {
            gameObject.SetActive(true);
            callOpeningTweens?.Invoke();
        }

        public void TurnOffCanvas()
        {
            callClosingTweens?.Invoke();
        }

        public void DeactivateGameObject()
        {
            gameObject.SetActive(false);
        }
    }
}