using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEngineAvailable : AbstractAfterEffect
{
    [SerializeField] RocketController controller;

    [SerializeField] bool engineAvailable = true;

    public override void Apply()
    {
        controller.isEngineAvailable = engineAvailable;
    }
}
