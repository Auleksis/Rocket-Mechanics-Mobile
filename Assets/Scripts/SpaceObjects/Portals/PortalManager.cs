using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    [SerializeField] GameObject[] availablePortals;

    [SerializeField] MinimapScript minimap;

    [SerializeField] int portalMinimapIndex = 0;
    
    private List<GameObject> allPortals;

    private int currentPortalIndex;

    private GameObject currentPortal;

    void Start()
    {
        allPortals = new List<GameObject>(availablePortals);

        foreach (GameObject portal in allPortals)
        {
            Portal p = portal.GetComponent<Portal>();

            p.SetPortalManager(this);

            portal.SetActive(false);
        }

        SetCurrentPortal();
    }

    
    void Update()
    {
        
    }

    public void SetCurrentPortal()
    {
        if(currentPortal != null)
        {
            currentPortal.SetActive(false);
        }

        GameObject previousPortal = currentPortal;

        do
        {
            currentPortalIndex = Random.Range(0, availablePortals.Length);

            currentPortal = allPortals[currentPortalIndex];

        } while (previousPortal == currentPortal);

        currentPortal.SetActive(true);

        minimap.ChangeSpaceObject(portalMinimapIndex, currentPortal);
    }
}
