using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CE_Wait : CinematicEvent
{
    [SerializeField] private float waitTime;

    public override void Play(Cinematic cine)
    {
        base.Play(cine);
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(waitTime);
        NextEvent();
    }
}
