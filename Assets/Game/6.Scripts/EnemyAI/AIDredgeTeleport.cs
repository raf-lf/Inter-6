using Action__;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDredgeTeleport : MonoBehaviour, IEnemy
{
    Enemy entity;
    AIPatrol patrol;

    [SerializeField] private Transform[] dredgeEncounterPoints;
    [SerializeField] private int actualEncounterIndex;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float actualTeleportTime;
    [SerializeField] private float teleportTimer;

    BehaviourState actualState = BehaviourState.Teleport;

    void Awake()
    {
        entity = GetComponent<Enemy>();
        entity.encounterPoint = dredgeEncounterPoints[actualEncounterIndex];
    }
    void Start()
    {
        patrol = GetComponent<AIPatrol>();
        patrol.patrolWaypoints = dredgeEncounterPoints[actualEncounterIndex].GetComponentsInChildren<Transform>();
        entity.EnemyActions += AIActionExecuting;
    }

    void OnDestroy()
    {
        entity.EnemyActions -= AIActionExecuting;
    }

    public void AIActionExecuting(BehaviourState state)
    {
        if(state == actualState)
        {
            if (!entity.isTeleporting)
                StartCoroutine(Teleport());
        }
        else
        {
            CheckNeededTeleport();
        }
    }

    void CheckNeededTeleport()
    {   
        if(Vector3.Distance(entity.encounterPoint.transform.position, GameManager.PlayerInstance.transform.position) > entity.rangeLeash)
        {
            for (int i = 0; i < dredgeEncounterPoints.Length; i++)
            {
                if (Vector3.Distance(dredgeEncounterPoints[i].transform.position, GameManager.PlayerInstance.transform.position) < entity.rangeLeash)
                {
                    actualEncounterIndex = i;
                    entity.ChangeState(actualState);
                    break;
                }
            }
        }
    }


    IEnumerator Teleport()
    {
        StartTeleporting();yield return new WaitForSeconds(4);
        patrol.CheckNearestPatrolWaypoint();
        while (Vector3.Distance(entity.transform.position, new Vector3(dredgeEncounterPoints[patrol.waypointIndex].transform.position.x, entity.transform.position.y, dredgeEncounterPoints[patrol.waypointIndex].transform.position.z)) > 0)
        {
            entity.transform.position = Vector3.MoveTowards(entity.transform.position, new Vector3(dredgeEncounterPoints[patrol.waypointIndex].transform.position.x, entity.transform.position.y, dredgeEncounterPoints[patrol.waypointIndex].transform.position.z), moveSpeed * Time.deltaTime);
            actualTeleportTime = Mathf.MoveTowards(actualTeleportTime, teleportTimer, Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        patrol.patrolWaypoints = dredgeEncounterPoints[actualEncounterIndex].GetComponentsInChildren<Transform>();
        entity.encounterPoint = dredgeEncounterPoints[actualEncounterIndex];
        ResetTeleporting();
    }

    void StartTeleporting()
    {
        entity.isTeleporting = true;
        entity.SetAnimationBool("submerge", true);
    }
    void ResetTeleporting()
    {
        entity.isTeleporting = false;
        entity.SetAnimationBool("submerge", false);
    }

    private void OnDrawGizmosSelected()
    {
        for (int i = 0; i < dredgeEncounterPoints.Length; i++)
        {
            if (dredgeEncounterPoints[i] != null)
            {
                Gizmos.color = new Color(1, 1, 0, 0.1f);
                Gizmos.DrawSphere(dredgeEncounterPoints[i].transform.position, entity.rangeLeash);
            }
            else
            {
                Gizmos.color = new Color(1, 1, 0, 0.1f);
                Gizmos.DrawSphere(dredgeEncounterPoints[i].transform.position, entity.rangeLeash);

            }
        }

    }

}
