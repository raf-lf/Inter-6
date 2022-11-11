using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action__;

public class AIBanish : MonoBehaviour, IEnemy
{
    Enemy entity;
    ActionStatus status = ActionStatus.Running;
    BehaviourState classState = BehaviourState.Banished;
    private LanternTarget lanternTarget;
    // Start is called before the first frame update
    void Start()
    {
        entity = GetComponent<Enemy>();
        lanternTarget = GetComponent<LanternTarget>();
        entity.EnemyActions += AIActionExecuting;
    }

    void OnDestroy()
    {
        entity.EnemyActions -= AIActionExecuting;
    }

    public void AIActionExecuting(BehaviourState state)
    {
        if (state == classState)
        {

        }
        else
        {
            if (entity.canBanish && !entity.isBanished)
            {
                entity.ChangeState(status, classState);
            }
        }
    }

}
