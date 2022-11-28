using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action__;

[RequireComponent(typeof(BehaviourManager))]
public class AIAttack : MonoBehaviour, IEnemy
{

    private Enemy entity;
    public BehaviourState classState = BehaviourState.Attack;

    [SerializeField] private float attackRange;

    [SerializeField]
    float waitTimer;
    [SerializeField]
    float moveSpeed;

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
            transform.position = Vector3.MoveTowards(transform.position, GameManager.PlayerInstance.transform.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            if (Vector3.Distance(transform.position, GameManager.PlayerInstance.transform.position) <= attackRange && !entity.isTakingDamage)
            {
                entity.ChangeState( classState);
                StartAttackAnimation();
            }
            else
            {
                ResetAttackAnimation();
            }
        }
    }

    void StartAttackAnimation()
    {
        entity.SetAnimationBool("attacking", true);
    }

    void ResetAttackAnimation()
    {
        entity.SetAnimationBool("attacking", true);
    }
}