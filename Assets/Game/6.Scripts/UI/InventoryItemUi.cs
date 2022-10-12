using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class InventoryItemUi : InventoryElement
{
    public InventoryItem associatedItem;
    public TextMeshProUGUI itemQty;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemDescription;
    //public TextMeshProUGUI itemFlavor;
    public Image itemIcon;
    [SerializeField] private RectTransform descriptionTransform;
    [SerializeField] private CanvasGroup descriptionCanvasGroup;
    private Tween tween;

    public void Setup(InventoryItem item)
    {
        associatedItem = item;
        itemQty.text = item.quantity.ToString();
        itemName.text = item.itemName;
        itemDescription.text = item.description;
        //itemFlavor.text = item.flavorText;
        itemIcon.sprite = item.itemIcon;

    }

    public void OpenDescription()
    {
        tween.Kill();
        Sequence seq = DOTween.Sequence();
        seq.Append(descriptionTransform.DOScaleX(1, .25f));
        //seq.Join(descriptionTransform.DOLocalMoveX(0, .5f));
        seq.Append(descriptionCanvasGroup.DOFade(1, .25f));
        tween = seq;
        tween.SetUpdate(true);
        tween.Play();
    }

    public void CloseDescription()
    {
        tween.Kill();
        Sequence seq = DOTween.Sequence();
        seq.Append(descriptionCanvasGroup.DOFade(0, .25f));
        seq.Append(descriptionTransform.DOScaleX(0, .25f));
        //seq.Join(descriptionTransform.DOLocalMoveX(-100, .5f));
        tween = seq;
        tween.SetUpdate(true);
        tween.Play();

    }
    
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        OpenDescription();
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        CloseDescription();
    }
}
