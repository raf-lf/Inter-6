using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ES_ShowLog : ES_EventBase
{
    [SerializeField] private Log logToShow;
    public override void Play(EventSequence cine)
    {
        base.Play(cine);

        logToShow.PlayLog();

        StartCoroutine(NextEvent());
    }

}
