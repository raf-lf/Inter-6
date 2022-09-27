using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TaskType { main, side }
public enum TaskState {notStarted, acquired, completed}

[CreateAssetMenu(fileName = "Task", menuName = "Scriptables/Task")]
public class Task : ScriptableObject
{
    public TaskState taskState;
    public TaskType taskType;
    [TextArea(1,1)]
    public string taskDescription;
    public Sprite taskIcon;
    [SerializeField] private Flag[] flagsOnCompletion;

    public void ChangeTaskState(TaskState changingState)
    {
        taskState = changingState;

        switch (changingState)
        {
            case TaskState.completed:
                CompleteTask();
                break;
            
        }

        GameManager.TaskManager.ShowHideTaskList();
    }
    public void CompleteTask()
    {
        foreach (Flag flag in flagsOnCompletion)
        {
            flag.ActivateFlag();
        }
    }
}
