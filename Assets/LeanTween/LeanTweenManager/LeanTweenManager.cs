using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// / <summary>
// /  A leanTween Manager By Evan Williams
// / </summary>


public class LeanTweenManager
{
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
    /// Move from the Top of the screen to original position
    /// </summary>
    /// <param name="transform">Original transform</param>
    /// <param name="time">Time to complete the tween</param>
    /// <param name="bounce">Bounce when tween is finished?</param>
    public static void MoveFromTop(RectTransform transform, float time, bool bounce = false)
    {
        Vector3 toPosition = transform.localPosition;
        float posY = (Screen.height + transform.rect.height);
        Vector3 offScreenPosition = new Vector3(transform.localPosition.x, posY, 0);

        transform.localPosition = offScreenPosition;

        if (!bounce)
            LeanTween.move(transform, toPosition, time);
        else
            LeanTween.move(transform, toPosition, time).setEaseOutBounce();
    }

    /// <summary>
    /// Move from the Bottom of the screen to original position
    /// </summary>
    /// <param name="transform">Original transform</param>
    /// <param name="time">Time to complete the tween</param>
    /// <param name="bounce">Bounce when tween is finished?</param>
    public static void MoveFromBottom(RectTransform transform, float time, bool bounce = false)
    {
        Vector3 toPosition = transform.localPosition;
        float posY = -(Screen.height + transform.rect.height);
        Vector3 offScreenPosition = new Vector3(transform.localPosition.x, posY, 0);

        transform.localPosition = offScreenPosition;

        if (!bounce)
            LeanTween.move(transform, toPosition, time);
        else
            LeanTween.move(transform, toPosition, time).setEaseOutBounce();
    }

    /// <summary>
    /// Move from the left of the screen to original position
    /// </summary>
    /// <param name="transform">Original transform</param>
    /// <param name="time">Time to complete the tween</param>
    /// <param name="bounce">Bounce when tween is finished?</param>
    public static void MoveFromLeft(RectTransform transform, float time, bool bounce = false)
    {
        Vector3 toPosition = transform.localPosition;
        float posX = -(Screen.width + transform.rect.width);
        Vector3 offScreenPosition = new Vector3(posX, transform.localPosition.y, 0);

        transform.localPosition = offScreenPosition;

        if (!bounce)
            LeanTween.move(transform, toPosition, time);
        else
            LeanTween.move(transform, toPosition, time).setEaseOutBounce();
    }

    /// <summary>
    /// Move from the Right of the screen to original position
    /// </summary>
    /// <param name="transform">Original transform</param>
    /// <param name="time">Time to complete the tween</param>
    /// <param name="bounce">Bounce when tween is finished?</param>
    public static void MoveFromRight(RectTransform transform, float time, bool bounce = false)
    {
        Vector3 toPosition = transform.localPosition;
        float posX = (Screen.width + transform.rect.width);
        Vector3 offScreenPosition = new Vector3(posX, transform.localPosition.y, 0);

        transform.localPosition = offScreenPosition;

        if (!bounce)
            LeanTween.move(transform, toPosition, time);
        else
            LeanTween.move(transform, toPosition, time).setEaseOutBounce();
    }

    /// <summary>
    ///  Move offscreen to the top
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="time"></param>
    /// <param name="bounce"></param>
    public static void MoveToTop(RectTransform transform, float time, bool bounce = false)
    {
        float posY = (Screen.height + transform.rect.height);
        Vector3 toPosition = new Vector3(transform.localPosition.x, posY, transform.localPosition.z);

        if (!bounce)
            LeanTween.move(transform, toPosition, time);
        else
            LeanTween.move(transform, toPosition, time).setEaseOutBounce();
    }

    /// <summary>
    ///  Move offscreen to the bottom
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="time"></param>
    /// <param name="bounce"></param>
    public static void MoveToBottom(RectTransform transform, float time, bool bounce = false)
    {
        float posY = -(Screen.height + transform.rect.height);
        Vector3 toPosition = new Vector3(transform.localPosition.x, posY, transform.localPosition.z);

        if (!bounce)
            LeanTween.move(transform, toPosition, time);
        else
            LeanTween.move(transform, toPosition, time).setEaseOutBounce();
    }

    /// <summary>
    /// Move offscreen to the left
    /// </summary>
    /// <param name="transform">Original transform</param>
    /// <param name="time">Time to complete the tween</param>
    /// <param name="bounce">Bounce when tween is finished?</param>
    public static void MoveToLeft(RectTransform transform, float time, bool bounce = false)
    {
        float posX = -(Screen.width + transform.rect.width);
        Vector3 toPosition = new Vector3(posX, transform.localPosition.y, transform.localPosition.z);

        if (!bounce)
            LeanTween.move(transform, toPosition, time);
        else
            LeanTween.move(transform, toPosition, time).setEaseOutBounce();
    }

    /// <summary>
    /// Move offscreen to the right
    /// </summary>
    /// <param name="transform">Original transform</param>
    /// <param name="time">Time to complete the tween</param>
    /// <param name="bounce">Bounce when tween is finished?</param>
    public static void MoveToRight(RectTransform transform, float time, bool bounce = false)
    {
        float posX = (Screen.width + transform.rect.width);
        Vector3 toPosition = new Vector3(posX, transform.localPosition.y, transform.localPosition.z);

        if (!bounce)
            LeanTween.move(transform, toPosition, time);
        else
            LeanTween.move(transform, toPosition, time).setEaseOutBounce();
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

    public static void BounceForeverRect(RectTransform transform, float bouncePercent, float time)
    {
        Vector3 bounceAmount = transform.localScale * bouncePercent;
        LeanTween.scale(transform, bounceAmount, time).setLoopPingPong();
    }

}