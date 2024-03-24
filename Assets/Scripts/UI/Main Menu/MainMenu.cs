using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject aboutPanel;

    public void StartNewGame()
    {
        SceneManager.LoadScene("EducationScene");
    }

    public void StartWithoutEducation()
    {
        SceneManager.LoadScene("DemoLevel");
    }

    public void About()
    {
        mainMenuPanel.SetActive(false);
        aboutPanel.SetActive(true);
    }

    public void GoMainMenu()
    {
        mainMenuPanel.SetActive(true);
        aboutPanel.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
