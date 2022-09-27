using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ES_ChangeFlag : ES_EventBase
{
    [SerializeField] private bool flagState;
    [SerializeField] private Flag flagToActivate;

    public override void Play(EventSequence cine)
    {
        base.Play(cine);
        flagToActivate.flagActive = flagState;

        StartCoroutine(NextEvent());
    }
}
