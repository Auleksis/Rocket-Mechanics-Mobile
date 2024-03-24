using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableTrajectorySimulation : AbstractAfterEffect
{
    [SerializeField] GameObject simulatingObject;

    [SerializeField] bool isEnabled = true;

    public override void Apply()
    {
        simulatingObject.GetComponent<SystemSimulator>().enabled = isEnabled;
    }
}
