using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ES_ProgressQuestline : ES_EventBase
{
    public Questline questline;

    public override void Play(EventSequence cine)
    {
        base.Play(cine);

        questline.AdvanceStage();

        StartCoroutine(NextEvent());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
            questline.AdvanceStage();
    }
}
