using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TaskManager : MonoBehaviour
{
    [SerializeField] private RectTransform taskParent;
    [SerializeField] private TaskUi taskPrefab;
    private CanvasGroup cv;
    [SerializeField] private List<TaskUi> taskUiElements = new List<TaskUi>();

    private void Awake()
    {
        GameManager.TaskManager = this;
        cv = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        PopulateTaskList();
    }

    //Used when the scene begins
    private void PopulateTaskList()
    {
        foreach (var item in GameManager.Instance.scriptableManger.allTasks)
        {
            if (item.taskState == TaskState.acquired || item.taskState == TaskState.completed)
            {
                CreateTask(item);
            }
        }

        ShowHideTaskList();
    }

    public void ShowHideTaskList()
    {
        bool hasActiveTask = false;

        foreach (var item in GameManager.Instance.scriptableManger.allTasks)
        {
            if (item.taskState == TaskState.acquired || item.taskState == TaskState.completed)
            { 
                hasActiveTask = true;
                break;
            }
        }
        
        cv.DOFade(hasActiveTask ? 1 : 0, .5f);
    }

    //Used when a new task appears
    public void CreateTask(Task creatingTask)
    {
        var newUiTask = Instantiate(taskPrefab, taskParent);
        taskUiElements.Add(newUiTask);
        newUiTask.Setup(creatingTask);
        ShowHideTaskList();

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
        ShowHideTaskList();

    }

    public void UpdateTaskList()
    {
        foreach (var item in taskUiElements)
        {
            Destroy(item.gameObject);
        }
        taskUiElements.Clear();

        foreach (var item in GameManager.Instance.scriptableManger.allTasks)
        {
            CreateTask(item);
        }
        ShowHideTaskList();
    }
}
