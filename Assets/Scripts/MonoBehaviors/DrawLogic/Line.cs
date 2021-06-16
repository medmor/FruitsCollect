using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(EdgeCollider2D))]
[RequireComponent(typeof(LineRenderer))]
[DisallowMultipleComponent]
public class Line : MonoBehaviour
{
    [HideInInspector]
    public List<Vector2> points;

    private LineRenderer lineRenderer;

    private EdgeCollider2D edgeCollider2D;

    public Material lineMaterial;

    public float pointZPosition = -3;

    private float pointMinOffset = 0.05f;

    private static Vector2 tempVector;

    private static Vector2 direction;

    private static float angle;

    private static float halfWidth;

    public bool autoAddColliderPoint = true;

    [HideInInspector]
    public Material lineColorMat;


    [Range(0, 5000)]
    public float maxPoints = Mathf.Infinity;

    // Use this for initialization
    void Awake()
    {
        points = new List<Vector2>();
        lineRenderer = GetComponent<LineRenderer>();
        edgeCollider2D = GetComponent<EdgeCollider2D>();
        lineColorMat = lineRenderer.material;
        if (lineMaterial == null)
        {
            //Create the material of the line
            lineMaterial = new Material(Shader.Find("Sprites/Default"));
        }
        if (lineRenderer.material == null)
            lineRenderer.material = lineMaterial;
        halfWidth = lineRenderer.endWidth / 2.0f;
    }

    public void AddPoint(Vector3 point)
    {
        if (points.Contains(point))
        {
            return;
        }

        if (points.Count > 1)
        {
            if (Vector2.Distance(point, points[points.Count - 1]) < pointMinOffset)
            {
                return;
            }
        }
        if (points.Count == 0) lineRenderer.positionCount = 0;

        point.z = pointZPosition;
        points.Add(point);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, point);
    }

    public void SetColliderAndRenderShape()
    {
        edgeCollider2D.points = points.ToArray();
    }

    public void EnableCollider()
    {
        edgeCollider2D.enabled = true;
    }



    public bool ReachedPointsLimit()
    {
        return points.Count >= maxPoints;
    }

    public void AddPointToCollider(int index)
    {
        direction = points[index] - points[index + 1 < points.Count ? index + 1 : (index - 1 >= 0 ? index - 1 : index)];
        angle = Mathf.Atan2(direction.x, -direction.y);

        tempVector = points[index];
        tempVector.x = tempVector.x + halfWidth * Mathf.Cos(angle);
        tempVector.y = tempVector.y + halfWidth * Mathf.Sin(angle);

        tempVector = points[index];
        tempVector.x = tempVector.x - halfWidth * Mathf.Cos(angle);
        tempVector.y = tempVector.y - halfWidth * Mathf.Sin(angle);

    }
}
