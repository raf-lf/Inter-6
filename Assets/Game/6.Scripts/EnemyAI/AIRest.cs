using Action__;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRest : MonoBehaviour, IEnemy
{
    private Enemy entity;
    [SerializeField] private float rangeDetection;
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
            else if (Vector3.Distance(entity.EnemyHolder.transform.position, entity.encounterPoint.position) > rangeLeash)
            {
                entity.ChangeState(status, classState);
                entity.EnemyHolder.transform.position = startPosition;
                SetRestAnimation();
            }
        }
    }

    IEnumerator BackToRest()
    {
        entity.isBanished = true;
        yield return new WaitForSeconds(banishedTime);
        entity.EnemyHolder.transform.position = startPosition;
        entity.ChangeState(status, classState);
        entity.canBanish = false;
        entity.isBanished = false;
    }

    void SetRestAnimation()
    {
        entity.SetAnimationBool("chasing", false);
        entity.SetAnimationBool("attacking", false);
        entity.SetAnimationBool("banishing", false);
        entity.SetAnimationTrigger("resting");
        lanternTarget.ResetProgress();
    }

}
