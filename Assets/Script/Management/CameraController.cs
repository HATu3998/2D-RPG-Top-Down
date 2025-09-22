using Unity.Cinemachine;
using UnityEngine;


public class CameraController : SingleTon<CameraController>
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private CinemachineCamera cinemachineVirtualCamera;
    public void SetPlayerCameraFollow()
    {
        cinemachineVirtualCamera = FindObjectOfType<CinemachineCamera>();
        cinemachineVirtualCamera.Follow = PlayerController.Instance.transform;
    }

}
