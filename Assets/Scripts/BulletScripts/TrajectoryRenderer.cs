using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TrajectoryRenderer : MonoBehaviour
{
    private LineRenderer lineRenderer;
    public int LineResolution = 10;
    private int gravity;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        gravity = (int)Physics.gravity.y;
        lineRenderer.positionCount = LineResolution + 1;
        lineRenderer.useWorldSpace = false;
    }

    private float CalculateTrajectoryDistance(Vector3 velocity, float angle)
    {
        float speed = velocity.magnitude;
        return speed * speed * Mathf.Sin(angle * 2) / gravity;
    }

    private Vector3[] CalculateVector3Array(Vector3 velocity, float angle)
    {
        Vector3[] trajectoryArray = new Vector3[LineResolution + 1];
        float maxDistance = CalculateTrajectoryDistance(velocity, angle);
        float speed = velocity.magnitude;

        for (int i = 0; i <= LineResolution; i++)
        {
            float t = (float)i / (float)LineResolution;
            trajectoryArray[i] = CalculateTrajectoryPoint(t, maxDistance, speed, angle);
        }

        return trajectoryArray;
    }

    private Vector3 CalculateTrajectoryPoint(float t, float maxDistance, float velocity, float angle)
    {
        float x = t * maxDistance;
        float y = x * Mathf.Tan(angle) - ((gravity * x * x) / (2 * velocity * velocity * Mathf.Cos(angle)) * Mathf.Cos(angle));

        return new Vector3(x, y, 0);
    }

    public void RenderTrajectory(Vector3 velocity, float angle)
    {
        lineRenderer.SetPositions(CalculateVector3Array(velocity, angle));
    }

}