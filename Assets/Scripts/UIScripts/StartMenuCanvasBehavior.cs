using System;
using UnityEngine;

public class StartMenuCanvasBehavior : MonoBehaviour, ICanvasBehavior
{
    [Header("Rect Transforms")]
    [SerializeField] private RectTransform startButton = null;
    [SerializeField] private RectTransform levelsButton = null;
    [SerializeField] private RectTransform titleTransform = null;

    [Header("LeanTween Settings")]
    [SerializeField] private float bouncePercent = .25f;
    [SerializeField] private float enterTime = 1.5f;
    [SerializeField] private float exitTime = 1.5f;

    [SerializeField] private CanvasController canvasController;

    private Action onClosingAction;

    private void OnEnable()
    {
        canvasController = GetComponent<CanvasController>();
        canvasController.callOpeningTweens += OpeningTween;
        canvasController.callClosingTweens += ClosingTween;
    }

    private void OnDisable()
    {
        canvasController = GetComponent<CanvasController>();
        canvasController.callOpeningTweens -= OpeningTween;
        canvasController.callClosingTweens -= ClosingTween;
        onClosingAction -= canvasController.DeactivateGameObject;
    }

    public void OpeningTween()
    {
        LeanTweenExtensions.MoveFromLeft(startButton, enterTime, true);
        LeanTweenExtensions.MoveFromRight(levelsButton, enterTime, true);
        LeanTweenExtensions.MoveFromTop(titleTransform, enterTime, true);
        LeanTweenExtensions.BounceForeverRect(startButton, bouncePercent, .5f);
    }

    public void ClosingTween()
    {
        onClosingAction += canvasController.DeactivateGameObject;

        LeanTweenExtensions.MoveToLeft(startButton, exitTime, false);
        LeanTweenExtensions.MoveToRight(levelsButton, exitTime, false);
        int id = LeanTweenExtensions.MoveToTop(titleTransform, exitTime, false);
        LeanTweenExtensions.OnTweenComplete(id, onClosingAction);
    }
}
