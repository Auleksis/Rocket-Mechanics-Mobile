using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBox : MonoBehaviour
{
    [SerializeField] UIManager uiManager;

    [SerializeField] AbstractAfterEffect[] effectIfExit;

    [SerializeField] string messageIfExit;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            foreach (var effect in effectIfExit)
            {
                effect.Apply();
            }

            uiManager.CallCrashMenu(messageIfExit);
        }
    }
}
