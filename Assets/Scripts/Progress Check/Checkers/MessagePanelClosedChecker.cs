using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagePanelClosedChecker : PointChecker
{
    [SerializeField] UIManager uiManager;

    public override bool Check()
    {
        return uiManager.GetMessagePanel() == null;
    }
}
