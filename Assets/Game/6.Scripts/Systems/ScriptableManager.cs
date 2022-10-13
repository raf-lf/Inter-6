using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableManager", menuName = "Scriptables/ScriptableManager")]
public class ScriptableManager : ScriptableObject
{
    public List<InventoryItem> allItems = new List<InventoryItem>();
    public List<Spirit> allSpirits = new List<Spirit>();
    public List<Flag> allFlags = new List<Flag>();
    public List<Task> allTasks = new List<Task>();

    public void PopulateLists()
    {
        if (allItems.Count == 0)
            allItems.AddRange(Resources.FindObjectsOfTypeAll<InventoryItem>());
        if (allSpirits.Count == 0)
            allSpirits.AddRange(Resources.FindObjectsOfTypeAll<Spirit>());
        if (allFlags.Count == 0)
            allFlags.AddRange(Resources.FindObjectsOfTypeAll<Flag>());
        if (allTasks.Count == 0)
            allTasks.AddRange(Resources.FindObjectsOfTypeAll<Task>());

    }
    public void ResetAll()
    {
        foreach (var item in allItems)
        {
            item.quantity = 0;
        }
        foreach (var item in allSpirits)
        {
            item.found = false;
        }
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
