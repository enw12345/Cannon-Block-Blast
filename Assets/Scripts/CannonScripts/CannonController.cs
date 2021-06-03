using UnityEngine;

public class CannonController : MonoBehaviour
{
    private BulletSpawner bulletSpawner;

    [SerializeField] private float maxXRot = 20, minXRot = -20;
    [SerializeField] private float maxYRot = 20, minYRot = -20;

    float horizontalSpeed = 2.0f;
    float verticalSpeed = 2.0f;

    public float rotationSpeed = 0.5f;
    private Vector3 firstpoint;
    private float t = 0.0f;

    private void Awake()
    {
        bulletSpawner = GetComponentInChildren<BulletSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isStarted)
        {
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                RotateCannonMobile();
            }

            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                RotateCannon();
            }
        }
    }

    private void RotateCannon()
    {
        Vector3 mousePos = Input.mousePosition;

        float h = horizontalSpeed * Input.GetAxis("Mouse X");
        float v = verticalSpeed * Input.GetAxis("Mouse Y");

        Vector3 rotVal = new Vector3(-v, -h, 0);

        Vector3 rotation = transform.eulerAngles;

        // t = rotationSpeed * Time.deltaTime;
        t = rotationSpeed + Time.deltaTime;

        if (t > 1.0f)
        {
            t = 1.0f;
        }
        if (t < 0)
        {
            t = 0.0f;
        }

        float xRot = Mathf.Lerp(rotation.x, rotation.x - rotVal.x, t);
        float yRot = Mathf.Lerp(rotation.y, rotation.y - rotVal.y, t);

        transform.eulerAngles = new Vector3(
        ClampAngle(xRot, minXRot, maxXRot),
        Mathf.Clamp(yRot, minYRot, maxYRot), 0);
    }

    private void RotateCannonMobile()
    {

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    firstpoint = touch.position;
                    break;

                case TouchPhase.Moved:
                    //  Vector3 rotVal = -(((Vector3)touch.position - firstpoint) * rotationSpeed * Time.deltaTime).normalized;
                    Vector3 rotVal = -(((Vector3)touch.position - firstpoint) * Time.deltaTime).normalized;


                    Vector3 rotation = transform.eulerAngles;

                    float xRot = Mathf.Lerp(rotation.x, rotation.x - rotVal.y, t);
                    float yRot = Mathf.Lerp(rotation.y, rotation.y - rotVal.x, t);

                    t = rotationSpeed + Time.deltaTime;

                    if (t > 1.0f)
                    {
                        t = 1.0f;
                    }
                    if (t < 0)
                    {
                        t = 0.0f;
                    }

                    transform.eulerAngles = new Vector3(
                    ClampAngle(xRot, minXRot, maxXRot),
                    Mathf.Clamp(yRot, minYRot, maxYRot), 0);

                    break;
            }
        }
    }

    public void Fire()
    {
        if (GameManager.instance.isStarted)
            bulletSpawner.ShootBullet();
    }

    private float ClampAngle(float angle, float from, float to)
    {
        if (angle < 0f) angle = 360 + angle;
        if (angle > 180f) return Mathf.Max(angle, 360 + from);
        return Mathf.Min(angle, to);
    }
}
