using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD;

public class SoundSource : MonoBehaviour
{
    
    public string eventRef;
    public FMOD.Studio.EventInstance sEvent;
    public string parameter;

    private void Start()
    {
    }
    public void CreateSoundEvent(){sEvent = FMODUnity.RuntimeManager.CreateInstance(eventRef);}
    public void CreateSoundEvent(string eventPath){ sEvent = FMODUnity.RuntimeManager.CreateInstance(eventPath);}
    public void SetParameter(float value){sEvent.setParameterByName(parameter, value);}
    public void SetParameter(string param, float value){sEvent.setParameterByName(param, value);}
    public void StartEvent() {sEvent.start();}
    public void StopEvent(FMOD.Studio.STOP_MODE stopMode) {sEvent.stop(stopMode);}

    public void PlayOneShot() 
    {
        FMODUnity.RuntimeManager.PlayOneShot(eventRef);
    }



}
