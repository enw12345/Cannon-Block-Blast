using System;
using Managers;
using UnityEngine;

namespace CannonScripts
{
    public class CannonController : MonoBehaviour
    {
        [SerializeField] private float maxXRot = 20f, minXRot = -3f;
        [SerializeField] private float maxYRot = 105f, minYRot = 70f;

        public float rotationSpeed = 0.8f;
        private Vector3 firstpoint;

        private const float horizontalSpeed = 2.0f;
        private float t;
        private const float verticalSpeed = 2.0f;

        //  Update is called once per frame
        private void Update()
        {
            if (!GameManager.Instance.isStarted) return;
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                case RuntimePlatform.IPhonePlayer:
                    RotateCannonMobile();
                    break;
                case RuntimePlatform.WindowsEditor:
                    RotateCannon();
                    break;
            }
        }

        private void RotateCannon()
        {
            var mousePos = Input.mousePosition;

            var h = horizontalSpeed * Input.GetAxis("Mouse X");
            var v = verticalSpeed * Input.GetAxis("Mouse Y");

            var rotVal = new Vector3(-v, -h, 0);

            var rotation = transform.eulerAngles;

            t = rotationSpeed + Time.deltaTime;

            if (t > 1.0f) t = 1.0f;
            if (t < 0) t = 0.0f;

            var xRot = Mathf.Lerp(rotation.x, rotation.x - rotVal.x, t);
            var yRot = Mathf.Lerp(rotation.y, rotation.y - rotVal.y, t);

            transform.eulerAngles = new Vector3(
                ClampAngle(xRot, minXRot, maxXRot),
                Mathf.Clamp(yRot, minYRot, maxYRot), 0);
        }

        private void RotateCannonMobile()
        {
            if (Input.touchCount <= 0) return;
            var touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    firstpoint = touch.position;
                    break;

                case TouchPhase.Moved:
                    //  Vector3 rotVal = -(((Vector3)touch.position - firstpoint) * rotationSpeed * Time.deltaTime).normalized;
                    var rotVal = -(((Vector3) touch.position - firstpoint) * Time.deltaTime).normalized;


                    var rotation = transform.eulerAngles;

                    var xRot = Mathf.Lerp(rotation.x, rotation.x - rotVal.y, t);
                    var yRot = Mathf.Lerp(rotation.y, rotation.y - rotVal.x, t);

                    t = rotationSpeed + Time.deltaTime;

                    if (t > 1.0f) t = 1.0f;
                    if (t < 0) t = 0.0f;

                    transform.eulerAngles = new Vector3(
                        ClampAngle(xRot, minXRot, maxXRot),
                        Mathf.Clamp(yRot, minYRot, maxYRot), 0);

                    break;
                case TouchPhase.Stationary:
                    break;
                case TouchPhase.Ended:
                    break;
                case TouchPhase.Canceled:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static float ClampAngle(float angle, float from, float to)
        {
            if (angle < 0f) angle = 360 + angle;
            return angle > 180f ? Mathf.Max(angle, 360 + @from) : Mathf.Min(angle, to);
        }
    }
}