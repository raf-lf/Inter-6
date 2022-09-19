using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CE_UnityEvent : CinematicEvent
{
    [SerializeField] private UnityEvent unityEvent;
    public override void Play(Cinematic cine)
    {
        base.Play(cine);
        unityEvent?.Invoke();
        StartCoroutine(NextEvent());
    }
}
