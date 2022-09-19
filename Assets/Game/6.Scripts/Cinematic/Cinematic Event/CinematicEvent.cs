using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CinematicEvent : MonoBehaviour
{
    protected Cinematic currentCinematic;
    [SerializeField] protected float nextEventDelay;

    public virtual void Play(Cinematic cine)
    {
        currentCinematic = cine;
    }

    public IEnumerator NextEvent()
    {
        yield return new WaitForSeconds(nextEventDelay);
        currentCinematic.ContinueCinematic();
    }
}
