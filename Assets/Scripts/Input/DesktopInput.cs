using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DesktopInput : MonoBehaviour
{
    [SerializeField] RocketController controller;

    [SerializeField] CinemachineVirtualCamera sceneCamera;
    [SerializeField] float scrollSensetivity = 1.0f;
    [SerializeField] float maxOrhtoSize = 70f;
    [SerializeField] float minOrthoSize = 20f;

    [SerializeField] UIManager uiManager;

    Vector3 inputDirection;

    bool force = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        float forceInput = Input.GetAxisRaw("Jump");

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (forceInput != 0)
            force = true;
        else
            force = false;


        inputDirection = new Vector3(horizontalInput, verticalInput, 0);

        if(scroll != 0)
        {
            ScrollCamera(scroll);   
        }


        if (Input.GetKeyDown(KeyCode.U))
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            Time.timeScale += 0.1f;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale -= 0.1f;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            uiManager.CallEscMenu();
        }

        uiManager.SetTimeScaleInfo("Ускорение времени: " + Time.timeScale.ToString());
    }

    private void FixedUpdate()
    {
        controller.HandleInput(inputDirection, force);
    }

    private void ScrollCamera(float scrollInput)
    {
        //with inverted input
        
        float scrollValue = sceneCamera.m_Lens.OrthographicSize;
            
        scrollValue += -scrollInput * scrollSensetivity;

        scrollValue = Mathf.Clamp(scrollValue, minOrthoSize, maxOrhtoSize);

        sceneCamera.m_Lens.OrthographicSize = scrollValue;
    }
}
