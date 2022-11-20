using Action__;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRest : MonoBehaviour, IEnemy
{
    private Enemy entity;

    [SerializeField] ParticleSystem teleportParticle;

    [SerializeField] private float rangeLeash;
    [SerializeField] private float banishedTime;
    bool isWaiting;
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
            else if (Vector3.Distance(GameManager.PlayerInstance.transform.position, entity.encounterPoint.position) > rangeLeash)
            {
                entity.ChangeState(status, classState);
                teleportParticle.Play();
                entity.EnemyHolder.transform.position = startPosition;
                SetRestAnimation();
            }
        }
    }

    IEnumerator BackToRest()
    {
        entity.isBanished = true;
        SetRestAnimation();
        teleportParticle.Play();
        entity.EnemyHolder.transform.position = startPosition;
        yield return new WaitForSeconds(banishedTime);
        entity.ChangeState(status, classState);
        entity.canBanish = false;
        entity.isBanished = false;
    }

    void SetRestAnimation()
    {
        entity.SetAnimationBool("canAttack", false);
        entity.SetAnimationBool("isAttacking", false);
        entity.SetAnimationBool("isTakingDamage", false);
        entity.SetAnimationBool("isChasing", false);
        entity.PlayAnimation("afflicted_rest");
        lanternTarget.ResetProgress();
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
