using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;
using FMODUnity;
public class SoundManager : MonoBehaviour
{
   
    public string mainSoundTrackPath;
    FMOD.Studio.EventInstance mainTrack;

    public string soundScapePath;
    FMOD.Studio.EventInstance soundScape;


    private void Awake()
    {
        mainTrack = FMODUnity.RuntimeManager.CreateInstance(mainSoundTrackPath);
        mainTrack.start();

        soundScape = FMODUnity.RuntimeManager.CreateInstance(soundScapePath);
        soundScape.start();
     




    }

}
