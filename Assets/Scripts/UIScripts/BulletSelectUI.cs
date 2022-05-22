using System;
using BulletScripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts
{
    public class BulletSelectUI : MonoBehaviour
    {
        public BulletType bulletType;
        [SerializeField] private Image buttonImage;
        [SerializeField] private TMP_Text bulletCountText;
        private BulletSpawner bulletSpawner;
        private Button button;

        private void OnEnable()
        {
            button = GetComponent<Button>();
            bulletSpawner = FindObjectOfType<BulletSpawner>();

            BulletSpawner.OnBulletFired += UpdateBulletCount;
            buttonImage.sprite = bulletType.BulletUI;
            button.onClick.AddListener(() => SelectBullet());
        }

        private void OnDisable()
        {
            button.onClick.RemoveAllListeners();
        }

        public static event EventHandler<OnBulletSelectedEventArgs> OnBulletSelected;

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

        public class OnBulletSelectedEventArgs : EventArgs
        {
            public int bulletCountArg;
            public Sprite bulletImage;
            public string bulletNameArg;
            public int bulletSelectionNumber;
        }
    }
}