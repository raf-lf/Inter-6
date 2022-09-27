using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ES_EventBase : MonoBehaviour
{
    protected EventSequence currentCinematic;
    [SerializeField] protected float nextEventDelay;

    public virtual void Play(EventSequence cine)
    {
        currentCinematic = cine;
    }

    public IEnumerator NextEvent()
    {
        yield return new WaitForSeconds(nextEventDelay);
        currentCinematic.ContinueCinematic();
    }
}
