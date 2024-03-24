using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMessage : AbstractAfterEffect
{
    [SerializeField] UIManager uiManager;

    [SerializeField][Multiline] string title;
    [SerializeField][Multiline] string content;

    public override void Apply()
    {
        uiManager.OpenMessagePanel();

        MessagePanel messagePanel = uiManager.GetMessagePanel();

        messagePanel.SetTitle(title);
        messagePanel.SetContent(content);
    }
}
