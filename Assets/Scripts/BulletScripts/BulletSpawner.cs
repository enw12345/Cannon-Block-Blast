using UnityEngine;
using Cinemachine;
using System;

[RequireComponent(typeof(TrajectoryRenderer))]
public class BulletSpawner : MonoBehaviour
{
    public float bulletSpeed = 1000;
    public float CannonForce = 10f;

    private CinemachineImpulseSource source;
    private TrajectoryRenderer trajectoryRenderer;

    [SerializeField] private BulletTypeContainer bulletContainer = null;
    private BulletType currentBulletType;
    private int bulletCount;
    private int totalBulletCount;

    [SerializeField] private BulletTypeContainer PlayersBullets = null;

    public int TotalBulletCount
    {
        get { return totalBulletCount; }
    }

    public static event EventHandler<OnBulletFiredEventArgs> OnBulletFired;
    public class OnBulletFiredEventArgs : EventArgs
    {
        public int bulletCountArg;
        public int totalBulletCount;
        public string bulletNameArg;
    }

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
        new OnBulletFiredEventArgs { bulletCountArg = bulletCount });

        //calculate the trajectory and render it
        Vector3 forwardVector = transform.parent.rotation * -Vector3.forward;
        float angle = Vector3.Angle(forwardVector, Vector3.left) * Mathf.Deg2Rad;
        trajectoryRenderer.RenderTrajectory(forwardVector * bulletSpeed, angle);

        //Reset Total Bullet count
        foreach (BulletType bullet in bulletContainer.Container)
        {
            totalBulletCount += bullet.AmmoCount;
        }

        GameManager.instance.resetEvent += ResetPlayerBullets;
    }

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && GameManager.instance.isStarted)
            ShootBullet();
    }
#endif

    public void ShootBullet()
    {
        if (bulletCount > 0 && GameManager.instance.canShoot)
        {
            Vector3 forwardVector = transform.parent.rotation * -Vector3.forward;

            Bullet spawnedBullet = Instantiate(currentBulletType.bulletPrefab, transform.position, transform.rotation).GetComponent<Bullet>();
            currentBulletType.bulletPrefab.transform.forward = forwardVector;

            spawnedBullet.GetComponent<Rigidbody>().AddForce(currentBulletType.bulletPrefab.transform.forward * bulletSpeed, ForceMode.Impulse);

            spawnedBullet.GetComponent<Rigidbody>().AddForce(Vector3.up * CannonForce, ForceMode.Impulse);

            source.GenerateImpulse();

            bulletCount--;
            currentBulletType.AmmoCount--;
            totalBulletCount--;

            OnBulletFired?.Invoke(this,
            new OnBulletFiredEventArgs { bulletCountArg = bulletCount, totalBulletCount = totalBulletCount, bulletNameArg = currentBulletType.bulletName });
        }
    }

    public void ChangeBullet(BulletType bulletType)
    {
        currentBulletType = bulletContainer.Container[bulletType.BulletSelectionNumber];
        bulletCount = currentBulletType.AmmoCount;

        OnBulletFired?.Invoke(this, new OnBulletFiredEventArgs { bulletCountArg = bulletCount, totalBulletCount = totalBulletCount, bulletNameArg = currentBulletType.bulletName });
    }

    private void ResetPlayerBullets()
    {
        foreach (BulletType playerBulletType in PlayersBullets.Container)
        {
            playerBulletType.AmmoCount = playerBulletType.DefaultAmmoCount;
        }
    }

    private void ResetPlayerBullets(object sender, EventArgs e)
    {
        foreach (BulletType playerBulletType in PlayersBullets.Container)
        {
            playerBulletType.AmmoCount = playerBulletType.DefaultAmmoCount;
            totalBulletCount += playerBulletType.AmmoCount;
        }

        OnBulletFired?.Invoke(this,
        new OnBulletFiredEventArgs { bulletCountArg = bulletCount, totalBulletCount = totalBulletCount });
    }
}
