using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public enum IslandModalType { exit, rest, investigation}
public class CanvasIslandManager : MonoBehaviour
{
    public CanvasGroup modalOverlayCanvasGroup;
    public IslandModal modalExit;
    public IslandModal modalRest;
    public IslandModal modalInvestigation;

    public CanvasGroup auxiliaryCV;
    public Button buttonAuxiliaryExit;
    public Button buttonAuxiliaryRest;

    [HideInInspector] public ClickableOpenModal clickableAnchor;
    [HideInInspector] public ClickableOpenModal clickableRestPoint;
    
    [SerializeField] private CanvasGroup canvasGroup;

    private void Start()
    {
        buttonAuxiliaryExit.onClick.AddListener(ButtonExit);
        buttonAuxiliaryRest.onClick.AddListener(ButtonRest);
        UpdateAuxiliaryButtons();

    }

    public void ShowHud(bool active)
    {
        canvasGroup.DOFade(active ? 1 : 0, .33f);
    }
    
    public void UpdateAuxiliaryButtons()
    {

        auxiliaryCV.DOFade(GameManager.PlayerControl ? 1 : 0 ,.5f);
        auxiliaryCV.interactable = GameManager.PlayerControl ? true : false;

        buttonAuxiliaryExit.interactable = modalExit.modalVisible ? false : true;
        buttonAuxiliaryRest.interactable = modalRest.modalVisible ? false : true;

    }

    public void ButtonExit()
    {
        if (modalRest.modalVisible)
            modalRest.Cancel();
        clickableAnchor.Click();
        UpdateAuxiliaryButtons();
    }

    public void ButtonRest()
    {
        if (modalExit.modalVisible)
            modalExit.Cancel();
        clickableRestPoint.Click();
        UpdateAuxiliaryButtons();
    }

    private void ActivateModalOverlay(bool active)
    {
        modalOverlayCanvasGroup.blocksRaycasts = active;
        modalOverlayCanvasGroup.DOFade(active ? 1 : 0 , .5f);
    }

    public void ToggleBlockingOverlay(bool active)
    {
        ActivateModalOverlay(active);
    }

    public void OpenCloseModal(bool open, IslandModalType modal)
    {
        ReturnModal(modal).OpenCloseModal(open);
        UpdateAuxiliaryButtons();
    }

    private IslandModal ReturnModal(IslandModalType modal)
    {
        switch(modal)
        {
            default: return null;
            case IslandModalType.exit: return modalExit;
            case IslandModalType.rest: return modalRest;
            case IslandModalType.investigation: return modalInvestigation; 
        }
    }

}
