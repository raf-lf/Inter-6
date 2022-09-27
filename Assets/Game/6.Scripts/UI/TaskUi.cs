using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskUi : MonoBehaviour
{

    public Task associatedTask;
    [SerializeField] private Image taskIcon;
    [SerializeField] private TextMeshProUGUI taskDescription;

    [SerializeField] private Sprite iconComplete;
    [SerializeField] private Color colorMain;
    [SerializeField] private Color colorSide;
    [SerializeField] private Color colorComplete;

    public void Setup(Task creatingTask)
    {
        associatedTask = creatingTask;
        taskDescription.text = creatingTask.taskDescription;

        if (associatedTask.taskState == TaskState.completed)
        {
            CompleteTask();
            return;
        }

        switch (creatingTask.taskType)
        {
            case TaskType.main:
                taskDescription.color = colorMain;
                break;

            case TaskType.side:
                taskDescription.color = colorSide;
                break;
        }

        taskIcon.sprite = creatingTask.taskIcon;


    }
    public void CompleteTask()
    {
        taskDescription.fontStyle = FontStyles.Strikethrough;
        taskDescription.color = colorComplete;
        taskIcon.sprite = iconComplete;

    }
}
