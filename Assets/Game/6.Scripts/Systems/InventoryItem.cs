using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryItem", menuName = "Scriptables/InventoryItem")]

public class InventoryItem : ScriptableObject
{
    public string itemPTName;
    [TextArea (2,5)]
    public string ptDescription;
    [TextArea(2, 5)]
    public string ptFlavorText;
    public Sprite itemIcon;

    public int quantity;

    public string itemENName;
    [TextArea(2, 5)]
    public string itemENDescription;
    [TextArea(2, 5)]
    public string itemENFlavorText;

}
