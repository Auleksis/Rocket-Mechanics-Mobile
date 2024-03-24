using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CrashMenu : MonoBehaviour
{
    [SerializeField] TMP_Text title;

    [SerializeField] SceneManagment sceneManagment;

    public void SetTitle(string text)
    {
        title.text = text;
    }

    public void ExitToMenu()
    {
        sceneManagment.LoadMainMenu();
    }

    public void ReloadLevel()
    {
        gameObject.SetActive(false);
        sceneManagment.ReloadScene();
    }
}
