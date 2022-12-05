using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD;
using System;

public class PlayerSfx : MonoBehaviour
{
    public static string explosionEventPath = "event:/SFX/explosion";
    public static string collisionEventPath = "event:/SFX/PLAYER/hit_boat";

    public static FMOD.Studio.EventInstance sfxEngine;
    public static FMOD.Studio.EventInstance collisionEvent;
    public static FMOD.Studio.EventInstance explosionEvent;

    private void Awake()
    {
        explosionEvent = RuntimeManager.CreateInstance(explosionEventPath);
        sfxEngine = RuntimeManager.CreateInstance("event:/SFX/PLAYER/boat_engine");
        collisionEvent = RuntimeManager.CreateInstance(collisionEventPath);
        
    }

    public static void EngineOnOff(bool on) 
    {
        if (on == true)
        {
            sfxEngine.start();
            sfxEngine.setParameterByName("rpm", 0.6f);

        }
        else
        {

            sfxEngine.setParameterByName("rpm", 0);
            sfxEngine.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
          //  sfxEngine.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        }
        
        
        }
    

}
