using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoText : MonoBehaviour
{
    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    public void SetText(string text)
    {
        this.text.text = text;
    }

    public void SetColor(Color color)
    {
        text.color = color;
    }
}
