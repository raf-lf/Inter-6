using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class ES_SfxEventEmitter : ES_EventBase
{
    public enum EmitterState {play, stop};
    public EmitterState setting;
    public StudioEventEmitter emitter;



    public override void Play(EventSequence cine)
    {
        base.Play(cine);

        switch(setting)
        {
            case EmitterState.play: 
                emitter.Play(); 
                break;
            case EmitterState.stop: 
                emitter.Stop(); 
                break;
        }

        StartCoroutine(NextEvent());
    }

}
