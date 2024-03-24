using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AXIS { X, Y, Z}
public class ObjectRotationChecker : PointChecker
{
    [SerializeField] GameObject checkedObject;

    [SerializeField] float degrees;

    [SerializeField] AXIS axis;

    public override bool Check()
    {
        switch (axis) {
            case AXIS.Z:
                return Mathf.Abs(checkedObject.transform.rotation.eulerAngles.z - degrees) < 8;
            case AXIS.X:
                return Mathf.Abs(checkedObject.transform.rotation.eulerAngles.x - degrees) < 8;
            case AXIS.Y:
                return Mathf.Abs(checkedObject.transform.rotation.eulerAngles.y - degrees) < 8;
        }

        return false;
    }
}
