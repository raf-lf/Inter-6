using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public enum OverlayAnimation
{
    Off, Black, White
}

public class CanvasManager : MonoBehaviour
{
    public Image Overlay;

    private void Awake()
    {
        GameManager.CanvasManager = this;

    }

    public void AnimateOverlay(OverlayAnimation animation, float speed)
    {
        switch (animation)
        {
            case OverlayAnimation.Off:
                Overlay.DOFade(0, speed);
                break;
            case OverlayAnimation.Black:
                Overlay.DOFade(1, speed);
                Overlay.DOColor(Color.black, speed);
                break;
            case OverlayAnimation.White:
                Overlay.DOFade(1, speed);
                Overlay.DOColor(Color.white, speed);
                break;

        }
    }
}
