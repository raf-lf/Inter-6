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
    [Header ("Scripts")]
    [SerializeField] public Inventory inventory;

    [Header("Overlay")]
    [SerializeField] private CanvasGroup overlay;
    [SerializeField] private Image overlayImage;

    private void Awake()
    {
        GameManager.CanvasManager = this;

    }

    public void AnimateOverlay(OverlayAnimation animation, float speed)
    {
        switch (animation)
        {
            case OverlayAnimation.Off:
                overlay.DOFade(0, speed);
                break;
            case OverlayAnimation.Black:
                overlay.DOFade(1, speed);
                overlayImage.DOColor(Color.black, speed);
                break;
            case OverlayAnimation.White:
                overlay.DOFade(1, speed);
                overlayImage.DOColor(Color.white, speed);
                break;

        }
    }
}
