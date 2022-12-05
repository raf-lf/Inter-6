using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD;

public class PlayerSfx : MonoBehaviour
{
    public StudioEventEmitter sfxEngine;
    public static string explosionEventPath = "event:/SFX/explosion";
    public static string collisionEventPath = "event:/SFX/PLAYER/hit_boat";

    public static FMOD.Studio.EventInstance collisionEvent;
    public static FMOD.Studio.EventInstance explosionEvent;

    private void Awake()
    {
        explosionEvent = RuntimeManager.CreateInstance(explosionEventPath);
        collisionEvent = RuntimeManager.CreateInstance(collisionEventPath);
        
    }
    

}
