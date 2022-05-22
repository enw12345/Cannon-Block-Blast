using UnityEngine;

namespace BulletScripts
{
    [RequireComponent(typeof(LineRenderer))]
    public class TrajectoryRenderer : MonoBehaviour
    {
        public int LineResolution = 10;
        private int gravity;
        private LineRenderer lineRenderer;

        private void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
            gravity = (int) Physics.gravity.y;
            lineRenderer.positionCount = LineResolution + 1;
            lineRenderer.useWorldSpace = false;
        }

        private float CalculateTrajectoryDistance(Vector3 velocity, float angle)
        {
            var speed = velocity.magnitude;
            return speed * speed * Mathf.Sin(angle * 2) / gravity;
        }

        private Vector3[] CalculateVector3Array(Vector3 velocity, float angle)
        {
            var trajectoryArray = new Vector3[LineResolution + 1];
            var maxDistance = CalculateTrajectoryDistance(velocity, angle);
            var speed = velocity.magnitude;

            for (var i = 0; i <= LineResolution; i++)
            {
                var t = i / (float) LineResolution;
                trajectoryArray[i] = CalculateTrajectoryPoint(t, maxDistance, speed, angle);
            }

            return trajectoryArray;
        }

        private Vector3 CalculateTrajectoryPoint(float t, float maxDistance, float velocity, float angle)
        {
            var x = t * maxDistance;
            var y = x * Mathf.Tan(angle) -
                    gravity * x * x / (2 * velocity * velocity * Mathf.Cos(angle)) * Mathf.Cos(angle);

            return new Vector3(x, y, 0);
        }

        public void RenderTrajectory(Vector3 velocity, float angle)
        {
            lineRenderer.SetPositions(CalculateVector3Array(velocity, angle));
        }
    }
}