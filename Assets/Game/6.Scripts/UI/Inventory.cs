using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Inventory : MonoBehaviour
{
    [Header("Inventory")]
    [SerializeField] private Transform inventoryParent;
    [SerializeField] private InventoryItemUi inventoryItemUi;
    [SerializeField] private Transform spiritParent;
    [SerializeField] private SpiritUi spiritUi;
    [SerializeField] private bool inventoryOpen;
    [SerializeField] private CanvasGroup canvasGroup;

    public void UpdateInventory()
    {
        foreach (var item in inventoryParent.GetComponentsInChildren<InventoryItemUi>())
        {
            Destroy(item.gameObject);
        }

        foreach (var item in GameManager.Instance.scriptableManger.allItems)
        {
            var newUiItem = Instantiate(inventoryItemUi, inventoryParent);
            newUiItem.Setup(item);
        }

        foreach (var item in spiritParent.GetComponentsInChildren<SpiritUi>())
        {
            Destroy(item.gameObject);
        }

        foreach (var item in GameManager.Instance.scriptableManger.allSpirits)
        {
            var newUiItem = Instantiate(spiritUi, spiritParent);
            newUiItem.Setup(item);
        }
    }
    public void InventoryOpenClose(bool open)
    {
        Time.timeScale = open ? 0 : 1;

        inventoryOpen = open;

        if(open)
            UpdateInventory();

        canvasGroup.DOFade(open ? 1 : 0, .5f).SetUpdate(true);

        GameManager.CameraManager.ChangeOverlayEffect( open ? OverlayEffectType.Pause : OverlayEffectType.None);
    }

    private void Update()
    {
        if (!GameManager.PlayerControl)
            return;

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            InventoryOpenClose(inventoryOpen ? false : true); 
        }
    }
}
