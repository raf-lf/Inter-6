using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public enum CameraType { Ship, Lantern }
public enum OverlayEffectType { None, Fog, Ethereal}

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCameraBase ShipCamera;
    private CinemachineFreeLook shipCamFreeLook;
    public CinemachineVirtualCameraBase LanternCamera;
    public CinemachineBrain brain;
    public float standardBlendTime = .5f;
    private float memoryFov;

    [Header ("Overlay Effects")]
    [SerializeField] private OverlayEffect[] overlayEffects = new OverlayEffect[0];

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
        shipCamFreeLook = ShipCamera.GetComponent<CinemachineFreeLook>();
        memoryFov = shipCamFreeLook.m_Lens.FieldOfView;
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

    public void CameraCloseUp(float customFov)
    {
        DOTween.To(()=> shipCamFreeLook.m_Lens.FieldOfView, x=> shipCamFreeLook.m_Lens.FieldOfView = x, memoryFov * customFov, 1);
    }

    public void ChangeOverlayEffect(OverlayEffectType effect)
    {
        foreach (var item in overlayEffects)
        {
            item.ActivateDeactivateEffect(item.associatedType == effect ? true : false);
        }

    }
}

[Serializable]
public class OverlayEffect
{
    public OverlayEffectType associatedType;
    public Volume volumeFilter;
    public ParticleSystem[] particles = new ParticleSystem[0];

    public void ActivateDeactivateEffect(bool active)
    {
        DOTween.To(() => volumeFilter.weight, x => volumeFilter.weight= x, active ? 1 : 0 , .5f);

        if (active)
        {
            for (int i = 0; i < particles.Length; i++)
            {
                var main = particles[i].main;
                main.simulationSpeed = 1;
            }

            particles[0].Play();
        }
        else
        {
            for (int i = 0; i < particles.Length; i++)
            {
                var main = particles[i].main;
                main.simulationSpeed = 3;
            }

            particles[0].Stop();
        }

    }

}
