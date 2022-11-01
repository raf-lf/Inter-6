using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LogTypes
{
    Item,
    Spirit,
    Info,
    Tutorial,
    Hint
}

[CreateAssetMenu(fileName = "LogStyles", menuName = "Scriptables/Design/LogStyles")]
public class LogStyles : ScriptableObject
{
    public LogType[] logTypes = Array.Empty<LogType>();
    
    public LogType ReturnLogInfo(LogTypes type)
    {
        switch (type)
        {
            case LogTypes.Item:
                return logTypes[0];
            case LogTypes.Spirit:
                return logTypes[1];
            case LogTypes.Info:
                return logTypes[2];
            case LogTypes.Tutorial:
                return logTypes[3];
            default:
            case LogTypes.Hint:
                return logTypes[4];
;        }
    }
}

[Serializable]
public class LogType
{
    public LogTypes type;
    public Color logColor;
    
}


