using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Log", menuName = "Scriptables/Log")]
public class Log : ScriptableObject
{
    public LogTypes logType;
    [TextArea(1,3)]
    public string logText;

}


