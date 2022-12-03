using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ES_ShowSlide : ES_EventBase
{
    public Sprite imageToShow;

    public override void Play(EventSequence cine)
    {
        base.Play(cine);
        GameManager.CanvasManager.ShowSlideImage(imageToShow);
        StartCoroutine(NextEvent());
    }
}
