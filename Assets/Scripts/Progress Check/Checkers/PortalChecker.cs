using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalChecker : PointChecker
{
    [SerializeField] Portal portal;

    [SerializeField] bool isPlayerInside;

    public override bool Check()
    {
        return portal.IsPlayerInside() == isPlayerInside;
    }
}
