using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemySfx : MonoBehaviour
{
    [SerializeField]
    public StudioEventEmitter[] sfxEmitter;

    public void PlaySfx(int i) 
    {
        sfxEmitter[i].Play();

    }


}
