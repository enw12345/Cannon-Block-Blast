using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class BulletSelectUI : MonoBehaviour
{
    public BulletType bulletType;
    private Button button;
    [SerializeField] private Image buttonImage = null;
    [SerializeField] private TMP_Text bulletCountText = null;
    private BulletSpawner bulletSpawner;

    public static event EventHandler<OnBulletSelectedEventArgs> OnBulletSelected;

    public class OnBulletSelectedEventArgs : EventArgs
    {
        public int bulletCountArg;
        public string bulletNameArg;
        public int bulletSelectionNumber;
        public Sprite bulletImage;
    }

    public void OnEnable()
    {
        button = GetComponent<Button>();
        bulletSpawner = FindObjectOfType<BulletSpawner>();

        BulletSpawner.OnBulletFired += UpdateBulletCount;
        buttonImage.sprite = bulletType.BulletUI;
        button.onClick.AddListener(() => SelectBullet());
    }

    private void SelectBullet()
    {
        OnBulletSelected?.Invoke(this, new OnBulletSelectedEventArgs
        {
            bulletCountArg = bulletType.AmmoCount,
            bulletNameArg = bulletType.bulletName,
            bulletSelectionNumber = bulletType.BulletSelectionNumber,
            bulletImage = bulletType.BulletUI
        });

        bulletSpawner.ChangeBullet(bulletType);
    }

    private void UpdateBulletCount(object sender, BulletSpawner.OnBulletFiredEventArgs e)
    {
        bulletCountText.text = bulletType.AmmoCount.ToString();
    }

    private void UpdateBulletCount()
    {
        bulletCountText.text = bulletType.AmmoCount.ToString();
    }
}
