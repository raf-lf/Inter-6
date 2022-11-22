using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action__;

public class AIPatrol : MonoBehaviour
{
    Enemy entity;
    ActionStatus status = ActionStatus.Running;
    BehaviourState classState = BehaviourState.Patrol;

    [SerializeField] private Transform[] patrolWaypoints;
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
            if (entity.GetActualState() == BehaviourState.Attack)
                return;

            if (Vector3.Distance(entity.encounterPoint.position, GameManager.PlayerInstance.transform.position) >= entity.rangeDetection)
            {
                entity.ChangeState(status, classState);
            }
        }
    }

    void Patrol()
    {
        if (patrolWaypoints.Length == 0)
            return;
        transform.position = Vector3.MoveTowards(entity.transform.position, patrolWaypoints[waypointIndex].transform.position, moveSpeed * Time.deltaTime);

        Vector3 dir = patrolWaypoints[waypointIndex].transform.position - entity.transform.position;
        Quaternion quaternion = Quaternion.LookRotation(dir, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, quaternion, 100*Time.deltaTime);

        if (Vector3.Distance(entity.transform.position, patrolWaypoints[waypointIndex].transform.position) == 0)
            waypointIndex = ((waypointIndex + 1) % patrolWaypoints.Length);
    }

    void ResetPatrol()
    {
        waypointIndex = 0;
    }

}
