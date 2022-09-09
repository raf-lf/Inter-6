using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public enum CameraType { Ship, Lantern }

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCameraBase ShipCamera;
    public CinemachineVirtualCameraBase LanternCamera;

    private void Awake()
    {
        GameManager.CameraManager = this;
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
