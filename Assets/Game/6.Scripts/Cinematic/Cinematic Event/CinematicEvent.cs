using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicEvent : MonoBehaviour
{
    protected Cinematic currentCinematic;

    public virtual void Play(Cinematic cine)
    {
        currentCinematic = cine;
    }

    public void NextEvent()
    {
        currentCinematic.PlayCinematic();
    }
}
