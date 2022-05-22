using UnityEngine;

namespace BulletScripts
{
    [CreateAssetMenu(fileName = "Bullet Object", menuName = "Bullet Type")]
    public class BulletType : ScriptableObject
    {
        [Header("Bullet Prefab")] public GameObject bulletPrefab;

        [Header("Bullet UI Data")] public Sprite BulletUI;

        public int BulletSelectionNumber;

        [Header("Bullet Data")] public int AmmoCount;

        public int DefaultAmmoCount;
        public string bulletName;
    }
}