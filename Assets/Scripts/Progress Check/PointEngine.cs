using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointEngine : MonoBehaviour
{
    [SerializeField] Point[] points;

    private Point currentPoint;

    private int pointIndex = 0;

    private void Start()
    {
        if(points.Length > 0)
            currentPoint = points[pointIndex++];
    }

    private void Update()
    {
        if(currentPoint && currentPoint.CheckAchieved())
        {
            Debug.Log(currentPoint);

            currentPoint.ApplyAfterEffects();

            if (pointIndex < points.Length)
                currentPoint = points[pointIndex++];
            else
                currentPoint = null;

        }

    }
}
