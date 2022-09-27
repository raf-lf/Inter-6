using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ES_ChangeTask : ES_EventBase
{
    [SerializeField] private Task taskToChange;
    [SerializeField] private TaskState stateToSet;

    public override void Play(EventSequence cine)
    {
        base.Play(cine);

        taskToChange.ChangeTaskState(stateToSet);

        switch (stateToSet)
        {
            case TaskState.acquired:
                if(taskToChange.taskState != TaskState.acquired)
                    GameManager.TaskManager.CreateTask(taskToChange);
                break;
            case TaskState.completed:
                if (taskToChange.taskState != TaskState.completed)
                    GameManager.TaskManager.CompleteTask(taskToChange);
                break;
        }
        GameManager.TaskManager.UpdateTaskList();

        StartCoroutine(NextEvent());
    }
}
