using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayQuestlineEventSequence : MonoBehaviour
{
    [SerializeField] private Questline questline;

    [SerializeField] private EventSequence[] eventSequences = new EventSequence[5];

    public void PlayCorrectSequence()
    {
        eventSequences[questline.currentIndex+1].gameObject.SetActive(true);
        eventSequences[questline.currentIndex+1].StartCinematic();
    }
}
