using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : AbstractAfterEffect
{
    [SerializeField] SceneManagment sceneManagment;

    [SerializeField] string sceneName;

    public override void Apply()
    {
        sceneManagment.LoadScene(sceneName);
    }
}
