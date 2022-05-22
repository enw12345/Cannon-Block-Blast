using System;
using Cinemachine;
using Managers;
using UnityEngine;

namespace BulletScripts
{
    [RequireComponent(typeof(TrajectoryRenderer))]
    public class BulletSpawner : MonoBehaviour
    {
        public float bulletSpeed = 1000;
        public float CannonForce = 10f;

        [SerializeField] private BulletTypeContainer bulletContainer;

        [SerializeField] private BulletTypeContainer PlayersBullets;
        private int bulletCount;
        private BulletType currentBulletType;

        private CinemachineImpulseSource source;
        private TrajectoryRenderer trajectoryRenderer;

        private int TotalBulletCount { get; set; }

        private void Awake()
        {
            source = GetComponentInParent<CinemachineImpulseSource>();
            trajectoryRenderer = GetComponent<TrajectoryRenderer>();
            ResetPlayerBullets();
        }

        private void Start()
        {
            currentBulletType = bulletContainer.Container[0];
            bulletCount = currentBulletType.AmmoCount;

            OnBulletFired?.Invoke(this,
                new OnBulletFiredEventArgs {bulletCountArg = bulletCount});

            //calculate the trajectory and render it
            var forwardVector = transform.parent.rotation * -Vector3.forward;
            var angle = Vector3.Angle(forwardVector, Vector3.left) * Mathf.Deg2Rad;
            trajectoryRenderer.RenderTrajectory(forwardVector * bulletSpeed, angle);

            //Reset Total Bullet count
            foreach (var bullet in bulletContainer.Container) TotalBulletCount += bullet.AmmoCount;

            GameManager.Instance.resetEvent += ResetPlayerBullets;
        }

#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && GameManager.Instance.isStarted)
                ShootBullet();
        }
#endif

        public static event EventHandler<OnBulletFiredEventArgs> OnBulletFired;

        public void ShootBullet()
        {
            if (bulletCount <= 0 || !GameManager.Instance.canShoot) return;
            var forwardVector = transform.parent.rotation * -Vector3.forward;

            var spawnedBullet = Instantiate(currentBulletType.bulletPrefab, transform.position, transform.rotation)
                .GetComponent<Bullet>();
            currentBulletType.bulletPrefab.transform.forward = forwardVector;

            spawnedBullet.GetComponent<Rigidbody>()
                .AddForce(currentBulletType.bulletPrefab.transform.forward * bulletSpeed, ForceMode.Impulse);

            spawnedBullet.GetComponent<Rigidbody>().AddForce(Vector3.up * CannonForce, ForceMode.Impulse);

            source.GenerateImpulse();

            bulletCount--;
            currentBulletType.AmmoCount--;
            TotalBulletCount--;

            OnBulletFired?.Invoke(this,
                new OnBulletFiredEventArgs
                {
                    bulletCountArg = bulletCount, totalBulletCount = TotalBulletCount,
                    bulletNameArg = currentBulletType.bulletName
                });
        }

        public void ChangeBullet(BulletType bulletType)
        {
            currentBulletType = bulletContainer.Container[bulletType.BulletSelectionNumber];
            bulletCount = currentBulletType.AmmoCount;

            OnBulletFired?.Invoke(this,
                new OnBulletFiredEventArgs
                {
                    bulletCountArg = bulletCount, totalBulletCount = TotalBulletCount,
                    bulletNameArg = currentBulletType.bulletName
                });
        }

        private void ResetPlayerBullets()
        {
            foreach (var playerBulletType in PlayersBullets.Container)
                playerBulletType.AmmoCount = playerBulletType.DefaultAmmoCount;
        }

        private void ResetPlayerBullets(object sender, EventArgs e)
        {
            foreach (var playerBulletType in PlayersBullets.Container)
            {
                playerBulletType.AmmoCount = playerBulletType.DefaultAmmoCount;
                TotalBulletCount += playerBulletType.AmmoCount;
            }

            OnBulletFired?.Invoke(this,
                new OnBulletFiredEventArgs {bulletCountArg = bulletCount, totalBulletCount = TotalBulletCount});
        }

        public class OnBulletFiredEventArgs : EventArgs
        {
            public int bulletCountArg;
            public string bulletNameArg;
            public int totalBulletCount;
        }
    }
}