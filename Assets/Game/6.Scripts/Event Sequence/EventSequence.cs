using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSequence : MonoBehaviour
{
    [SerializeField] private bool CinematicMode;
    [SerializeField] private List<ES_EventBase> eventList = new List<ES_EventBase>();
    [SerializeField] private int currentEventIndex;
    [SerializeField] private bool repeats;

    private void Awake()
    {
        if(eventList.Count <= 0)
           eventList.AddRange(GetComponentsInChildren<ES_EventBase>());
    }
    public void StartCinematic()
    {
        if (!repeats)
        {
            if (SaveSystem.Load(SaveableDataType.eventSequence, gameObject.name))
                return;
            else
                SaveSystem.Save(SaveableDataType.eventSequence, gameObject.name, true);

        }

        if (CinematicMode)
        {
            SoundManager.OpenCloseSnapshot(true);
            GameManager.DialogueSystem.CinematicMode(true);
            GameManager.CanvasManager.ShowHud(false);
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
            SoundManager.OpenCloseSnapshot(false);
            GameManager.DialogueSystem.CinematicMode(false);
            GameManager.CanvasManager.ShowHud(true);
            GameManager.PlayerControl = true;
            GameManager.PlayerClickControl = true;
        }
    }


}
