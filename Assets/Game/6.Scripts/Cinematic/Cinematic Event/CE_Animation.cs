using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CE_Animation : CinematicEvent
{
    public enum AnimationType { Trigger, Boolean, Integer, Float }
    public AnimationType type;
    public Animator animator;
    public string parameter;
    public bool valueBool;
    public float valueNumeric;

    public override void Play(Cinematic cine)
    {
        base.Play(cine);
        switch (type)
        {
            case AnimationType.Trigger:
                animator.SetTrigger(parameter);
                break;
            case AnimationType.Boolean:
                animator.SetBool(parameter, valueBool);
                break;
            case AnimationType.Integer:
                animator.SetInteger(parameter, (int)valueNumeric);
                break;
            case AnimationType.Float:
                animator.SetFloat(parameter, valueNumeric);
                break;
        }
    }
}
