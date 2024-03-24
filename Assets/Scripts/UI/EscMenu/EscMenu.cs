using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscMenu : MonoBehaviour
{
    [SerializeField] SceneManagment sceneManagment;

    [SerializeField] AbstractAfterEffect[] effectIfActive;
    [SerializeField] AbstractAfterEffect[] effectIfClose;

    public void Open()
    {
        foreach (var effect in effectIfActive)
        {
            effect.Apply();
        }
    }

    public void Close()
    {
        foreach (var effect in effectIfClose)
        {
            effect.Apply();
        }
    }

    public void ContinueGame()
    {
        gameObject.SetActive(false);
    }

    public void ExitToMenu()
    {
        sceneManagment.LoadMainMenu();    
    }

    public void ReloadLevel()
    {
        sceneManagment.ReloadScene();
    }
}
