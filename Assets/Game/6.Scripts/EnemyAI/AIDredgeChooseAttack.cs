using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action__;
using DredgeAttack;

public class AIDredgeChooseAttack : MonoBehaviour, IEnemy
{
    Enemy entity;
    ActionStatus status = ActionStatus.Running;
    BehaviourState classState = BehaviourState.Observing;

    [SerializeField] private float speedRotation;
    [SerializeField] private float DredgeHead;

    [SerializeField] private bool isObserving;
    [SerializeField] private float observeTime;
    [SerializeField] private float actualTimeObserving;
    [SerializeField] private float maxDistanceToTackle;

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
            if(!isObserving)
            StartCoroutine(ObservingPlayer());
        }
        else
        {

            if (Vector3.Distance(entity.EnemyHolder.transform.position, GameManager.PlayerInstance.transform.position) <= entity.rangeDetection && entity.GetDredgeAttack() == DredgeAttackVariations.Noone)
            {
                ResetObservingTimer();
                entity.ChangeState(status, classState);
            }
        }
    }

    IEnumerator ObservingPlayer()
    {
        isObserving = true;

        entity.SetAnimationBool("attackingPurge", true);

        while (actualTimeObserving < observeTime)
        {
            Vector3 dir = GameManager.PlayerInstance.transform.position - entity.EnemyHolder.transform.position;
            Quaternion quaternion = Quaternion.LookRotation(dir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, quaternion, speedRotation * Time.deltaTime);
            actualTimeObserving = Mathf.MoveTowards(actualTimeObserving, observeTime, Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        ChooseAttack();
    }

    void ChooseAttack()
    {
        float dist = Vector3.Distance(GameManager.PlayerInstance.transform.position, entity.transform.position);
        if(dist <= maxDistanceToTackle)
            entity.SetDredgeAttack(DredgeAttackVariations.Tackle);
        else
            entity.SetDredgeAttack(DredgeAttackVariations.Puke);
    }

    void ResetObservingTimer()
    {
        isObserving = false;
        actualTimeObserving = 0;
    }
}
