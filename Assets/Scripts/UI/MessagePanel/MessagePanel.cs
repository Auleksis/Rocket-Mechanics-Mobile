using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MessagePanel : AbstractPanel
{
    [SerializeField] TMP_Text title;
    [SerializeField] TMP_Text content;


    public void SetTitle(string title)
    {
        this.title.text = title;
    }

    public void SetContent(string text)
    {
        this.content.text = text;
    }

    public override void Open()
    {
        title.gameObject.SetActive(true);
        content.gameObject.SetActive(true);
    }

    public override void Close()
    {
        title.gameObject.SetActive(false);
        content.gameObject.SetActive(false);
    }
}
