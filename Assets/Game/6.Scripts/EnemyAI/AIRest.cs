using Action__;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRest : MonoBehaviour, IEnemy
{
    private Enemy entity;

    [SerializeField] ParticleSystem teleportParticle;

    [SerializeField] private float banishedTime;
    [SerializeField] private float timeToTeleport;
    [SerializeField] private Vector3 startPosition;

    private LanternTarget lanternTarget;
    private ActionStatus status;
    private BehaviourState classState = BehaviourState.Rest;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
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
            if (state == BehaviourState.Banished)
            {
                if (!entity.isBanished)
                    StartCoroutine(BackToRest());
            }
            else if (entity.isTakingDamage || entity.isAttacking)
            {
                return;
            }
            else if (Vector3.Distance(GameManager.PlayerInstance.transform.position, entity.encounterPoint.position) > entity.rangeLeash)
            {
                entity.ChangeState(status, classState);
                teleportParticle.Play();
                entity.EnemyHolder.transform.position = startPosition;
                SetRestAnimation();
                lanternTarget.ResetProgress();
            }
        }
    }

    IEnumerator BackToRest()
    {
        StartCoroutine(StartBanish());
        yield return new WaitForSeconds(banishedTime);
        entity.ChangeState(status, classState);
        ResetToDefault();
    }

    IEnumerator StartBanish()
    {
        entity.isBanished = true;
        SetRestAnimation();
        teleportParticle.Play();
        yield return new WaitForSeconds(timeToTeleport);
        entity.EnemyHolder.transform.position = startPosition;
    }

    void SetRestAnimation()
    {
        entity.SetAnimationBool("canAttack", false);
        entity.SetAnimationBool("isAttacking", false);
        entity.SetAnimationBool("isTakingDamage", false);
        entity.SetAnimationBool("isChasing", false);
        entity.PlayAnimation("afflicted_rest");
    }

    void ResetToDefault()
    {
        lanternTarget.ResetProgress();
        lanternTarget.lanternImmune = false;
        entity.canBanish = false;
        entity.isBanished = false;
    }

    private void OnDrawGizmosSelected()
    {
        //if(entity.encounterPoint != null && entity != null)
        //{
        //    Gizmos.color = new Color(1, 1, 0, 0.1f);
        //    Gizmos.DrawSphere(entity.encounterPoint.position, rangeLeash);
        //}
    }

}