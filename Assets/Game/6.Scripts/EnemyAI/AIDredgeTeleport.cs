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
        patrol.patrolWaypoints.AddRange(dredgeEncounterPoints[actualEncounterIndex].GetComponentsInChildren<Transform>());
        patrol.patrolWaypoints.RemoveAt(0);
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
            if (!entity.isTeleporting && Vector3.Distance(entity.encounterPoint.transform.position, GameManager.PlayerInstance.transform.position) > entity.rangeLeash)
                StartCoroutine(Teleport());
        }
        else
        {
            if(state == BehaviourState.Patrol)
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
                    Debug.Log("CheckNeededTeleport");
                    break;
                }
            }
        }
    }


    IEnumerator Teleport()
    {
        StartTeleporting();yield return new WaitForSeconds(4);
        patrol.CheckNearestPatrolWaypoint();
        while (Vector3.Distance(entity.transform.position, new Vector3(dredgeEncounterPoints[actualEncounterIndex].transform.position.x, entity.transform.position.y, dredgeEncounterPoints[actualEncounterIndex].transform.position.z)) != 0)
        {
            entity.transform.position = Vector3.MoveTowards(entity.transform.position, new Vector3(dredgeEncounterPoints[actualEncounterIndex].transform.position.x, entity.transform.position.y, dredgeEncounterPoints[actualEncounterIndex].transform.position.z), moveSpeed * Time.deltaTime);
            actualTeleportTime = Mathf.MoveTowards(actualTeleportTime, teleportTimer, Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        patrol.patrolWaypoints.Clear();
        patrol.patrolWaypoints.AddRange(dredgeEncounterPoints[actualEncounterIndex].GetComponentsInChildren<Transform>());
        patrol.patrolWaypoints.RemoveAt(0);
        entity.encounterPoint = dredgeEncounterPoints[actualEncounterIndex];
        ResetTeleporting();
    }

    void StartTeleporting()
    {
        Debug.Log("TESTE");
        entity.isTeleporting = true;
        entity.SetAnimationBool("submerge", true);
    }
    void ResetTeleporting()
    {
        entity.isTeleporting = false;
        entity.SetAnimationBool("submerge", false);
        entity.SetAnimationBool("alert", false);
        entity.SetDredgeAttack(DredgeAttack.DredgeAttackVariations.Noone);
        entity.attackSequenceCount = 1;
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
