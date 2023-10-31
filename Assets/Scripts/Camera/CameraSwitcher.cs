using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] private CinemachineBrain cinemachineBrain;
    [SerializeField] private CinemachineVirtualCamera surgeryRoomCamera;
    [SerializeField] private CinemachineVirtualCamera conferenceRoomCamera;
    [SerializeField] private CinemachineVirtualCamera officeRoomCamera;

    private const int puzzleSelectPriority = 3, puzzleDeselectPriority = -1;
    private const int roomSelectPriority = 2, roomDeselctPriority = 1;
    
    private Dictionary<string, CinemachineVirtualCamera> roomCameras = new Dictionary<string, CinemachineVirtualCamera>();
    private CinemachineVirtualCamera activeCamera;
    
    private void Start()
    {
        roomCameras.Add("SurgeryRoom", surgeryRoomCamera);
        roomCameras.Add("ConferenceRoom", conferenceRoomCamera);
        roomCameras.Add("OfficeRoom", officeRoomCamera);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (roomCameras.TryGetValue(other.tag, out CinemachineVirtualCamera targetCamera))
            SwitchRoomCamera(targetCamera);
    }

    private void SwitchRoomCamera(CinemachineVirtualCamera targetCamera)
    {
        foreach (var camera in roomCameras.Values)
            camera.Priority = roomDeselctPriority;

        SetBlendStyle(CinemachineBlendDefinition.Style.Cut);
        targetCamera.Priority = roomSelectPriority;
    }
    
    public void SwitchToPuzzleCamera(CinemachineVirtualCamera targetCamera)
    {
        SetBlendStyle(CinemachineBlendDefinition.Style.EaseInOut);
        targetCamera.Priority = puzzleSelectPriority;
        activeCamera = targetCamera;
    }

    public void DefocusPuzzleCamera()
    {
        activeCamera.Priority = puzzleDeselectPriority;
    }

    private void SetBlendStyle(CinemachineBlendDefinition.Style blendStyle)
    {
        cinemachineBrain.m_DefaultBlend.m_Style = blendStyle;
    }
}