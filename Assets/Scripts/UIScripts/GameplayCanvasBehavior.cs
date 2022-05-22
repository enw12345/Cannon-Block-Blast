using System;
using BulletScripts;
using Managers;
using ObjectivesScripts;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts
{
    public class GameplayCanvasBehavior : MonoBehaviour, ICanvasBehavior
    {
        [Header("Rect Transforms")] [SerializeField]
        private RectTransform Header;

        [SerializeField] private RectTransform BulletSelection;

        [Header("LeanTween Settings")] [SerializeField]
        private float bouncePercent = .25f;

        [SerializeField] private float time = 1.5f;
        [SerializeField] private float buttonTweenTime = 0.5f;

        [Header("Buttons")] [SerializeField] private Button nextLevelButton;

        [SerializeField] private Button restartButton;
        [SerializeField] private Button shootButton;

        [Space(10)] [SerializeField] private CanvasController canvasController;

        private void OnEnable()
        {
            HideButtons();
            ObjectivesManager.OnObjectivesComplete += HandleLevelComplete;
            BulletSpawner.OnBulletFired += HandleLevelFail;

            canvasController = GetComponent<CanvasController>();
            canvasController.callOpeningTweens += OpeningTween;
            canvasController.callClosingTweens += ClosingTween;
        }

        private void OnDisable()
        {
            ObjectivesManager.OnObjectivesComplete -= HandleLevelComplete;

            canvasController = GetComponent<CanvasController>();
            canvasController.callOpeningTweens -= OpeningTween;
            canvasController.callClosingTweens -= ClosingTween;
        }

        public void OpeningTween()
        {
            LeanTweenExtensions.MoveFromTop(Header, time, true);
            LeanTweenExtensions.MoveFromBottom(BulletSelection, time, true);
            LeanTweenExtensions.MoveFromRight(shootButton.GetComponent<RectTransform>(), time, true);
        }

        public void ClosingTween()
        {
        }

        private void PlayOpeningTweens(object sender, EventArgs e)
        {
            OpeningTween();
        }

        private void HandleLevelComplete(object sender, EventArgs e)
        {
            ShowContinueButton();
        }

        private void HandleLevelFail(object sender, BulletSpawner.OnBulletFiredEventArgs e)
        {
            if (e.totalBulletCount == 0) ShowRestartButton();
        }

        private void ShowContinueButton()
        {
            nextLevelButton.gameObject.SetActive(true);
            LeanTweenExtensions.MoveFromRight(nextLevelButton.GetComponent<RectTransform>(), buttonTweenTime, true);
        }

        private void ShowRestartButton()
        {
            restartButton.gameObject.SetActive(true);
        }

        public void HideButtons()
        {
            nextLevelButton.gameObject.SetActive(false);
            restartButton.gameObject.SetActive(false);
        }
    }
}