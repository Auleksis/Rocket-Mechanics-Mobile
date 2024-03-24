using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HelpPanel : AbstractPanel
{
    [SerializeField] TMP_Text content;

    public void SetContent(string text)
    {
        this.content.text = text;
    }

    public void SetContentColor(Color color)
    {
        content.color = color;
    }

    public override void Open()
    {
        content.gameObject.SetActive(true);
    }

    public override void Close()
    {
        content.gameObject.SetActive(false);
    }
}
