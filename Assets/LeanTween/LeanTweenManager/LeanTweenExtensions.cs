using System;
using UnityEngine;

/// <summary>
///     LeanTween Manager by Evan Williams
///     - LeanTween Developed by Dented Pixel
/// </summary>
public static class LeanTweenExtensions
{
    public static void InitLeanTween(int maxTweens)
    {
        LeanTween.init(maxTweens);
    }

    public static void FadeIn()
    {
    }

    /// <summary>
    ///     Add an OnComplete method to a LeanTween
    /// </summary>
    /// <param name="id">The ID of the tween</param>
    /// <param name="action">The action to invoke</param>
    public static void OnTweenComplete(int id, Action action)
    {
        var tween = LeanTween.descr(id);
        tween.setOnComplete(action);
    }

    /// <summary>
    ///     Move from a designated position to original position
    /// </summary>
    /// <param name="transform">Original transform</param>
    /// <param name="fromPosition">Position to move from</param>
    /// <param name="time">Time to complete the tween</param>
    /// <returns> Returns the ID of the tween
    ///     <returns>
    public static int MoveFrom(RectTransform transform, Vector3 fromPosition, float time)
    {
        var toPosition = transform.localPosition;
        transform.position = fromPosition;

        var descr = LeanTween.move(transform, toPosition, time);
        return descr.id;
    }

    /// <summary>
    ///     Move from the Top of the screen to original position
    /// </summary>
    /// <param name="transform">Original transform</param>
    /// <param name="time">Time to complete the tween</param>
    /// <param name="bounce">Bounce when tween is finished?</param>
    /// <returns> Returns the ID of the tween
    ///     <returns>
    public static int MoveFromTop(RectTransform transform, float time, bool bounce = false)
    {
        var toPosition = new Vector3(transform.anchoredPosition.x, transform.anchoredPosition.y, 0);

        var posY = Screen.height + transform.rect.height;
        var offScreenPosition = new Vector3(transform.anchoredPosition.x, posY, 0);

        transform.localPosition = offScreenPosition;

        LTDescr descr;

        if (!bounce)
            descr = LeanTween.move(transform, toPosition, time);
        else
            descr = LeanTween.move(transform, toPosition, time).setEaseOutBounce();

        return descr.id;
    }

    /// <summary>
    ///     Move from the bottom off screen to to original position
    /// </summary>
    /// <param name="transform">Original transform</param>
    /// <param name="time">Time to complete the tween</param>
    /// <param name="bounce">Bounce when tween is finished?</param>
    /// <returns> Returns the ID of the tween
    ///     <returns>
    public static int MoveFromBottom(RectTransform transform, float time, bool bounce = false)
    {
        var toPosition = new Vector3(transform.anchoredPosition.x, transform.anchoredPosition.y, 0);
        var posY = -(Screen.height + transform.rect.height);
        var offScreenPosition = new Vector3(transform.anchoredPosition.x, posY, 0);

        transform.localPosition = offScreenPosition;

        LTDescr descr;

        if (!bounce)
            descr = LeanTween.move(transform, toPosition, time);
        else
            descr = LeanTween.move(transform, toPosition, time).setEaseOutBounce();

        return descr.id;
    }

    /// <summary>
    ///     Move from the left off screen to original position
    /// </summary>
    /// <param name="transform">Original transform</param>
    /// <param name="time">Time to complete the tween</param>
    /// <param name="bounce">Bounce when tween is finished?</param>
    /// <returns> Returns the ID of the tween
    ///     <returns>
    public static int MoveFromLeft(RectTransform transform, float time, bool bounce = false)
    {
        var toPosition = new Vector3(transform.anchoredPosition.x, transform.anchoredPosition.y, 0);
        var posX = -(Screen.width + transform.rect.width);
        var offScreenPosition = new Vector3(posX, transform.localPosition.y, 0);

        transform.localPosition = offScreenPosition;

        LTDescr descr;

        if (!bounce)
            descr = LeanTween.move(transform, toPosition, time);
        else
            descr = LeanTween.move(transform, toPosition, time).setEaseOutBounce();

        return descr.id;
    }

