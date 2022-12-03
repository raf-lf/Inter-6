using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ES_GiveItem : ES_EventBase
{
    [SerializeField] private int quantity = 1;
    [SerializeField] private InventoryItem itemToGive;
    public override void Play(EventSequence cine)
    {
        base.Play(cine);

        itemToGive.quantity += quantity;
        StartCoroutine(GameManager.Hud.GetItemSequence(itemToGive, quantity));
        GameManager.CanvasManager.DisplayItemMessage(itemToGive, quantity);

        if (GameManager.GameData.milkItem == itemToGive)
            GameManager.GameData.UpdateHpBoosts();

        StartCoroutine(NextEvent());
    }

}
