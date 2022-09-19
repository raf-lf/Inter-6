using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CE_Wait : CinematicEvent
{
    public override void Play(Cinematic cine)
    {
        base.Play(cine);
        StartCoroutine(NextEvent());
    }
}
