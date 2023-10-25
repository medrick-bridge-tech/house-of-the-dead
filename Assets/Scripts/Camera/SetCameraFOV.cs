using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class SetCameraFOV : MonoBehaviour
{
    [SerializeField] private float zoomInFOV;
    [SerializeField] private float zoomOutFOV;
    private CinemachineVirtualCamera virtualCamera;

    private void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void CameraZoomIn()
    {
        virtualCamera.m_Lens.FieldOfView = zoomInFOV;
    }
    
    public void CameraZoomOut()
    {
        virtualCamera.m_Lens.FieldOfView = zoomOutFOV;
    }
}
