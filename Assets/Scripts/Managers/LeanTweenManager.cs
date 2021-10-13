using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LeanTweenManager : MonoBehaviour
{
    private void Awake()
    {
        LeanTween.init(800);
    }

    public static void BounceRect(RectTransform transform, float bouncePercent, float time)
    {
        Vector3 bounceAmount = transform.localScale * bouncePercent;

        LeanTween.scale(transform, bounceAmount, time).setLoopPingPong();
        // LeanTween.scale(transform, bounceAmount, time).setEase(LeanTweenType.easeInBounce).setLoopPingPong();
    }
}
