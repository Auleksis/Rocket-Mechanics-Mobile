using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] InfoText velocityInfo;
    [SerializeField] InfoText gasInfo;
    [SerializeField] InfoText timeScaleInfo;
    [SerializeField] InfoText allVelocityInfo;

    [SerializeField] MessagePanel messagePanel;

    [SerializeField] HelpPanel helpPanel;

    [SerializeField] EscMenu escMenu;

    [SerializeField] CrashMenu crashMenu;

    public void SetVelocityInfo(string data)
    {
        if (velocityInfo.gameObject.activeSelf)
            velocityInfo.SetText(data);
        
    }

    public void SetTimeScaleInfo(string data)
    {
        if (timeScaleInfo.gameObject.activeSelf)
            timeScaleInfo.SetText(data);
    }

    public void SetGasInfo(string data)
    {
        if(gasInfo.gameObject.activeSelf)
            gasInfo.SetText(data);
    }

    public void SetGasInfoColor(Color color)
    {
        if(gasInfo.gameObject.activeSelf)
            gasInfo.SetColor(color);
    }

    public void SetAllVelocityInfo(string data)
    {
        if (allVelocityInfo.gameObject.activeSelf)
            allVelocityInfo.SetText(data);
    }

    public void OpenMessagePanel()
    {
        CloseHelpPanel();
        messagePanel.gameObject.SetActive(true);
        messagePanel.Open();
    }

    public void CloseMessagePanel()
    {
        messagePanel.Close();
        messagePanel.gameObject.SetActive(false);
    }

    public void OpenHelpPanel()
    {
        CloseMessagePanel();
        helpPanel.gameObject.SetActive(true);
        helpPanel.Open();
    }

    public void CloseHelpPanel()
    {
        helpPanel.Close();
        helpPanel.gameObject.SetActive(false);
    }


    public void CallEscMenu()
    {
        if (escMenu == null || crashMenu.gameObject.activeSelf)
            return;

        if (!escMenu.gameObject.activeSelf)
        {
            escMenu.gameObject.SetActive(true);
            escMenu.Open();
        }
        else
        {
            escMenu.gameObject.SetActive(false);
            escMenu.Close();
        }
    }

    public void CallCrashMenu(string title)
    {
        if (crashMenu != null)
        {
            crashMenu.gameObject.SetActive(true);
            crashMenu.SetTitle(title);
        }
    }


    public MessagePanel GetMessagePanel()
    {
        if (messagePanel.gameObject.activeSelf)
        {
            return messagePanel;
        }

        return null;
    }

    public HelpPanel GetHelpPanel()
    {
        if (helpPanel.gameObject.activeSelf)
        {
            return helpPanel;
        }

        return null;
    }
}
