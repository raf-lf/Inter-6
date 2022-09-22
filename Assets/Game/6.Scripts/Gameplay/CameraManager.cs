using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public enum CameraType { Ship, Lantern }

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCameraBase ShipCamera;
    public CinemachineVirtualCameraBase LanternCamera;
    public CinemachineBrain brain;
    public float standardBlendTime = .5f;

    private void Awake()
    {
        brain = Camera.main.GetComponent<CinemachineBrain>();
        GameManager.CameraManager = this;
    }

    private void Start()
    {
        ShipCamera.Follow = GameManager.PlayerInstance.cameraTargetPlayer;
        ShipCamera.LookAt = GameManager.PlayerInstance.cameraTargetPlayer;
        LanternCamera.Follow = GameManager.PlayerInstance.cameraTargetLantern;
        LanternCamera.LookAt = GameManager.PlayerInstance.cameraTargetLantern;
    }
    public void EnableDisableBaseCams(bool active)
    {
        ShipCamera.enabled = active;
        LanternCamera.enabled = active;
    }

    public void SwitchCamera(CameraType type)
    {
        CinemachineVirtualCameraBase chosenCam = null;

        switch (type)
        {
            case CameraType.Ship:
                chosenCam = ShipCamera;
                break;
            case CameraType.Lantern:
                chosenCam = LanternCamera;
                break;
        }

        List<CinemachineVirtualCameraBase> allCams = new List<CinemachineVirtualCameraBase>();
        allCams.Add(ShipCamera);
        allCams.Add(LanternCamera);
        allCams.Remove(chosenCam);

        chosenCam.enabled = true;

        foreach (var item in allCams)
        {
            item.enabled = false;
        }

        
    }

}
