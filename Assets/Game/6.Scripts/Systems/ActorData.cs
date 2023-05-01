using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActorEmotion { neutral, happy, sad, angry, confused }

[CreateAssetMenu(fileName = "ActorData", menuName = "Scriptables/Cinematic/Actor")]
public class ActorData : ScriptableObject
{
    public string actorPTName;
    public string actorNameUnknown;
    public string actonENName;
    public Sprite portraitNeutral;
    public Sprite portraitHappy;
    public Sprite portraitSad;
    public Sprite portraitAngry;
    public Sprite portraitConfused;
    public AudioClip[] sfxVoice = new AudioClip[0];

    public string ReturnPTName()
    {
        return actorPTName;
    }

    public string ReturnEnName()
    {
        return actonENName;
    }

    public Sprite ReturnPortrait(ActorEmotion emotion)
    {
        switch(emotion)
        {
            default:
            case ActorEmotion.neutral: return portraitNeutral;
            case ActorEmotion.happy: return portraitHappy;
            case ActorEmotion.sad: return portraitSad;
            case ActorEmotion.angry: return portraitAngry;
            case ActorEmotion.confused: return portraitConfused;
        }
    }
}
