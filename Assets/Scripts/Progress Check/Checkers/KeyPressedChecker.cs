using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPressedChecker : PointChecker
{
    [SerializeField] KeyCode keyCode;

    private bool pressed = false;

    public override bool Check()
    {
        return pressed;
    }

    
    void Update()
    {
        if(Input.GetKeyDown(keyCode))
        {
            pressed = true;
        }
    }
}
