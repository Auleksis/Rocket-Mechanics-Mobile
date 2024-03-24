using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTimeScale : AbstractAfterEffect
{
    [SerializeField] float timeScale;

    public override void Apply()
    {
        Time.timeScale = timeScale;
    }
}
