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

    public List <Transform> patrolWaypoints;
    public int waypointIndex;

    [SerializeField] private bool isMovingToNearestWaypoint;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float speedDistanceMultiplier;
    private float actualSpeed;
    [SerializeField] private float rotationSpeed;
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

            if (!isMovingToNearestWaypoint && entity.GetDredgeAttack() == DredgeAttackVariations.Hide && !entity.isTeleporting)
            {
                StartCoroutine(GoToNearestWaypoint());
                return;
            }

            if (entity.GetActualState() == BehaviourState.Attack && entity.GetDredgeAttack() != DredgeAttackVariations.Noone || entity.isTeleporting)
                return;

            if (Vector3.Distance(entity.transform.position, GameManager.PlayerInstance.transform.position) >= entity.rangeDetection && entity.GetDredgeAttack() == DredgeAttackVariations.Noone)
            {
                CheckNearestPatrolWaypoint();
                entity.ChangeState(classState);
            }
        }
    }

    public void CheckNearestPatrolWaypoint()
    {
        int distBufferIndex = 0;
        float[] dist = new float [patrolWaypoints.Count];
        for (int i = 0; i < patrolWaypoints.Count; i++)
        {
               dist[i] = Vector3.Distance(patrolWaypoints[i].transform.position, GameManager.PlayerInstance.transform.position);
        }

        for (int i = 0; i < patrolWaypoints.Count; i++)
        {
            if (i != 0 && dist[i] < dist[distBufferIndex])
                distBufferIndex = i;
        }
        waypointIndex = distBufferIndex;
    }

    IEnumerator GoToNearestWaypoint()
    {
        isMovingToNearestWaypoint = true;
        CheckNearestPatrolWaypoint();
        while(Vector3.Distance(entity.transform.position, patrolWaypoints[waypointIndex].transform.position) != 0)
        {
            UpdateSpeed();
            entity.transform.position = Vector3.MoveTowards(entity.transform.position, patrolWaypoints[waypointIndex].transform.position, actualSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        isMovingToNearestWaypoint = false;
        entity.SetDredgeAttack(DredgeAttackVariations.Noone);
        entity.SetAnimationBool("submerge", false);
        entity.attackSequenceCount = 1;
    }

    void UpdateSpeed()
    {
        float dist = Vector3.Distance(patrolWaypoints[waypointIndex].transform.position, GameManager.PlayerInstance.transform.position);
        actualSpeed = dist >= entity.rangeDetection * 4f ? moveSpeed * speedDistanceMultiplier : moveSpeed;
    }

    void Patrol()
    {
        if (patrolWaypoints.Count == 0)
            return;
        entity.SetAnimationBool("alert", false);
        entity.SetAnimationBool("isObserving", false);
        UpdateSpeed();
        transform.position = Vector3.MoveTowards(entity.transform.position, patrolWaypoints[waypointIndex].transform.position, actualSpeed * Time.deltaTime);

        Vector3 dir = patrolWaypoints[waypointIndex].transform.position - entity.transform.position;
        Quaternion quaternion = Quaternion.LookRotation(dir, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, quaternion, rotationSpeed*Time.deltaTime);

        if (Vector3.Distance(entity.transform.position, patrolWaypoints[waypointIndex].transform.position) == 0)
        {
            waypointIndex = Mathf.Clamp(actualBehaviour == PatrolBehaviour.Sequential ? waypointIndex + sign : (waypointIndex + sign) % patrolWaypoints.Count, 0, patrolWaypoints.Count -1);
            if (waypointIndex == patrolWaypoints.Count - 1 || waypointIndex == 0)
                sign *= actualBehaviour == PatrolBehaviour.Sequential ? -1 : 1;
        }
    }

    void ResetPatrol()
    {
        waypointIndex = 0;
    }

}
