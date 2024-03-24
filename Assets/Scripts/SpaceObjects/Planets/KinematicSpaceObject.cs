using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class KinematicSpaceObject : SpaceObject
{
    [SerializeField] SpaceObject parentObject;
    [SerializeField] GameObject nextFocus;
    [SerializeField] float angularVelocityScale = 1f;

    [SerializeField] float angularAxisVelocityDeg = 5f;

    private float axisAngle = 0f;

    private float orbitalAngularVelocity;

    private float orbitalVelocity;

    [SerializeField] float systemAngle;

    private Vector3 ellipseCenter;

    private float ellipesA;
    private float ellipseB;

    private Vector3 targetPosition;

    private bool isCircle = true;

    protected override void Awake()
    {
        base.Awake();

        //systemAngle = 0f;

        if (nextFocus != null)
            isCircle = false;

        if (isCircle)
        {
            ellipseCenter = parentObject.transform.position;
            ellipesA = ellipseB = (ellipseCenter - transform.position).magnitude;

            orbitalAngularVelocity = Mathf.Sqrt((SpaceLogic.GRAVITY_SCALE * parentObject.GetMass()) / (Mathf.Pow(ellipesA, 3))) * angularVelocityScale;

            orbitalVelocity = Mathf.Sqrt((SpaceLogic.GRAVITY_SCALE * parentObject.GetMass()) / (ellipesA));
        }
        else
        {
            //Дополнительно нужно установить тело на нулевой угол!

            ellipseCenter = (nextFocus.transform.position - parentObject.transform.position) / 2 + parentObject.transform.position;

            Vector3 starVector = parentObject.transform.position - transform.position;
            Vector3 focusVector = nextFocus.transform.position - transform.position;

            float starDist = starVector.magnitude;
            float focusDist = focusVector.magnitude;


        }
    }


    private void FixedUpdate()
    {
        //VelocityMoveObject();
        MoveObject();

        RotateAroundAxis();
    }

    private void Update()
    {
        

        //Debug.Log(transform.position + " " + systemAngle + " " + orbitalAngularVelocity);
    }

    private void RotateAroundAxis()
    {
        float rotationZ = angularAxisVelocityDeg * Time.fixedDeltaTime;
        transform.Rotate(new Vector3(0, 0, rotationZ));
    }

    public void MoveObject()
    {
        float radians = Mathf.Deg2Rad * systemAngle;

        float systemX = ellipesA * Mathf.Cos(radians);
        float systemY = ellipseB * Mathf.Sin(radians);

        systemAngle += orbitalAngularVelocity * Time.fixedDeltaTime;

        systemAngle %= 360f;

        targetPosition = ellipseCenter + new Vector3(systemX, systemY, 0);

        //transform.position = Vector3.Lerp(transform.position, targetPosition, 0.9f);

        rigidbody.position = Vector3.Lerp(rigidbody.position, targetPosition, 0.9f);

        //rigidbody.position = Vector3.Lerp(rigidbody.position, targetPosition, 0.9f);
    }

    public void VelocityMoveObject()
    {
        Vector2 gravityVector = (Vector2)parentObject.transform.position - rigidbody.position;

        //rigidbody.velocity = Quaternion.Euler(0, 0, 90) * gravityVector.normalized * orbitalVelocity * -1;

        Vector2 newDirection = Vector3.Cross(gravityVector, Vector3.forward).normalized;

        rigidbody.velocity = newDirection * orbitalVelocity;

        //Debug.Log(gravityVector.magnitude + " " + rigidbody.velocity.magnitude); 
    }

    public void SetsystemAngle(float angle)
    {
        systemAngle = angle;
    }

    public float GetSystemAngle()
    {
        return systemAngle;
    }

    public float GetOrbitalAngularVelocity()
    {
        return orbitalAngularVelocity;
    }

}
