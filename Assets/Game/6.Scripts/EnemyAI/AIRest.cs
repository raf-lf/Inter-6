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
            float dist = Vector3.Distance(GameManager.PlayerInstance.transform.position, entity.encounterPoint.position);
            if (state == BehaviourState.Banished)
            {
                if (!entity.isBanished)
                    StartCoroutine(BackToRest());
            }
            else if (entity.isTakingDamage || entity.isAttacking)
            {
                return;
            }
            else if (dist > entity.rangeLeash )
            {
                entity.ChangeState( classState);
                teleportParticle.Play();
                entity.EnemyHolder.transform.position = startPosition;
                SetRestAnimation();
                lanternTarget.ResetProgress();
                entity.StopCombatMusic();
            }
        }
    }

    IEnumerator BackToRest()
    {
        StartCoroutine(StartBanish());
        yield return new WaitForSeconds(banishedTime);
        entity.ChangeState(classState);
        ResetToDefault();
    }

    IEnumerator StartBanish()
    {
        entity.isBanished = true;
        SetRestAnimation();
        teleportParticle.Play();
        entity.StopCombatMusic();
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
