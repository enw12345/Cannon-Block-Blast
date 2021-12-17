using System;
using UnityEngine;
using UnityEngine.UI;

public class GameplayCanvasBehavior : MonoBehaviour, ICanvasBehavior
{
    [Header("Rect Transforms")]
    [SerializeField] private RectTransform Header = null;
    [SerializeField] private RectTransform BulletSelection = null;

    [Header("LeanTween Settings")]
    [SerializeField] private float bouncePercent = .25f;
    [SerializeField] private float time = 1.5f;
    [SerializeField] private float buttonTweenTime = 0.5f;

    [Header("Buttons")]
    [SerializeField] private Button nextLevelButton = null;
    [SerializeField] private Button restartButton = null;
    [SerializeField] private Button shootButton = null;

    [Space(10)]
    [SerializeField] private CanvasController canvasController;

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

    private void PlayOpeningTweens(object sender, EventArgs e)
    {
        OpeningTween();
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

    public void HandleLevelComplete(object sender, EventArgs e)
    {
        ShowContinueButton();
    }

    public void HandleLevelFail(object sender, BulletSpawner.OnBulletFiredEventArgs e)
    {
        if (e.totalBulletCount == 0)
        {
            ShowRestartButton();
        }
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
