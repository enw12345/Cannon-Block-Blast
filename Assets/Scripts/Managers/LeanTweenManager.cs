using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LeanTweenManager
{
    private void Awake()
    {
        LeanTween.init(800);
    }

    public void InitLeanTween(int maxTweens)
    {
        LeanTween.init(maxTweens);
    }

    public static void FadeIn()
    {

    }

    /// <summary>
    /// Move from a designated position to original position
    /// </summary>
    /// <param name="transform">Original transform</param>
    /// <param name="fromPosition">Position to move from</param>
    /// <param name="time">Time to complete the tween</param>
    public static void MoveFrom(RectTransform transform, Vector3 fromPosition, float time)
    {
        Vector3 toPosition = transform.localPosition;
        transform.position = fromPosition;

        LeanTween.move(transform, toPosition, time);
    }

    /// <summary>
    /// Move from the left of the screen to original position
    /// </summary>
    /// <param name="transform">Original transform</param>
    /// <param name="time">Time to complete the tween</param>
    public static void MoveFromLeft(RectTransform transform, float time)
    {
        // Vector3 toPosition = transform.localPosition;
        Vector3 toPosition = transform.position;
        // float posX = transform.localPosition.x - Screen.width;
        // float posX = (-Screen.width + -transform.position.x + -transform.rect.width);// * 2;
        float posX = -Screen.width;
        Debug.Log(-Screen.width + -transform.position.x + -transform.rect.width);
        Vector3 offScreenPosition = new Vector3(posX, transform.localPosition.y, 0);

        transform.localPosition = offScreenPosition;

        // LeanTween.move(transform, toPosition, time);
    }

    public static void ScaleUp(RectTransform transform, float scalePercent, float time)
    {
        Vector3 scaleAmount = transform.localScale * scalePercent;
        LeanTween.scale(transform, scaleAmount, time);
    }

    public static void ScaleDown(RectTransform transform, float scalePercent, float time)
    {
        Vector3 scaleAmount = transform.localScale * scalePercent;
        LeanTween.scale(transform, scaleAmount, time);
    }

    public static void BounceRect(RectTransform transform, float bouncePercent, float time)
    {
        Vector3 bounceAmount = transform.localScale * bouncePercent;
        LeanTween.scale(transform, bounceAmount, time).setLoopPingPong();
    }
}
