using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransition : AbstractAfterEffect
{
    [SerializeField] CinemachineVirtualCamera virtualCamera;

    [SerializeField] float transitionDurationSeconds;

    [SerializeField] float targetOrthoSize;

    [SerializeField] Transform targetPosition;

    [SerializeField] Transform camStaff;

    private float currentOrthoSize;

    public override void Apply()
    {
        currentOrthoSize = virtualCamera.m_Lens.OrthographicSize;

        StartCoroutine(SetCamOrtho());
    }

    private IEnumerator SetCamOrtho()
    {
        float elapsedTime = 0f;

        camStaff.position = targetPosition.position;

        float t = 0f;

        virtualCamera.Follow = camStaff;

        while (elapsedTime < transitionDurationSeconds)
        {
            elapsedTime += Time.deltaTime;

            t = Mathf.Lerp(currentOrthoSize, targetOrthoSize, elapsedTime / transitionDurationSeconds);

            virtualCamera.m_Lens.OrthographicSize = t;

            Vector3 p = new Vector3(targetPosition.position.x, targetPosition.position.y, virtualCamera.transform.position.z);

            camStaff.position = Vector3.Lerp(camStaff.position, p, elapsedTime / transitionDurationSeconds);

            yield return null;
        }

        virtualCamera.Follow = targetPosition;
    }
}
