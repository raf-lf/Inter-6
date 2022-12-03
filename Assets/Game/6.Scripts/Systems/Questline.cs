using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Questline", menuName = "Scriptables/Questline")]
public class Questline : ScriptableObject
{
    public int currentIndex = -1;
    public QuestlineStage[] stages = new QuestlineStage[0];

    public void ResetQuestline()
    {
        UpdateStage(-1);
    }

    public void AdvanceStage()
    {
        UpdateStage(currentIndex + 1);

    }
    public void UpdateStage(int index)
    {
        currentIndex = index;
        if (currentIndex >= stages.Length)
        {
            for (int i = 0; i < stages.Length; i++)
            {
                if (i == stages.Length-1)
                    stages[i].stageTask.ChangeTaskState(TaskState.completed);
                else
                    stages[i].stageTask.ChangeTaskState(TaskState.hidden);
            }
        }
        else
        {
            for (int i = 0; i < stages.Length; i++)
            {
                if (i < currentIndex)
                {
                    stages[i].stageTask.ChangeTaskState(TaskState.hidden);
                }
                else if (i == currentIndex)
                {
                    if (currentIndex > stages.Length)
                        stages[i].stageTask.ChangeTaskState(TaskState.completed);
                    else
                        stages[i].stageTask.ChangeTaskState(TaskState.acquired);
                }
            }
        }

        for (int i = 0; i < stages.Length; i++)
        {
            if (currentIndex > i)
                stages[i].stageFlag.ActivateFlag();
            else
                stages[i].stageFlag.flagActive = false;
        }

    }

}

[Serializable]
public class QuestlineStage
{
    public Flag stageFlag;
    public Task stageTask;
}

