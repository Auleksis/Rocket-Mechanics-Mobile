using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrajectoryRenderer : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private List<Vector3> points;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        points = new List<Vector3>();
    }

    public void Refresh()
    {
        points.Clear();
    }

    public void AddPoint(Vector3 point)
    {
        points.Add(point);
    }

    public void DrawLine()
    {
        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }

    public List<Vector3> GetPoints()
    {
        return points;
    }
}
