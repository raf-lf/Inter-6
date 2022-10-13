using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ES_ChangeCamera : ES_EventBase
{

    [SerializeField] private CinemachineVirtualCameraBase changingCamera;
    [SerializeField] private bool activeCam = true;
    [SerializeField] private float blendTime = 1;

    public override void Play(EventSequence cine)
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