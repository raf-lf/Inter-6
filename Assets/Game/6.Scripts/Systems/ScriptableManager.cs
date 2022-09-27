using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableManager", menuName = "Scriptables/ScriptableManager")]
public class ScriptableManager : ScriptableObject
{
    public List<Flag> allFlags = new List<Flag>();
    public List<Task> allTasks = new List<Task>();

    public void PopulateLists()
    {
        if (allFlags.Count == 0)
            allFlags.AddRange(Resources.FindObjectsOfTypeAll<Flag>());
        if (allTasks.Count == 0)
            allTasks.AddRange(Resources.FindObjectsOfTypeAll<Task>());

    }
    public void ResetAll()
    {
        foreach (var item in allFlags)
        {
            item.flagActive = false;
        }

        foreach (var item in allTasks)
        {
            item.ChangeTaskState(TaskState.notStarted);
        }
    }
}
