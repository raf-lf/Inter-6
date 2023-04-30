using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spirit", menuName = "Scriptables/Spirit")]
public class Spirit : ScriptableObject
{
    public string spiritPTName;
    public string spiritENName;
    [TextArea (1,3)]
    public string spiritPTDescription;
    [TextArea (1,3)]
    public string spiritENDescription;
    public Sprite spiritIcon;
    public bool found;
    public Dialogue dialogue;


    public void FindSpirit()
    {
        found = true;
        GameManager.CanvasManager.DisplaySpiritMessage(this);
    }
}
