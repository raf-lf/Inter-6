using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ES_Wait : ES_EventBase
{
    public override void Play(EventSequence cine)
    {
        base.Play(cine);
        StartCoroutine(NextEvent());
    }
}
