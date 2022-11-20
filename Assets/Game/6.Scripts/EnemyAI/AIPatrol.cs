using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action__;

public class AIPatrol : MonoBehaviour
{
    Enemy entity;
    ActionStatus status = ActionStatus.Running;
    BehaviourState classState = BehaviourState.Patrol;

    private Transform[] patrolWaypoints;
    private int waypointIndex;

    [SerializeField] private float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        entity = GetComponent<Enemy>();
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
            Patrol();
        }
        else
        {
            if (entity.isTakingDamage)
                return;

            if (Vector3.Distance(entity.encounterPoint.position, GameManager.PlayerInstance.transform.position) >= entity.rangeDetection)
            {
                ResetPatrol();
                entity.ChangeState(status, classState);
            }
        }
    }

    void Patrol()
    {
        transform.position = Vector3.MoveTowards(entity.transform.position, patrolWaypoints[waypointIndex].transform.position, moveSpeed * Time.deltaTime);
        waypointIndex = waypointIndex + 1 % patrolWaypoints.Length;
    }

    void ResetPatrol()
    {
        waypointIndex = 0;
    }

}
