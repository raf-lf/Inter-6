using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action__;

public class AfflictedAIAttack : MonoBehaviour, IEnemy
{
    private Enemy entity;

    private Coroutine chargeAttackCoroutine;

    [SerializeField] private float chargeTimer;
    [SerializeField] private float attackTimer;
    [SerializeField] private int[] timeToAttackRange = new int[2];

    [SerializeField] private bool isPreparingAttack;

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
                chargeAttackCoroutine = StartCoroutine(ChargeAttackTimer());
        }
        else
        {
            if (entity.isTakingDamage)
            {
                RestoreToDefault();
                return;
            }

            if (entity.canAttack)
            {
                StopCoroutine(chargeAttackCoroutine);
                RestoreToDefault();
            }
            if (state == BehaviourState.Chase)
            {
                if (!isPreparingAttack)
                {
                    int chooseTimeToAttack = Random.RandomRange(timeToAttackRange[0], timeToAttackRange[1]);
                    StartCoroutine(PrepareAttackTimer(chooseTimeToAttack));
                }
            }
        }
    }

    IEnumerator ChargeAttackTimer()
    {
        SetChargingAnimation();
        yield return new WaitForSeconds(chargeTimer);
        entity.isAttacking = true;
        SetAttackAnimation();
        yield return new WaitForSeconds(attackTimer);
        entity.isAttacking = false;
        entity.canAttack = false;
    }

    IEnumerator PrepareAttackTimer(int chooseTimeToAttack)
    {
        entity.isAttacking = false;
        entity.canAttack = false;
        isPreparingAttack = true;
        yield return new WaitForSeconds(chooseTimeToAttack);
        entity.ChangeState(status, classState);
        entity.canAttack = true;
        isPreparingAttack = false;
    }
    void RestoreToDefault()
    {
        entity.isAttacking = false;
        entity.canAttack = false;
        isPreparingAttack = false;
    }

    void SetAttackAnimation()
    {
        entity.SetAnimationBool("isAttacking", entity.isAttacking);
    }    
    
    void SetChargingAnimation()
    {
        entity.SetAnimationBool("canAttack", entity.canAttack);
    }
}
