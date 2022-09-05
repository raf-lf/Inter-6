using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCinematic : MonoBehaviour
{
    [SerializeField] bool alreadyPlayed;
    public Cinematic cinematicToPlay;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !alreadyPlayed)
        {
            alreadyPlayed = true;
            PlayCinematic();
        }
    }

    public void PlayCinematic()
    {
        cinematicToPlay.StartCinematic();
    }
}
