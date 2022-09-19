using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cinematic : MonoBehaviour
{
    [SerializeField] private bool CinematicMode;
    [SerializeField] private List<CinematicEvent> eventList = new List<CinematicEvent>();
    [SerializeField] private int currentEventIndex;
    private void Awake()
    {
        if(eventList.Count <= 0)
           eventList.AddRange(GetComponentsInChildren<CinematicEvent>());
    }
    public void StartCinematic()
    {
        if (CinematicMode)
        {
            GameManager.DialogueSystem.CinematicMode(true);
            GameManager.PlayerControl = false;
            GameManager.PlayerClickControl = false;
        }

        currentEventIndex = 0;
        ContinueCinematic();
    }

    public void ContinueCinematic()
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
        if (CinematicMode)
        {

            GameManager.DialogueSystem.CinematicMode(false);
            GameManager.PlayerControl = true;
            GameManager.PlayerClickControl = true;
        }
    }


}
