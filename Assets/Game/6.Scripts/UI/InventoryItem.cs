using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItem", menuName = "Scriptables/InventoryItem")]

public class InventoryItem : ScriptableObject
{
    public string itemName;
    [TextArea (2,5)]
    public string description;
    [TextArea(2, 5)]
    public string flavorText;
    public Sprite itemIcon;

    public int quantity;

}
