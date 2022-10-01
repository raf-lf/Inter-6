using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SpiritGate : MonoBehaviour
{
    [SerializeField] private SpiritLock[] connectedLocks = new SpiritLock[0];
    public ParticleSystem[] lockParticles = new ParticleSystem[0];
    private Animator animator;
    [SerializeField] private CinemachineVirtualCamera focusCamera;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        for (int i = 0; i < connectedLocks.Length; i++)
        {
            connectedLocks[i].lockIndex = i;
            connectedLocks[i].connectedGate = this;
        }
    }
    public IEnumerator OpenLockSequence(int lockIndex)
    {
        GameManager.PlayerControl = false;
        GameManager.CanvasManager.AnimateOverlay(OverlayAnimation.Black, .5f);
        yield return new WaitForSeconds(.5f);
        GameManager.CameraManager.brain.m_DefaultBlend.m_Time = 0;
        focusCamera.enabled = true;
        GameManager.CanvasManager.AnimateOverlay(OverlayAnimation.Off, .5f);
        yield return new WaitForSeconds(1f);
        lockParticles[lockIndex].Stop();
        yield return new WaitForSeconds(1f);
        CheckLocks();
        yield return new WaitForSeconds(2f);
        GameManager.CanvasManager.AnimateOverlay(OverlayAnimation.Black, .5f);
        yield return new WaitForSeconds(.5f);
        focusCamera.enabled = false;
        yield return new WaitForEndOfFrame();
        GameManager.CameraManager.brain.m_DefaultBlend.m_Time = GameManager.CameraManager.standardBlendTime;
        GameManager.CanvasManager.AnimateOverlay(OverlayAnimation.Off, .5f);
        GameManager.PlayerControl = true;
    }

    private void CheckLocks()
    {
        int locksActive = 0;

        foreach (var item in connectedLocks)
        {
            if(item.active)
                locksActive++;
        }

        if (locksActive <= 0)
            Open();

    }

    public void Open()
    {
        animator.SetBool("open", true);
    }

    public void Close()
    {
        animator.SetBool("open", false);
        foreach (var item in lockParticles)
        {
            item.Play();
        }

        foreach (var item in connectedLocks)
        {
            item.ResetLock();
        }
    }
}

