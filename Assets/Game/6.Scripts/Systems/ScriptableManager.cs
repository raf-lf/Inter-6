using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptableManager : MonoBehaviour
{
    public List<Flag> allFlags = new List<Flag>();
    public List<Task> allTasks = new List<Task>();

    public void ResetAll()
    {
        foreach (var item in allFlags)
        {
            item.flagActive = false;
        }

        foreach (var item in allTasks)
        {
            item.completed = false;
        }
    }
}
