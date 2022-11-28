using UnityEngine;
using Action__;

public abstract class BehaviourManager : MonoBehaviour
{
    protected StateManager stateManager;
    //public delegate void ChangeState();
    //public static event ChangeState StateChange;


    protected virtual void Start()
    {
        stateManager.StateChange += ActionToExecute;
    }    

    protected virtual void OnDestroy()
    {
        stateManager.StateChange -= ActionToExecute;
    }

    public virtual void ActionToExecute(BehaviourState state)
    {

    }

}