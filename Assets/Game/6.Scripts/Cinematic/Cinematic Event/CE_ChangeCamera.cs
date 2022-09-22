using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CE_ChangeCamera : CinematicEvent
{

    [SerializeField] private CinemachineVirtualCameraBase changingCamera;
    [SerializeField] private bool activeCam = true;
    [SerializeField] private float blendTime = 1;

    public override void Play(Cinematic cine)
    {
        base.Play(cine);
        GameManager.CameraManager.brain.m_DefaultBlend.m_Time = blendTime;
        GameManager.CameraManager.EnableDisableBaseCams(!activeCam);
        changingCamera.enabled = activeCam;

        StartCoroutine(DelayBlendChange());
        StartCoroutine(NextEvent());
    }

    private IEnumerator DelayBlendChange()
    {
        yield return new WaitForSeconds(blendTime);
        GameManager.CameraManager.brain.m_DefaultBlend.m_Time = GameManager.CameraManager.standardBlendTime;
    }
}
