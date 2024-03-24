using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetObjectActive : AbstractAfterEffect
{
    [SerializeField] GameObject effectObject;

    [SerializeField] bool isActive = true;

    public override void Apply()
    {
        effectObject.SetActive(isActive);
    }
}
