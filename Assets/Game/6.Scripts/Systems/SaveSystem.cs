using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SaveableDataType {eventSequence, item, shortcut, spiritLock, oneShot, lostCow}

public static class SaveSystem
{
    public static  Hashtable saveableHashtable = new Hashtable();

    public static void Save(SaveableDataType type, string objectName, bool value)
    {
        string key = GetSaveString(type, objectName);
        saveableHashtable.Add(key, value);
        Debug.Log("Saved key | " + key + " | with value | " + value);
    }

    public static bool Load(SaveableDataType type, string objectName)
    {
        string key = GetSaveString(type, objectName);

        if (!saveableHashtable.ContainsKey(key))
            return false;

        Debug.Log("Loaded key | " + key + " | with value | " + (bool)saveableHashtable[key]);
        return (bool)saveableHashtable[key];
    }

    public static string GetSaveString(SaveableDataType type, string objectName)
    {
        string saveString = SceneManager.GetActiveScene().name + "_";
        saveString += RetrieveTypeKey(type) + "_";
        saveString += objectName;
        return saveString;
    }

    public static string RetrieveTypeKey(SaveableDataType type)
    {
        switch (type)
        {
            default:
                return "???";
            case SaveableDataType.eventSequence:
                return "Seq";
            case SaveableDataType.item:
                return "Itm";
            case SaveableDataType.shortcut:
                return "Shc";
            case SaveableDataType.spiritLock:
                return "SpL";
            case SaveableDataType.lostCow:
                return "Cow";

        }
    }

}
