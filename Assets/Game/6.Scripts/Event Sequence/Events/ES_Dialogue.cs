using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ES_Dialogue : ES_EventBase
{
    public Dialogue[] scenes = new Dialogue[0];

    public override void Play(EventSequence cine)
    {
        base.Play(cine);
        GameManager.DialogueSystem.StartDialogue(this);
    }
}

[Serializable]
public class Dialogue
{
    public ActorData actor;
    public ActorEmotion emotion;
    [TextArea(2,10)]
    public string line;
}

