using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ES_OverlayControl : ES_EventBase
{
    [SerializeField] private OverlayAnimation overlayEffect;
    [SerializeField] private float time = 1;

    public override void Play(EventSequence cine)
    {
        base.Play(cine);
        GameManager.CanvasManager.AnimateOverlay(overlayEffect, time);
        StartCoroutine(NextEvent());
    }
}
