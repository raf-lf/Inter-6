using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemySfx : MonoBehaviour
{
    [SerializeField]
    public string[] eventsPath;

    FMOD.Studio.EventInstance[] soundFxEvents;



    private void Start()
    {
        for(int i = 0; i< eventsPath.Length; i++) 
        {
            soundFxEvents[i] = RuntimeManager.CreateInstance(eventsPath[i]);
        }
    }

    public void PlaySfx(int i) 
    {

        if (i < eventsPath.Length) RuntimeManager.PlayOneShot(eventsPath[i]);
        else Debug.Log("Esse parametro está acima do index de Eventos");

    }


}
