using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action__;

public class AfflictedAIAttack : MonoBehaviour, IEnemy
{
    private Enemy entity;

    private Coroutine chargeAttackCoroutine;
    private Coroutine prepareAttackCoroutine;

    [SerializeField] private float chargeTimer;
    [SerializeField] private float attackTimer;
    [SerializeField] private int[] timeToAttackRange = new int[2];

    private ActionStatus status;
    private BehaviourState classState = BehaviourState.Attack;

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
            if (entity.canAttack && !entity.isAttacking)
                StartCoroutine(ChargeAttackTimer());
        }
        else
        {
            if(state == BehaviourState.Rest)
            {
                if (entity.isPreparingAttack)
                    StopCoroutine(PrepareAttackTimer(0));
                if (entity.canAttack)
                    StopCoroutine(ChargeAttackTimer());
                RestoreToDefault();

                return;
            }
            if (entity.isTakingDamage)
            {
                RestoreToDefault();
                return;
            }

            if (entity.canAttack)
            {
                StopCoroutine(chargeAttackCoroutine);
                RestoreToDefault();
                return;
            }
            if (state == BehaviourState.Chase)
            {
                if (!entity.isPreparingAttack)
                {
                    ResetAnimation();
                    RestoreToDefault();
                    int chooseTimeToAttack = Random.RandomRange(timeToAttackRange[0], timeToAttackRange[1]);
                    StartCoroutine(PrepareAttackTimer(chooseTimeToAttack));
                }
            }
        }
    }

    IEnumerator ChargeAttackTimer()
    {
        if(entity.GetActualState() != classState)
            yield break;
        SetChargingAnimation();
        yield return new WaitForSeconds(chargeTimer);
        entity.isAttacking = true;
        SetAttackAnimation();
        yield return new WaitForSeconds(attackTimer);
        entity.isAttacking = false;
        entity.canAttack = false;
        RestoreToDefault();
    }

    IEnumerator PrepareAttackTimer(int chooseTimeToAttack)
    {
        entity.isAttacking = false;
        entity.canAttack = false;
        entity.isPreparingAttack = true;
        yield return new WaitForSeconds(chooseTimeToAttack);
        if (entity.GetActualState() != BehaviourState.Chase)
            yield break;
        entity.ChangeState(status, classState);
        entity.canAttack = true;
    }
    void RestoreToDefault()
    {
        entity.isAttacking = false;
        entity.canAttack = false;
        entity.isPreparingAttack = false;
        ResetAnimation();
    }

    void SetAttackAnimation()
    {
        entity.SetAnimationBool("isAttacking", true);
    }    
    
    void SetChargingAnimation()
    {
        entity.SetAnimationBool("canAttack", true);
    }

    void ResetAnimation()
    {
        entity.SetAnimationBool("canAttack", false);
        entity.SetAnimationBool("isAttacking", false);
    }
}
