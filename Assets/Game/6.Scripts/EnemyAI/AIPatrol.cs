using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action__;
using DredgeAttack;

public class AIPatrol : MonoBehaviour
{
    Enemy entity;
    BehaviourState classState = BehaviourState.Patrol;
    enum PatrolBehaviour { Circular, Sequential }
    [SerializeField] PatrolBehaviour actualBehaviour;

    public Transform[] patrolWaypoints;
    public int waypointIndex;

    [SerializeField] private bool isMovingToNearestWaypoint;
    [SerializeField] private float moveSpeed;
    private int sign =  1;

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

            if (!isMovingToNearestWaypoint && entity.GetDredgeAttack() == DredgeAttackVariations.Hide)
            {
                StartCoroutine(GoToNearestWaypoint());
                return;
            }

            if (entity.GetActualState() == BehaviourState.Attack && entity.GetDredgeAttack() != DredgeAttackVariations.Noone || entity.isTeleporting)
                return;

            if (Vector3.Distance(entity.encounterPoint.position, GameManager.PlayerInstance.transform.position) >= entity.rangeDetection)
            {
                CheckNearestPatrolWaypoint();
                entity.ChangeState(classState);
            }
        }
    }

    public void CheckNearestPatrolWaypoint()
    {
        for (int i = 0; i < patrolWaypoints.Length; i++)
        {
            float[] dist = new float [patrolWaypoints.Length];
               dist[i] = Vector3.Distance(patrolWaypoints[i].transform.position, GameManager.PlayerInstance.transform.position);
            if (i == 0 || dist[i] < dist[i-1])
                waypointIndex = i;
        }
    }

    IEnumerator GoToNearestWaypoint()
    {
        isMovingToNearestWaypoint = true;
        CheckNearestPatrolWaypoint();
        while(Vector3.Distance(entity.transform.position, patrolWaypoints[waypointIndex].transform.position) > 0)
        {
            entity.transform.position =Vector3.MoveTowards(entity.transform.position, patrolWaypoints[waypointIndex].transform.position, moveSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        isMovingToNearestWaypoint = false;
        entity.SetDredgeAttack(DredgeAttackVariations.Noone);
        entity.SetAnimationBool("submerge", false);
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
        {
            waypointIndex = Mathf.Clamp(actualBehaviour == PatrolBehaviour.Sequential ? waypointIndex + sign : (waypointIndex + sign) % patrolWaypoints.Length, 0, patrolWaypoints.Length);
            if (waypointIndex == patrolWaypoints.Length || waypointIndex == 0)
                sign *= actualBehaviour == PatrolBehaviour.Sequential ? -1 : 1;
            //waypointIndex = ((waypointIndex + 1) % patrolWaypoints.Length);
        }
    }

    void ResetPatrol()
    {
        waypointIndex = 0;
    }

}