    /// <summary>
    ///     Move from the Right off screen to original position
    /// </summary>
    /// <param name="transform">Original transform</param>
    /// <param name="time">Time to complete the tween</param>
    /// <param name="bounce">Bounce when tween is finished?</param>
    /// <returns> Returns the ID of the tween
    ///     <returns>
    public static int MoveFromRight(RectTransform transform, float time, bool bounce = false)
    {
        var toPosition = new Vector3(transform.anchoredPosition.x, transform.anchoredPosition.y, 0);
        var posX = Screen.width + transform.rect.width;
        var offScreenPosition = new Vector3(posX, transform.localPosition.y, 0);

        transform.localPosition = offScreenPosition;

        LTDescr descr;

        if (!bounce)
            descr = LeanTween.move(transform, toPosition, time);
        else
            descr = LeanTween.move(transform, toPosition, time).setEaseOutBounce();

        return descr.id;
    }

    /// <summary>
    ///     Move offscreen to the top
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="time"></param>
    /// <param name="bounce"></param>
    /// <returns> Returns the ID of the tween
    ///     <returns>
    public static int MoveToTop(RectTransform transform, float time, bool bounce = false)
    {
        var posY = Screen.height + transform.rect.height;
        var toPosition = new Vector3(transform.anchoredPosition.x, posY, 0);

        LTDescr descr;

        if (!bounce)
            descr = LeanTween.move(transform, toPosition, time);
        else
            descr = LeanTween.move(transform, toPosition, time).setEaseOutBounce();

        return descr.id;
    }

    /// <summary>
    ///     Move offscreen to the bottom
    /// </summary>
    /// <param name="transform"></param>
    /// <param name="time"></param>
    /// <param name="bounce"></param>
    /// <returns> Returns the ID of the tween
    ///     <returns>
    public static int MoveToBottom(RectTransform transform, float time, bool bounce = false)
    {
        var posY = -(Screen.height + transform.rect.height);
        var toPosition = new Vector3(transform.anchoredPosition.x, posY, 0);

        LTDescr descr;

        if (!bounce)
            descr = LeanTween.move(transform, toPosition, time);
        else
            descr = LeanTween.move(transform, toPosition, time).setEaseOutBounce();

        return descr.id;
    }

    /// <summary>
    ///     Move offscreen to the left
    /// </summary>
    /// <param name="transform">Original transform</param>
    /// <param name="time">Time to complete the tween</param>
    /// <param name="bounce">Bounce when tween is finished?</param>
    /// <returns> Returns the ID of the tween
    ///     <returns>
    public static int MoveToLeft(RectTransform transform, float time, bool bounce = false)
    {
        var posX = -(Screen.width + transform.rect.width);
        var toPosition = new Vector3(posX, transform.anchoredPosition.y, transform.localPosition.z);

        LTDescr descr;

        if (!bounce)
            descr = LeanTween.move(transform, toPosition, time);
        else
            descr = LeanTween.move(transform, toPosition, time).setEaseOutBounce();

        return descr.id;
    }

    /// <summary>
    ///     Move offscreen to the right
    /// </summary>
    /// <param name="transform">Original transform</param>
    /// <param name="time">Time to complete the tween</param>
    /// <param name="bounce">Bounce when tween is finished?</param>
    /// <returns> Returns the ID of the tween
    ///     <returns>
    public static int MoveToRight(RectTransform transform, float time, bool bounce = false)
    {
        var posX = Screen.width + transform.rect.width;
        var toPosition = new Vector3(posX, transform.anchoredPosition.y, transform.localPosition.z);

        LTDescr descr;

        if (!bounce)
            descr = LeanTween.move(transform, toPosition, time);
        else
            descr = LeanTween.move(transform, toPosition, time).setEaseOutBounce();

        return descr.id;
    }

    public static void ScaleUp(RectTransform transform, float scalePercent, float time)
    {
        var scaleAmount = transform.localScale * scalePercent;
        LeanTween.scale(transform, scaleAmount, time);
    }

    public static void ScaleDown(RectTransform transform, float scalePercent, float time)
    {
        var scaleAmount = transform.localScale * scalePercent;
        LeanTween.scale(transform, scaleAmount, time);
    }

    public static void BounceForeverRect(RectTransform transform, float bouncePercent, float time)
    {
        var bounceAmount = transform.localScale * bouncePercent;
        LeanTween.scale(transform, bounceAmount, time).setLoopPingPong();
    }
}