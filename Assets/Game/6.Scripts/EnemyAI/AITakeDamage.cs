using Action__;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITakeDamage : MonoBehaviour, IEnemy
{
    private Enemy entity;
    private LanternTarget lanternTarget;
    private ActionStatus status;
    private BehaviourState classState = BehaviourState.Banishing;
    [SerializeField]
    private float timeToBeBanished;
    private float actualTimeBeingBanished;
    private float waitTime = 1;

    // Start is called before the first frame update
    void Start()
    {
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
            if (!entity.isTakingDamage)
                StartCoroutine(TakeDamage());
        }
        else
        {
            ResetDefault();
            if (lanternTarget.lanternProgress > 0 && !entity.canBanish)
            {
                entity.ChangeState(status, classState);
            }
        }
    }

    IEnumerator TakeDamage()
    {
        while (lanternTarget.lanternProgress > 0)
        {
            entity.isTakingDamage = true;
            StartBanishingAnimation();
            if (lanternTarget.lanternProgress >= lanternTarget.targetThreshold)
            {
                entity.canBanish = true;
            }
            yield return new WaitForSeconds(waitTime);
        }
        entity.isTakingDamage = false;
        StopBanishingAnimation();
    }

    void StopBanishingAnimation()
    {
        entity.SetAnimationBool("isTakingDamage", false);
    }

    void StartBanishingAnimation()
    {
        entity.SetAnimationBool("isAttacking", false);
        entity.SetAnimationBool("canAttack", false);
        entity.SetAnimationBool("isTakingDamage", true);
    }

    void ResetDefault()
    {


    }


}
