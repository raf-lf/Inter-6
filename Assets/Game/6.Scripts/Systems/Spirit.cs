using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spirit", menuName = "Scriptables/Spirit")]
public class Spirit : ScriptableObject
{
    public string spiritName;
    [TextArea (1,3)]
    public string spiritDescription;
    public Sprite spiritIcon;
    public bool found;
    public Dialogue dialogue;


}
