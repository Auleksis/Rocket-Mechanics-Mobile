using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHelpMessage : AbstractAfterEffect
{
    [SerializeField] UIManager uiManager;

    [SerializeField][Multiline] string helpText;

    [SerializeField] Color textColor;

    public override void Apply()
    {
        uiManager.OpenHelpPanel();

        HelpPanel helpPanel = uiManager.GetHelpPanel();

        if(helpPanel != null)
        {
            helpPanel.SetContentColor(textColor);
            helpPanel.SetContent(helpText);
        }
    }
}
