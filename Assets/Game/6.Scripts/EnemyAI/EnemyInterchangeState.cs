using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action__;
[CreateAssetMenu(fileName = "Enemy", menuName = "EnemyActions")]
public class EnemyInterchangeState : ScriptableObject
{
    [Header("State")]
    public BehaviourState NextState;
    public BehaviourState FailureState;

    [Header("Node")]
    public int nodeIndex;
    public BehaviourNodes[] NextNode;

    public BehaviourState GetNextState(ActionStatus stateStatus)
    {
        if (stateStatus == ActionStatus.Success)
            return NextState;
        else if (stateStatus == ActionStatus.Fail)
            return FailureState;
        else
            return FailureState;
    }

    public BehaviourNodes GetNextNode(ActionStatus nodeStatus)
    {
        BehaviourNodes _nextNode;
        if (nodeStatus == ActionStatus.Success)
        {
            _nextNode = NextNode[nodeIndex];
            nodeIndex++;
        }
        else
        {
            nodeIndex = 0;
            _nextNode = NextNode[nodeIndex];
        }

        return _nextNode;
    }

    public BehaviourNodes ResetNodesIndex()
    {
        nodeIndex = 0;
        return NextNode[nodeIndex];
    }
}