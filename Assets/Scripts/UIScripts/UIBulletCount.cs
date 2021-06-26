using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class UIBulletCount : MonoBehaviour
{
    [SerializeField] private TMP_Text currentBulletCountText = null;
    [SerializeField] private Image currentBulletImage = null;

    private void Awake()
    {
        BulletSpawner.OnBulletFired += UpdateBulletCount;
        BulletSelectUI.OnBulletSelected += UpdateBulletCount;
    }

    private void UpdateBulletCount(object sender,
    BulletSpawner.OnBulletFiredEventArgs e)
    {
        currentBulletCountText.text = e.bulletCountArg.ToString();
    }

    private void UpdateBulletCount(object sender, BulletSelectUI.OnBulletSelectedEventArgs e)
    {
        currentBulletCountText.text = e.bulletCountArg.ToString();
        currentBulletImage.sprite = e.bulletImage;
    }
}
