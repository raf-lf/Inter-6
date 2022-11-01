using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TriggerType {OnEntry, OnStart}

public class TriggerEventSequence : MonoBehaviour
{
    [SerializeField] private TriggerType triggerType = TriggerType.OnEntry;
    [SerializeField] private float delay;
    [SerializeField] bool repeats;
    //THIS NEEDS TO BE A FLAG OR SOMETHING
    private bool disablePlaying;
    [SerializeField] private EventSequence cinematicToPlayFlagCleared;
    [SerializeField] private EventSequence cinematicToPlayFlagBlocked;
    private FlagLock flagLock;

    private void Awake()
    {
        flagLock = GetComponent<FlagLock>();
    }

    private void Start()
    {
        if (triggerType != TriggerType.OnStart)
            return;

        AttemptPlayCinematic();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (triggerType != TriggerType.OnEntry || !other.gameObject.CompareTag("Player"))
            return;
        
        AttemptPlayCinematic();
    }

    public void AttemptPlayCinematic()
    {
        if (disablePlaying)
            return;
        
        if(!repeats)
            disablePlaying = true;
        
        if(flagLock != null)
        {
            if (flagLock.FlagsCleared())
                StartCoroutine(DelayEvent(true));
            else
                StartCoroutine(DelayEvent(false));
        }
        else
            StartCoroutine(DelayEvent(true));
    }

    private IEnumerator DelayEvent(bool flagCleared)
    {
        yield return new WaitForSeconds(delay);
        PlayCinematic(flagCleared);
    }

    public void PlayCinematic(bool flagCleared)
    {
        if (flagCleared)
            cinematicToPlayFlagCleared.StartCinematic();
        else
            cinematicToPlayFlagBlocked.StartCinematic();

    }
}
