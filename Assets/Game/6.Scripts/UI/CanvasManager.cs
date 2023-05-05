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
    [SerializeField] private CanvasGroup slideCg;
    [SerializeField] private Image slideImage;

    [Header("Logs")] 
    [SerializeField] private CanvasGroup logGroup;
    [SerializeField] private TextMeshProUGUI logTitle;
    [SerializeField] private TextMeshProUGUI logText;
    [SerializeField] private LogStyles logStyles;
    [SerializeField] private Log spiritTutorialLog;
    [SerializeField] private float extraLogDuration;

    private void Awake()
    {
        GameManager.CanvasManager = this;

    }

    public void ShowHud(bool active)
    {
        hudCanvasGroup.DOFade(active ? 1 : 0, .33f).SetUpdate(true); ;
        
        if(FindObjectOfType<IslandManager>())
            IslandManager.CurrentIslandManager.canvasIslandManager.ShowHud(active);
    }

    public void AnimateOverlay(OverlayAnimation animation, float speed)
    {
        switch (animation)
        {
            case OverlayAnimation.Off:
                overlay.DOFade(0, speed).SetUpdate(true); ;
                break;
            case OverlayAnimation.Black:
                overlayImage.DOColor(Color.black, 0).SetUpdate(true); ;
                overlay.DOFade(1, speed).SetUpdate(true); ;
                overlayImage.DOColor(Color.black, speed).SetUpdate(true); ;
                break;
            case OverlayAnimation.White:
                overlayImage.DOColor(Color.white, 0).SetUpdate(true); ;
                overlay.DOFade(1, speed).SetUpdate(true);
                overlayImage.DOColor(Color.white, speed).SetUpdate(true); ;
                break;

        }
    }
    public void ShowSlideImage(Sprite image)
    {
        StopCoroutine(SlideSequence(image));
        StartCoroutine(SlideSequence(image));
    }

    private IEnumerator SlideSequence(Sprite image)
    {
        if (image != null)
        {
            slideCg.alpha = 0;
            slideImage.sprite = image;
            slideCg.DOFade(1, 1);
            yield return new WaitForSeconds(1);
        }
        else
        {
            slideCg.DOFade(0, 1);
            yield return new WaitForSeconds(1);
            slideImage.sprite = null;
        }
    }

    public void DisplayLog(Log log, float duration)
    {
        var logDuration = duration + extraLogDuration;
        logTitle.color = logStyles.ReturnLogInfo(log.logType).logColor;
        logTitle.text = log.logType.ToString();
        if(GameManager.gameLanguage == Language.portuguese) 
        {
            logText.text = log.ptLogText;
        }
        else 
        {
            logText.text = log.enLogtext;
        }
        StopCoroutine(ShowLogSequence(logDuration));
        StartCoroutine(ShowLogSequence(logDuration));
    }

    public void DisplayItemMessage(InventoryItem item, int quantity)
    {
        logTitle.color = logStyles.ReturnLogInfo(LogTypes.Item).logColor;
        logTitle.text = logStyles.ReturnLogInfo(LogTypes.Item).type.ToString();
        
        if(GameManager.gameLanguage == Language.portuguese) 
        {
            if(quantity == 1)
                logText.text = item.itemPTName + " encontrado";
            else
                logText.text = item.itemPTName +  quantity + " x"  + " encontrados";

        }
        else 
        {
            if (quantity == 1)
                logText.text = "found " + item.itemENName ;
            else
                logText.text = "found " + item.itemENName  + quantity + " x";


        }
        StopCoroutine(ShowLogSequence(2));
        StartCoroutine(ShowLogSequence(2));


    }
    
    public void DisplaySpiritMessage(Spirit spirit)
    {
        logTitle.color = logStyles.ReturnLogInfo(LogTypes.Spirit).logColor;
        logTitle.text = logStyles.ReturnLogInfo(LogTypes.Spirit).type.ToString();
        
        if(GameManager.gameLanguage == Language.portuguese) 
        {
            logText.text = "Espírito resgatado: " + spirit.spiritPTName;
        }
        else 
        {
            logText.text = "Spirit rescued: " + spirit.spiritENName;
        
        }

        
        StopCoroutine(ShowLogSequence(2));
        StartCoroutine(ShowLogSequence(2));

        if (!spiritTutorialLog.alreadyShown)
            StartCoroutine(SpiritTutorialSequence());
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
