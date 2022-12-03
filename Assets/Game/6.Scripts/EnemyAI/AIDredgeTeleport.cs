using Action__;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDredgeTeleport : MonoBehaviour, IEnemy
{
    Enemy entity;
    AIPatrol patrol;

    [SerializeField] private EncounterPoints encounterPoints;

    [SerializeField] private int actualEncounterIndex;

    //[SerializeField] private float moveSpeed;

    BehaviourState actualState = BehaviourState.Teleport;

    void Awake()
    {
        entity = GetComponent<Enemy>();
        SetNewencounterPoint();
    }
    void Start()
    {
        patrol = GetComponent<AIPatrol>();
        patrol.patrolWaypoints.AddRange(encounterPoints.dredgeEncounterPoints[actualEncounterIndex].GetComponentsInChildren<Transform>());
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
            if(state == BehaviourState.Patrol || entity.GetDredgeAttack() == DredgeAttack.DredgeAttackVariations.Hide)
            CheckNeededTeleport();
        }
    }

    void CheckNeededTeleport()
    {   
        if(Vector3.Distance(entity.encounterPoint.transform.position, GameManager.PlayerInstance.transform.position) > entity.rangeLeash)
        {
            for (int i = 0; i < encounterPoints.dredgeEncounterPoints.Length; i++)
            {
                if (Vector3.Distance(encounterPoints.dredgeEncounterPoints[i].transform.position, GameManager.PlayerInstance.transform.position) < entity.rangeLeash)
                {
                    actualEncounterIndex = i;
                    entity.ChangeState(actualState);
                    break;
                }
            }
        }
    }

    void SetNewencounterPoint()
    {
        entity.encounterPoint = encounterPoints.dredgeEncounterPoints[actualEncounterIndex];
        entity.rangeLeash = encounterPoints.rangeLeash[actualEncounterIndex];
        entity.rangeDetection = encounterPoints.rangeDetection[actualEncounterIndex];
    }


    IEnumerator Teleport()
    {
        StartTeleporting();yield return new WaitForSeconds(4);
        patrol.patrolWaypoints.Clear();
        patrol.patrolWaypoints.AddRange(encounterPoints.dredgeEncounterPoints[actualEncounterIndex].GetComponentsInChildren<Transform>());
        patrol.patrolWaypoints.RemoveAt(0);
        patrol.CheckNearestPatrolWaypoint();
        if(Vector3.Distance(entity.transform.position, new Vector3(patrol.patrolWaypoints[patrol.waypointIndex].transform.position.x, entity.transform.position.y, patrol.patrolWaypoints[patrol.waypointIndex].transform.position.z)) != 0)
            entity.transform.position = patrol.patrolWaypoints[patrol.waypointIndex].transform.position;

        SetNewencounterPoint();
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
        entity.SetAnimationBool("alert", false);
        entity.SetDredgeAttack(DredgeAttack.DredgeAttackVariations.Noone);
        entity.attackSequenceCount = 1;
    }

    private void OnDrawGizmos()
    {
        if (encounterPoints.dredgeEncounterPoints.Length > 0)
        {
            Gizmos.color = new Color(1, 1, 0, 0.1f);
            for (int i = 0; i < encounterPoints.dredgeEncounterPoints.Length; i++)
            {
                if(encounterPoints.dredgeEncounterPoints[i] != null)
                    Gizmos.DrawSphere(encounterPoints.dredgeEncounterPoints[i].transform.position, encounterPoints.rangeLeash[i]);
            }
        }
    }

}

[System.Serializable]
public class EncounterPoints
{
    public Transform[] dredgeEncounterPoints;
    public float[] rangeLeash;
    public float[] rangeDetection;
}