using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItem", menuName = "Scriptables/InventoryItem")]

public class InventoryItem : ScriptableObject
{
    public string itemName;
    public string description;
    public string flavorText;
    public Sprite itemIcon;

    public int quantity;

}
