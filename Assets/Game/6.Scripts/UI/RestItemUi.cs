using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RestItemUi : InventoryElement
{
    private RestManager restManager;
    [SerializeField] private bool noItemOption;
    [SerializeField] private InventoryItem inventoryItem;

    private void Awake()
    {
        restManager = GetComponentInParent<RestManager>();
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);

        if(noItemOption)
            restManager.UpdateDescription(null, true);

        restManager.UpdateDescription(inventoryItem, false);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);

        restManager.UpdateDescription(null, false);
    }
}
