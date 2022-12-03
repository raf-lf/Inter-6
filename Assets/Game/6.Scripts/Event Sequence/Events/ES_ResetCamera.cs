using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ES_ResetCamera : ES_EventBase
{

    [SerializeField] private float blendTime = 0;
    public override void Play(EventSequence cine)
    {
        base.Play(cine);

        GameManager.CameraManager.brain.m_DefaultBlend.m_Time = blendTime;

        foreach (var item in transform.parent.GetComponentsInChildren<ES_ChangeCamera>())
        {
            item.changingCamera.enabled = false;
        }
        /*
        foreach (var item in transform.parent.GetComponentsInChildren<CinemachineVirtualCameraBase>())
        {
            item.enabled = false;
        }
        */

        GameManager.CameraManager.EnableDisableBaseCams(true);

        if (ES_ChangeCamera.lastCamera != null)
            ES_ChangeCamera.lastCamera = null;

        StartCoroutine(DelayBlendChange());
        StartCoroutine(NextEvent());
    }

    private IEnumerator DelayBlendChange()
    {
        yield return new WaitForSeconds(blendTime);
        GameManager.CameraManager.brain.m_DefaultBlend.m_Time = GameManager.CameraManager.standardBlendTime;
    }
}
