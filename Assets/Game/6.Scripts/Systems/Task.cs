using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TaskType { main, side }
public enum TaskState {hidden, acquired, completed}

[CreateAssetMenu(fileName = "Task", menuName = "Scriptables/Task")]
public class Task : ScriptableObject
{
    public TaskState taskState;
    public TaskType taskType;
    [TextArea(1,1)]
    public string taskPTDescription;
    [TextArea(1,1)]
    public string taskEnDescription;
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

        if(GameManager.TaskManager)
            GameManager.TaskManager.UpdateTaskList();
    }
    public void CompleteTask()
    {
        foreach (Flag flag in flagsOnCompletion)
        {
            flag.ActivateFlag();
        }
    }
}
