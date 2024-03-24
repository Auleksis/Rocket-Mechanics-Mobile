using System.Collections;
using UnityEngine;


public class Point : MonoBehaviour
{
    private bool isAchieved = false;

    [SerializeField] PointChecker checker;

    [SerializeField] AbstractAfterEffect[] afterEffects;

    public bool CheckAchieved()
    {
        if(!isAchieved)
            isAchieved = checker.Check();

        return isAchieved;
    }

    public void ApplyAfterEffects()
    {
        foreach(AbstractAfterEffect effect in afterEffects)
        {
            effect.Apply();
        }
    }
}
