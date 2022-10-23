using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IslandModal : MonoBehaviour
{
    public IslandModalType associatedModal;
    public CanvasGroup canvasGroup;
    public bool modalVisible;
    
    public void OpenCloseModal(bool open)
    {
        modalVisible = open;
        canvasGroup.DOFade(open ? 1 : 0, .5f);
        canvasGroup.interactable = open ? true : false;
        canvasGroup.blocksRaycasts = open ? true : false;

        if (open)
            ModalOpened();
        else
            ModalClosed();

    }

    public void Cancel()
    {
        IslandManager.CurrentIslandManager.canvasIslandManager.OpenCloseModal(false, associatedModal);
        GameManager.CameraManager.ReturnCamera();
    }

    public virtual void ModalOpened()
    {

    }
    public virtual void ModalClosed()
    {
    }
}
