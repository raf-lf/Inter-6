using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SaveableDataType {eventSequence}

public static class SaveSystem
{
    public static  Hashtable eventSequenceHash = new Hashtable();

    public static void Save(SaveableDataType type, string objectName, bool value)
    {
        string key = GetSaveString(type, objectName);
        eventSequenceHash.Add(key, value);
        Debug.Log("Saved key | " + key + " | with value | " + value);
    }

    public static bool Load(SaveableDataType type, string objectName)
    {
        string key = GetSaveString(type, objectName);

        if (!eventSequenceHash.ContainsKey(key))
            return false;

        Debug.Log("Loaded key | " + key + " | with value | " + (bool)eventSequenceHash[key]);
        return (bool)eventSequenceHash[key];
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
                return "unknown";
            case SaveableDataType.eventSequence:
                return "ES";
        }
    }

}
