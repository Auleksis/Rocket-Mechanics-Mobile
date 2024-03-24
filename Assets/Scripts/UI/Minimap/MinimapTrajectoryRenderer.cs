using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapTrajectoryRenderer : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private List<Vector3> points;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        points = new List<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
