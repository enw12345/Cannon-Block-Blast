using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayCanvasBehavior : MonoBehaviour, ICanvasBehavior
{
    [Header("Rect Transforms")]
    [SerializeField] private RectTransform Header = null;
    [SerializeField] private RectTransform BulletSelection = null;

    [Header("LeanTween Settings")]
    [SerializeField] public float bouncePercent = .25f;
    [SerializeField] public float time = 1.5f;

    [Header("Buttons")]
    [SerializeField] private Button nextLevelButton = null;
    [SerializeField] private Button restartButton = null;

    private void OnEnable()
    {
        HideButtons();
        ObjectivesManager.OnObjectivesComplete += HandleLevelComplete;
        BulletSpawner.OnBulletFired += HandleLevelFail;

    }

    private void OnDisable()
    {
        ObjectivesManager.OnObjectivesComplete -= HandleLevelComplete;
    }

    public void OpeningTween()
    {
        LeanTweenManager.MoveFromTop(Header, time, true);
        LeanTweenManager.MoveFromBottom(BulletSelection, time, true);
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
