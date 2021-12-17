using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public CanvasType canvasType;

    public delegate void CallOpeningTweens();
    public delegate void CallClosingTweens();
    public CallOpeningTweens callOpeningTweens;
    public CallClosingTweens callClosingTweens;

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
