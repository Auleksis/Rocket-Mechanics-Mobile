using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private bool isPlayerInside = false;

    private PortalManager manager;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            isPlayerInside = true;

            manager.SetCurrentPortal();
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
            isPlayerInside = false;
    }

    public bool IsPlayerInside()
    {
        return isPlayerInside;
    }

    public void SetPortalManager(PortalManager manager)
    {
       this.manager = manager;
    }
}
