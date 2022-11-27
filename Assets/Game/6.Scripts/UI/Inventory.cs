using System;
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
    public bool inventoryOpen;
    [SerializeField] private CanvasGroup canvasGroup;

    private CanvasIslandManager canvasIslandManager;

    private void Awake()
    {
        canvasIslandManager = FindObjectOfType<CanvasIslandManager>();
    }

    public void UpdateInventory()
    {
        foreach (var item in inventoryParent.GetComponentsInChildren<InventoryItemUi>())
        {
            Destroy(item.gameObject);
        }

        foreach (var item in GameManager.ScriptableManager.allItems)
        {
            var newUiItem = Instantiate(inventoryItemUi, inventoryParent);
            newUiItem.Setup(item);
        }

        foreach (var item in spiritParent.GetComponentsInChildren<SpiritUi>())
        {
            Destroy(item.gameObject);
        }

        foreach (var item in GameManager.ScriptableManager.allSpirits)
        {
            var newUiItem = Instantiate(spiritUi, spiritParent);
            newUiItem.Setup(item);
        }
    }
    public void InventoryOpenClose(bool open)
    {
        if(open)
            Cursor.visible = open;
        else
            GameManager.GameplayManager.ConfigureCursor();

        if (canvasIslandManager)
            canvasIslandManager.ShowHud(!open);
        
        Time.timeScale = open ? 0 : 1;

        inventoryOpen = open;

        if(open)
            UpdateInventory();

        canvasGroup.DOFade(open ? 1 : 0, .5f).SetUpdate(true);
        canvasGroup.blocksRaycasts = open;

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
