using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class SpiritUi : InventoryElement
{
    public Spirit associatedSpirit;
    public TextMeshProUGUI spiritName;
    public TextMeshProUGUI spiritDescription;
    public Image itemIcon;
    [SerializeField] private RectTransform descriptionTransform;
    [SerializeField] private CanvasGroup descriptionCanvasGroup;
    [SerializeField] private EventSequence eventSequence;
    [SerializeField] private ES_Dialogue dialogueEvent;
    [SerializeField] private ContentSizeFitter contentSizeFitterUpdate;
    private Tween tween;

    public void Setup(Spirit spirit)
    {
        associatedSpirit = spirit;
        spiritName.text = spirit.spiritName;
        spiritDescription.text = spirit.spiritDescription;
        itemIcon.sprite = spirit.spiritIcon;
        itemIcon.enabled = spirit.found ? true : false;
        
        if(associatedSpirit.dialogue != null)
            dialogueEvent.dialogue = associatedSpirit.dialogue;

        contentSizeFitterUpdate.verticalFit = ContentSizeFitter.FitMode.MinSize;
    }

    public void OpenDescription()
    {
        if (!associatedSpirit.found)
            return;

        tween.Kill();
        Sequence seq = DOTween.Sequence();
        seq.Append(descriptionTransform.DOScaleY(1, .25f));
        //seq.Join(descriptionTransform.DOLocalMoveX(0, .5f));
        seq.Append(descriptionCanvasGroup.DOFade(1, .25f));
        tween = seq;
        tween.SetUpdate(true);
        tween.Play();
    }

    public void CloseDescription()
    {
        if (!associatedSpirit.found)
            return;

        tween.Kill();
        Sequence seq = DOTween.Sequence();
        seq.Append(descriptionCanvasGroup.DOFade(0, .25f));
        seq.Append(descriptionTransform.DOScaleY(0, .25f));
        //seq.Join(descriptionTransform.DOLocalMoveX(-100, .5f));
        tween = seq;
        tween.SetUpdate(true);
        tween.Play();

    }
    public void Click()
    {
        if (!GameManager.PlayerClickControl || !associatedSpirit.found)
            return;

        CloseDescription();

        if (GameManager.CanvasManager.inventory.inventoryOpen)
            GameManager.CanvasManager.inventory.InventoryOpenClose(false);

            eventSequence.StartCinematic();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if (!GameManager.PlayerClickControl)
            return;
        
        base.OnPointerEnter(eventData);
        OpenDescription();
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if (!GameManager.PlayerClickControl)
            return;
        base.OnPointerExit(eventData);
        CloseDescription();
    }

}
