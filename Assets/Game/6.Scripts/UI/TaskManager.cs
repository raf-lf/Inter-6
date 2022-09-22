using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [SerializeField] private RectTransform taskParent;
    [SerializeField] private TaskUi taskPrefab;
    private List<TaskUi> taskUiElements = new List<TaskUi>();
    [SerializeField] static public List<Task> activeTasks = new List<Task>();

    private void Start()
    {
        PopulateTaskList();
    }

    //Used when the scene begins
    private void PopulateTaskList()
    {
        foreach (var item in activeTasks)
        {
            CreateTask(item);
        }
    }

    //Used when a new task appears
    public void CreateTask(Task creatingTask)
    {
        var newTask = Instantiate(taskPrefab, taskParent);
        newTask.Setup(creatingTask);
        taskUiElements.Add(newTask);

    }

    public void CompleteTask(Task completingTask)
    {
        completingTask.CompleteTask();

        foreach (var item in taskUiElements)
        {
            if (item.associatedTask == completingTask)
            {
                item.CompleteTask();
                break;
            }

        }

    }
}
