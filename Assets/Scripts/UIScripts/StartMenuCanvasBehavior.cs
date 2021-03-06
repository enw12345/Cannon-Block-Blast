using System;
using Managers;
using UnityEngine;

namespace UIScripts
{
    public class StartMenuCanvasBehavior : MonoBehaviour, ICanvasBehavior
    {
        [Header("Rect Transforms")] [SerializeField]
        private RectTransform startButton;

        [SerializeField] private RectTransform levelsButton;
        [SerializeField] private RectTransform titleTransform;

        [Header("LeanTween Settings")] [SerializeField]
        private float bouncePercent = .25f;

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

            LeanTweenExtensions.MoveToLeft(startButton, exitTime);
            LeanTweenExtensions.MoveToRight(levelsButton, exitTime);
            var id = LeanTweenExtensions.MoveToTop(titleTransform, exitTime);
            LeanTweenExtensions.OnTweenComplete(id, onClosingAction);
        }
    }
}