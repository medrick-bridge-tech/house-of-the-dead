using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera surgeryRoomCamera;
    [SerializeField] private CinemachineVirtualCamera conferenceRoomCamera;
    [SerializeField] private CinemachineVirtualCamera officeRoomCamera;

    private Dictionary<string, CinemachineVirtualCamera> roomCameras = new Dictionary<string, CinemachineVirtualCamera>();

    private void Start()
    {
        roomCameras.Add("SurgeryRoom", surgeryRoomCamera);
        roomCameras.Add("ConferenceRoom", conferenceRoomCamera);
        roomCameras.Add("OfficeRoom", officeRoomCamera);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (roomCameras.TryGetValue(other.tag, out CinemachineVirtualCamera targetCamera))
        {
            SwitchCamera(targetCamera);
        }
    }

    private void SwitchCamera(CinemachineVirtualCamera targetCamera)
    {
        DisableAllCameras();
        targetCamera.gameObject.SetActive(true);
    }

    private void DisableAllCameras()
    {
        foreach (var camera in roomCameras.Values)
        {
            camera.gameObject.SetActive(false);
        }
    }
}