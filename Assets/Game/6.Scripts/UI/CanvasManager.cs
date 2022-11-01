using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;

public enum OverlayAnimation
{
    Off, Black, White
}

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup hudCanvasGroup;

    [Header ("Scripts")]
    [SerializeField] public Inventory inventory;

    [Header("Overlay")]
    [SerializeField] private CanvasGroup overlay;
    [SerializeField] private Image overlayImage;

    [Header("Logs")] 
    [SerializeField] private CanvasGroup logGroup;
    [SerializeField] private TextMeshProUGUI logTitle;
    [SerializeField] private TextMeshProUGUI logText;
    [SerializeField] private LogStyles logStyles;
    [SerializeField] private Log spiritTutorialLog;

    private void Awake()
    {
        GameManager.CanvasManager = this;

    }

    public void ShowHud(bool active)
    {
        hudCanvasGroup.DOFade(active ? 1 : 0, .33f);
        
        if(FindObjectOfType<IslandManager>())
            IslandManager.CurrentIslandManager.canvasIslandManager.ShowHud(!active);
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

    public void DisplayLog(Log log, float duration)
    {
        logTitle.color = logStyles.ReturnLogInfo(log.logType).logColor;
        logTitle.text = log.logType.ToString();
        logText.text = log.logText;
        StopCoroutine(ShowLogSequence(duration));
        StartCoroutine(ShowLogSequence(duration));
    }

    public void DisplayItemMessage(InventoryItem item, int quantity)
    {
        logTitle.color = logStyles.ReturnLogInfo(LogTypes.Item).logColor;
        logTitle.text = logStyles.ReturnLogInfo(LogTypes.Item).type.ToString();
        
        if(quantity == 1)
            logText.text = item.itemName + " encontrado";
        else
            logText.text = item.itemName + " x" + quantity + " encontrados";

        
        StopCoroutine(ShowLogSequence(2));
        StartCoroutine(ShowLogSequence(2));

        if (!spiritTutorialLog.alreadyShown)
            StartCoroutine(SpiritTutorialSequence());

    }
    
    public void DisplaySpiritMessage(Spirit spirit)
    {
        logTitle.color = logStyles.ReturnLogInfo(LogTypes.Spirit).logColor;
        logTitle.text = logStyles.ReturnLogInfo(LogTypes.Spirit).type.ToString();
        
        logText.text = "Espírito resgatado: " + spirit.spiritName;

        
        StopCoroutine(ShowLogSequence(2));
        StartCoroutine(ShowLogSequence(2));
    }

    public IEnumerator ShowLogSequence(float duration)
    {
        logGroup.DOFade(0,0);
        yield return new WaitForEndOfFrame();
        logGroup.DOFade(1, .5f);
        yield return new WaitForSeconds(duration);
        logGroup.DOFade(0, .5f);
    }

    private IEnumerator SpiritTutorialSequence()
    {
        yield return new WaitForSeconds(3);
        spiritTutorialLog.PlayLog();

    }
}
