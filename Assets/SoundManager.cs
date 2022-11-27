using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static FMOD.Studio.EventInstance soundTrackEvent;
    public static FMOD.Studio.EventInstance soundScapeEvent;
    public static FMOD.Studio.EventInstance snapshot;
    public static int hazardNumber;

    public void Start()
    {
        HandleSounds();
    }
    public static void OpenCloseSnapshot(bool open)
    {
        if (open) snapshot.start();
        else snapshot.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

    }
    public static void HazardOnOff(bool hazard) 
    {
        if (hazard == true) soundTrackEvent.setParameterByName("draga",1);
        else soundTrackEvent.setParameterByName("draga",1);
    }
    void HandleSounds()
    {
        IslandManager island = FindObjectOfType<IslandManager>();
        string soundTrackPath;
        if (island != null) soundTrackPath = "event:/MUSIC/calm";
        else soundTrackPath = "event:/MUSIC/main";
        soundTrackEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        soundScapeEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        soundTrackEvent = RuntimeManager.CreateInstance(soundTrackPath);
        soundScapeEvent = RuntimeManager.CreateInstance("event:/SOUNDSCAPE/environment");
        snapshot = RuntimeManager.CreateInstance("snapshot:/dialogo");
        soundTrackEvent.start();
        soundScapeEvent.start();

    }


}
