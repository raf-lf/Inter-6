using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TaskType { main, side }

[CreateAssetMenu(fileName = "Task", menuName = "Scriptables/Task")]
public class Task : ScriptableObject
{
    public bool completed;
    [TextArea(1,1)]
    public string taskDescription;
    public Sprite taskIcon;
    public TaskType taskType;
    [SerializeField] private Flag[] flagsOnCompletion;

    public void CompleteTask()
    {
        foreach (Flag flag in flagsOnCompletion)
        {
            flag.ActivateFlag();
        }
    }
}
