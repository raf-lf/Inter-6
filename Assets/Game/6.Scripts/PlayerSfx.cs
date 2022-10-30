using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD;

public class PlayerSfx : MonoBehaviour
{
    public string engineEventPath;
    public string collisionEventPath;
    public string lanternEventPath;

    public FMOD.Studio.EventInstance engineEvent;
    public FMOD.Studio.EventInstance collisionEvent;
    public FMOD.Studio.EventInstance lanternEvent;

    private void Awake()
    {

        engineEvent = RuntimeManager.CreateInstance(engineEventPath);
        collisionEvent = RuntimeManager.CreateInstance(collisionEventPath);
        lanternEvent = RuntimeManager.CreateInstance(lanternEventPath);

        engineEvent.start();
        engineEvent.setParameterByName("rpm", 0.4f );

    }

}
