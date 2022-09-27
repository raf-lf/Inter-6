using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritLock : MonoBehaviour
{
    [SerializeField] Animator selfAnimator;
    [SerializeField] Animator connectedAnimator;

    private void Awake()
    {
        selfAnimator = GetComponent<Animator>();
    }

    public void OpenLock()
    {
        selfAnimator.SetBool("open", true);
        connectedAnimator.SetBool("open", true);
    }
}
