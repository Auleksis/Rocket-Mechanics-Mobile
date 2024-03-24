using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamOrthoSizeChecker : PointChecker
{
    [SerializeField] CinemachineVirtualCamera cam;

    [SerializeField] float criticalValue;

    [SerializeField] bool less = false;

    public override bool Check()
    {
        return less ? cam.m_Lens.OrthographicSize < criticalValue : cam.m_Lens.OrthographicSize >= criticalValue;
    }
}
