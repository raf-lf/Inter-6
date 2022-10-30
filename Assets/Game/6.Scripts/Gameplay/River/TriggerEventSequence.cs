using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEventSequence : MonoBehaviour
{
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !disablePlaying)
        {
            if(!repeats)
                disablePlaying = true;
            AttemptPlayCinematic();
        }
    }

    public void AttemptPlayCinematic()
    {
        if(flagLock != null)
        {
            if(flagLock.FlagsCleared())
                PlayCinematic(true);
            else
                PlayCinematic(false);
        }
        else
            PlayCinematic(true);
    }

    public void PlayCinematic(bool flagCleared)
    {
        if (flagCleared)
            cinematicToPlayFlagCleared.StartCinematic();
        else
            cinematicToPlayFlagBlocked.StartCinematic();

    }
}