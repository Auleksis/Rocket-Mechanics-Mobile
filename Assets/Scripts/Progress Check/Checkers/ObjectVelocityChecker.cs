using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectVelocityChecker : PointChecker
{
    [SerializeField] Rigidbody2D checkedObject;

    [SerializeField] float criticalValue;

    [SerializeField] bool less = false;

    public override bool Check()
    {
        return less ? checkedObject.velocity.magnitude < criticalValue : checkedObject.velocity.magnitude >= criticalValue;
    }
}
