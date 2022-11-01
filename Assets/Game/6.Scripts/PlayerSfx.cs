using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD;

public class PlayerSfx : MonoBehaviour
{
    static string engineEventPath = "event:/SFX/PERSONAGEM/motor_barco";
    static string collisionEventPath = "event:/SFX/PERSONAGEM/colisao_barco";
    public static string lanternEventPath = "event:/SFX/PERSONAGEM/lanterna";

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
