using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Cheats : MonoBehaviour
{
    [SerializeField] KeyCode cheatKey;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] Toggle toggleInvulnerability;
    [SerializeField] Toggle toggleEnergy;
    [SerializeField] Button buttonClose;
    private bool isOpen;

    public static bool cheatInvulnerability;
    public static bool cheatInfiniteEnergy;

    private void Start()
    {
        toggleInvulnerability.onValueChanged.AddListener(delegate { ToggleInvulnerability(); });
        toggleEnergy.onValueChanged.AddListener(delegate { ToggleInfiniteEnergy(); });
        buttonClose.onClick.AddListener(OpenClose);
    }
    public void ToggleInvulnerability()
    {
        cheatInvulnerability = toggleInvulnerability.isOn;
    }
    
    public void ToggleInfiniteEnergy()
    {
        cheatInfiniteEnergy = toggleEnergy.isOn;
    }

    public void OpenClose()
    {
        isOpen = !isOpen;
        Cursor.visible = isOpen;
        canvasGroup.DOFade(isOpen ? 1 : 0, .5f);
        canvasGroup.interactable = isOpen;
        canvasGroup.blocksRaycasts = isOpen;

    }

    private void Update()
    {
        if(Input.GetKeyDown(cheatKey))
        {
            OpenClose();
        }

    }
}
