using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceObject : MonoBehaviour
{
    protected Rigidbody2D rigidbody;

    protected virtual void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public float GetMass()
    {
        if(rigidbody== null)
            rigidbody = GetComponent<Rigidbody2D>();

        return rigidbody.mass;
    }


    public void SetPosition(Vector2 position)
    {
        rigidbody.position = position;
    }

    public void SetRotation(float rotation)
    {
        rigidbody.rotation = rotation;
    }

    public Vector2 GetPosition()
    {
        return rigidbody.position;
    }

    public float GetRotation()
    {
        return rigidbody.rotation;
    }

    public void SetKinematic(bool kinematic)
    {
        rigidbody.isKinematic = kinematic;
    }
}
