using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD;

public class PlayerSfx : MonoBehaviour
{
    public static string engineEventPath = "event:/SFX/PLAYER/boat_engine";
    public static string collisionEventPath = "event:/SFX/PLAYER/hit_boat";
    public static string lanternEventPath = "event:/SFX/PLAYER/lamp";

    public static FMOD.Studio.EventInstance engineEvent;
    public static FMOD.Studio.EventInstance collisionEvent;
    public static FMOD.Studio.EventInstance lanternEvent;

    private void Awake()
    {
        engineEvent = RuntimeManager.CreateInstance(engineEventPath);
        collisionEvent = RuntimeManager.CreateInstance(collisionEventPath);
        lanternEvent = RuntimeManager.CreateInstance(lanternEventPath);
        
    }
    

}
