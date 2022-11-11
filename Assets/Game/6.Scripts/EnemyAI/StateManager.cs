using UnityEngine;
using Action__;
using System;

public class StateManager : MonoBehaviour
{
    public Action<ActionStatus, BehaviourState> StateChange;
}
