using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpiritLock : MonoBehaviour
{
    public bool active = true;
    [HideInInspector] public SpiritGate connectedGate;
    [HideInInspector] public int lockIndex;
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void OpenLock()
    {
        active = false;
        anim.SetBool("active", false);
        StartCoroutine(connectedGate.OpenLockSequence(lockIndex));
    }

    public void ResetLock()
    {
        active = true;
        anim.SetBool("active", true);
    }
}
