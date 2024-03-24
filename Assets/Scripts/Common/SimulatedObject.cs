using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulatedObject : SpaceObject
{
    [HideInInspector] public LayerMask physicsLayer;

    private TrajectoryRenderer trajectoryRenderer;

    public bool isSimulatedOnAnotherScene = false;

    protected override void Awake()
    {
        base.Awake();
        trajectoryRenderer = GetComponent<TrajectoryRenderer>();
    }

    private void FixedUpdate()
    {
        RecalculateForce();
    }

    public void RecalculateForce()
    {
        GameObject[] physicable = GameObject.FindGameObjectsWithTag("MakeGravity");

        rigidbody = gameObject.GetComponent<Rigidbody2D>();

        foreach (GameObject physicableObj in physicable)
        {
            //Реализуем физику только на определённом слое
            if (physicableObj.layer != physicsLayer.value)
                continue;

            Rigidbody2D rb = physicableObj.GetComponent<Rigidbody2D>();

            Vector2 position = rb.position;

            Vector3 distance = position - rigidbody.position;

            float force = (SpaceLogic.GRAVITY_SCALE * rigidbody.mass * rb.mass) / (distance.sqrMagnitude);

            rigidbody.AddForce(distance.normalized * force * Time.fixedDeltaTime);
        }

        //Debug.Log("Total: " + rigidbody.totalForce);
    }

    public void CopyRigidbodyInfo(SimulatedObject from)
    {
        rigidbody.position = from.rigidbody.position;
        rigidbody.rotation = from.rigidbody.rotation;

        rigidbody.velocity = from.rigidbody.velocity;
        rigidbody.angularVelocity = from.rigidbody.angularVelocity;
        rigidbody.totalForce = from.rigidbody.totalForce;
    }

    public void RefreshTrajectoryLine()
    {
        trajectoryRenderer.Refresh();
    }

    public void AddPositionToTrajectoryLine(Vector3 point)
    {
        trajectoryRenderer.AddPoint(point);
    }

    public void DrawTrajectoryLine()
    {
        trajectoryRenderer.DrawLine();
    }

    public Vector2 GetRigidbodyPosition()
    {
        return rigidbody.position;
    }
}
