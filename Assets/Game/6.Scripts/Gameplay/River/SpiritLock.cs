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

    public void OpenLock(bool skipSequence)
    {
        active = false;
        anim.SetBool("active", false);

        StartCoroutine(connectedGate.OpenLockSequence(lockIndex, skipSequence));

        if (skipSequence)
            GetComponent<LanternTarget>().lanternImmune = true;

        else
        {
            Saveable saveable = GetComponent<Saveable>();
            if (saveable != null)
                saveable.Save(true);
        }

    }

    public void ResetLock()
    {
        Saveable saveable = GetComponent<Saveable>();
        if (saveable != null)
            saveable.Save(false);

        active = true;
        anim.SetBool("active", true);
    }
}
