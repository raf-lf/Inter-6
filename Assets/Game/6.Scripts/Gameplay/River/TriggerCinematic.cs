using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCinematic : MonoBehaviour
{
    [SerializeField] bool repeats;
    //THIS NEEDS TO BE A FLAG OR SOMETHING
    private bool alreadyPlayed;
    [SerializeField] private Cinematic cinematicToPlayFlagCleared;
    [SerializeField] private Cinematic cinematicToPlayFlagBlocked;
    private FlagLock flagLock;

    private void Awake()
    {
        flagLock = GetComponent<FlagLock>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !alreadyPlayed)
        {
            alreadyPlayed = true;
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
