using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CE_Dialogue : CinematicEvent
{
    public Dialogue[] scenes = new Dialogue[0];

    public override void Play(Cinematic cine)
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

