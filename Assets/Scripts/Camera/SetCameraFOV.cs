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
    private bool IsInitialized { get; set; }
    
    public void Initialize()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        IsInitialized = true;
    }
    public void CameraZoomIn()
    {
        if (!IsInitialized)
        {
            Initialize();
        }
        virtualCamera.m_Lens.FieldOfView = zoomInFOV;
    }
    
    public void CameraZoomOut()
    {
        if (!IsInitialized)
        {
            Initialize();
        }
        virtualCamera.m_Lens.FieldOfView = zoomOutFOV;
    }
}
