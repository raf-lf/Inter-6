using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ES_UnityEvent : ES_EventBase
{
    [SerializeField] private UnityEvent unityEvent;
    public override void Play(EventSequence cine)
    {
        base.Play(cine);
        unityEvent?.Invoke();
        StartCoroutine(NextEvent());
    }
}
