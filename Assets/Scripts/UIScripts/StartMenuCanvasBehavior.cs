using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuCanvasBehavior : MonoBehaviour, ICanvasBehavior
{
    [Header("Rect Transforms")]
    [SerializeField] private RectTransform startButton = null;
    [SerializeField] private RectTransform levelsButton = null;
    [SerializeField] private RectTransform titleTransform = null;

    [Header("LeanTween Settings")]
    [SerializeField] private float bouncePercent = .25f;
    [SerializeField] private float time = 1.5f;

    public void OpeningTween()
    {
        LeanTweenManager.MoveFromLeft(startButton, time, true);
        LeanTweenManager.MoveFromRight(levelsButton, time, true);
        LeanTweenManager.MoveFromTop(titleTransform, time, true);
        LeanTweenManager.BounceForeverRect(startButton, bouncePercent, .5f);
    }

    public void ClosingTween()
    {
        LeanTweenManager.MoveToLeft(startButton, time, true);
        LeanTweenManager.MoveToRight(levelsButton, time, true);
        LeanTweenManager.MoveToTop(titleTransform, time, true);
    }
}
