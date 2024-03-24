using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlayerObject : MonoBehaviour
{
    [SerializeField] UIManager uiManager;

    [SerializeField] AbstractAfterEffect[] effectIfDestroyed;

    [SerializeField] string messageIfDestroyed;

    public bool isSimulated = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(isSimulated)
        {
            return;
        }


        if (collision.collider.gameObject.tag.Equals("Player"))
        {
            SimulatedObject rocketController = collision.gameObject.GetComponent<SimulatedObject>();

            if (rocketController.isSimulatedOnAnotherScene)
                return;

            foreach (var effect in effectIfDestroyed)
            {
                effect.Apply();
            }

            uiManager.CallCrashMenu(messageIfDestroyed);
        }
    }
}
