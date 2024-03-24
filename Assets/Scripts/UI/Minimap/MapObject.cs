using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapObject : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [SerializeField] TMP_Text text;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUp(Color color, string name)
    {
        spriteRenderer.material.color = color;

        text.text = name;
    }
}
