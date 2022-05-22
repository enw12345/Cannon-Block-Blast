using BulletScripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UIScripts
{
    public class UIBulletCount : MonoBehaviour
    {
        [SerializeField] private TMP_Text currentBulletCountText;
        [SerializeField] private Image currentBulletImage;

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

            if (e.bulletImage != currentBulletImage.sprite)
                currentBulletImage.sprite = e.bulletImage;
        }
    }
}