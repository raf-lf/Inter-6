using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinematic : MonoBehaviour
{
    [SerializeField] private List<CinematicEvent> eventList = new List<CinematicEvent>();
    [SerializeField] private int currentEventIndex;

    public void StartCinematic()
    {
        currentEventIndex = 0;
        PlayCinematic();
    }

    public void PlayCinematic()
    {
        if (currentEventIndex >= eventList.Count)
        {
            EndCinematic();
            return;
        }

        eventList[currentEventIndex].Play(this);
        currentEventIndex++;
            
    }

    public void EndCinematic()
    {

    }


}
