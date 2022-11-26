using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using FMODUnity;

public class RestItemUi : InventoryElement
{
    private ModalRest restManager;
    [SerializeField] private bool noItemOption;
    [SerializeField] private InventoryItem inventoryItem;

    private void Awake()
    {
        restManager = GetComponentInParent<ModalRest>();
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);

        if(noItemOption)
            restManager.UpdateDescription(null, true);

        restManager.UpdateDescription(inventoryItem, false);
        RuntimeManager.PlayOneShot("event:/UI/hover");
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);

        restManager.UpdateDescription(null, false);
    }
}
