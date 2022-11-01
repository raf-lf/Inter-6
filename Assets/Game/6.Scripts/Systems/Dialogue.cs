using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Scriptables/Cinematic/Dialogue")]
public class Dialogue : ScriptableObject
{
    public DialogueLine[] lines = new DialogueLine[0];

}

[Serializable]
public class DialogueLine
{
    public ActorData actor;
    public ActorEmotion emotion;
    public PortraitPosition portraitPosition;
    [TextArea(3, 10)]
    public string line;
}
