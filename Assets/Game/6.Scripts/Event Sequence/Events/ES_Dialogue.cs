using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ES_Dialogue : ES_EventBase
{
    public Dialogue dialogue;

    public override void Play(EventSequence cine)
    {
        base.Play(cine);
        GameManager.DialogueSystem.StartDialogue(this);
    }
}


