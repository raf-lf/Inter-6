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
    [SerializeField] GameObject DredgeHead;

    Coroutine observingPlayer;

    [SerializeField] private bool isObserving;
    [SerializeField] private float observeTime;
    [SerializeField] private float actualTimeObserving;

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
            observingPlayer = StartCoroutine(ObservingPlayer());
        }
        else
        {
            if (entity.isTakingDamage)
                return;

            if (Vector3.Distance(entity.encounterPoint.position, GameManager.PlayerInstance.transform.position) <= entity.rangeDetection)
            {
                ResetObservingTimer();
                entity.ChangeState(status, classState);
            }
        }
    }

    IEnumerator ObservingPlayer()
    {
        isObserving = true;
        while (actualTimeObserving < observeTime)
        {
            DredgeHead.transform.LookAt(GameManager.PlayerInstance.transform.position, transform.forward);
            actualTimeObserving = Mathf.MoveTowards(actualTimeObserving, observeTime, Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        ChooseAttack();
        isObserving = false;
    }

    void ChooseAttack()
    {
        entity.SetDredgeAttack(DredgeAttackVariations.Puke);
    }

    void ResetObservingTimer()
    {
        isObserving = false;
        actualTimeObserving = 0;
    }
}
