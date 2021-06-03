using UnityEngine;
using Cinemachine;
using System;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(TrajectoryRenderer))]
public class BulletSpawner : MonoBehaviour
{
    public float bulletSpeed = 1000;
    public float CannonForce = 10f;

    private CinemachineImpulseSource source;
    public static event EventHandler<OnBulletFiredEventArgs> OnBulletFired;
    private TrajectoryRenderer trajectoryRenderer;

    [SerializeField] private BulletTypeContainer bulletContainer;
    private BulletType currentBulletType;
    private int bulletCount;
    private int TotalBulletCount;

    public class OnBulletFiredEventArgs : EventArgs
    {
        public int bulletCountArg;
        public string bulletNameArg;
    }

    private void Awake()
    {
        source = GetComponentInParent<CinemachineImpulseSource>();
        trajectoryRenderer = GetComponent<TrajectoryRenderer>();
        BulletSelectUI.OnBulletSelected += ChangeBullet;
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
            TotalBulletCount += bullet.AmmoCount;
        }
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
        if (bulletCount > 0)
        {
            Vector3 forwardVector = transform.parent.rotation * -Vector3.forward;

            Bullet spawnedBullet = Instantiate(currentBulletType.bulletPrefab, transform.position, transform.rotation).GetComponent<Bullet>();
            currentBulletType.bulletPrefab.transform.forward = forwardVector;

            spawnedBullet.GetComponent<Rigidbody>().AddForce(currentBulletType.bulletPrefab.transform.forward * bulletSpeed, ForceMode.Impulse);

            spawnedBullet.GetComponent<Rigidbody>().AddForce(Vector3.up * CannonForce, ForceMode.Impulse);

            source.GenerateImpulse();

            bulletCount--;
            currentBulletType.AmmoCount--;
            TotalBulletCount--;

            OnBulletFired?.Invoke(this,
            new OnBulletFiredEventArgs { bulletCountArg = bulletCount, bulletNameArg = currentBulletType.bulletName });
        }

        if (TotalBulletCount <= 0)
        {
            GameManager.instance.ShowRestartButton();
        }
    }

    private void ChangeBullet(object sender, BulletSelectUI.OnBulletSelectedEventArgs e)
    {
        currentBulletType = bulletContainer.Container[e.bulletSelectionNumber];
        bulletCount = currentBulletType.AmmoCount;

        OnBulletFired?.Invoke(this, new OnBulletFiredEventArgs { bulletCountArg = bulletCount, bulletNameArg = currentBulletType.bulletName });
    }
}
