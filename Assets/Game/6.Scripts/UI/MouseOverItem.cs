using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseOverItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RestManager restManager;
    [SerializeField] private bool noItemOption;
    [SerializeField] private InventoryItem inventoryItem;

    private void Awake()
    {
        restManager = GetComponentInParent<RestManager>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(noItemOption)
            restManager.UpdateDescription(null, true);

        restManager.UpdateDescription(inventoryItem, false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        restManager.UpdateDescription(null, false);
    }
}
