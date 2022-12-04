using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action__;
using DredgeAttack;

public class AIDredgeChooseAttack : MonoBehaviour, IEnemy
{
    Enemy entity;
    BehaviourState classState = BehaviourState.Observing;

    [SerializeField] private float speedRotation;

    [SerializeField] private bool isObserving;
    [SerializeField] private float observeTime;
    [SerializeField] private float actualTimeObserving;
    [SerializeField] private float stopObservingDistance;

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
            if (!isObserving || !entity.isTeleporting)
            {
                StartCoroutine(ObservingPlayer());
                entity.StartCombatMusic();
            }
        }
        else
        {
            if (entity.isTeleporting && entity.GetDredgeAttack() != DredgeAttackVariations.Hide || Vector3.Distance(entity.encounterPoint.transform.position, GameManager.PlayerInstance.transform.position) > entity.rangeLeash)
                return;
            if (Vector3.Distance(entity.transform.position, GameManager.PlayerInstance.transform.position) <= entity.rangeDetection && entity.GetDredgeAttack() == DredgeAttackVariations.Noone)
            {
                ResetObservingTimer();
                entity.ChangeState(classState);
            }
        }
    }

    IEnumerator ObservingPlayer()
    {
        isObserving = true;

        entity.SetAnimationBool("alert", true);
        entity.SetAnimationBool("isObserving", true);

        while (actualTimeObserving < observeTime)
        {
            if(Vector3.Distance(entity.transform.position, GameManager.PlayerInstance.transform.position) >= entity.rangeDetection)
            {
                ResetObservingTimer();
                yield break;
            }
            Vector3 dir = GameManager.PlayerInstance.transform.position - entity.transform.position;
            Quaternion quaternion = Quaternion.LookRotation(dir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, quaternion, speedRotation * Time.deltaTime);
            actualTimeObserving = Mathf.MoveTowards(actualTimeObserving, observeTime, Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        ChooseAttack();
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
