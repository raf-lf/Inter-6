using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action__;
using DredgeAttack;

public class AIDredgeAttack : MonoBehaviour, IEnemy
{

    Enemy entity;
    ActionStatus status = ActionStatus.Running;
    BehaviourState classState = BehaviourState.Attack;
    [SerializeField] ParticleSystem pukeParticle;
    [SerializeField] DredgeAttackVariations actualAttack;
    [SerializeField] private float speedRotation;

    [SerializeField] private float actualPreparationTime;

    [SerializeField] private float actualPukeTime;
    [SerializeField] private float pukeAttackTime;

    [SerializeField] private float pukePreparationTime;
    [SerializeField] private float hungryPreparationTime;
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
            
            if (entity.GetDredgeAttack() == DredgeAttackVariations.Noone)
                return;
            StartAttack();
            UpdateRotation();
        }
        else
        {
            if (entity.GetDredgeAttack() != DredgeAttackVariations.Noone)
                entity.ChangeState(status, classState);
        }
    }

    void StartAttack()
    {
        if (entity.isPreparingAttack || entity.canAttack || entity.isAttacking)
            return;
        actualAttack = entity.GetDredgeAttack();
        entity.isPreparingAttack = true;
        switch (actualAttack)
        {
            case DredgeAttackVariations.Hungry:
                StartCoroutine(HungryAttackPreparation());
                break;
            case DredgeAttackVariations.Puke:  
                StartCoroutine(PukeAttackPreparation());
                break;
        }
    }

    IEnumerator PukeAttackPreparation()
    {
        while (actualPreparationTime < pukePreparationTime)
        {
            actualPreparationTime = Mathf.MoveTowards(actualPreparationTime, pukePreparationTime, Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(PukeAttack());
    }    
    
    IEnumerator HungryAttackPreparation()
    {
        while (actualPreparationTime < hungryPreparationTime)
        {
            actualPreparationTime = Mathf.MoveTowards(actualPreparationTime, hungryPreparationTime, Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        StartCoroutine(HungryAttack());
    }
    IEnumerator PukeAttack()
    {
        Debug.Log("Puke");
        while (actualPukeTime < pukeAttackTime)
        {
            if (!pukeParticle.isPlaying)
                pukeParticle.Play();
            actualPukeTime = Mathf.MoveTowards(actualPukeTime, pukeAttackTime, Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        if(pukeParticle.isPlaying)
            pukeParticle.Stop();
        actualPukeTime = 0;
        ResetAttackVar();
    }

    IEnumerator HungryAttack()
    {
        yield return new WaitForEndOfFrame();
    }

    private void UpdateRotation()
    {
        Vector3 dir = GameManager.PlayerInstance.transform.position - entity.EnemyHolder.transform.position;
        Quaternion quaternion = Quaternion.LookRotation(dir, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, quaternion, speedRotation * Time.deltaTime);
    }

    private void ResetAttackVar()
    {
        actualPreparationTime = 0;
        entity.isPreparingAttack = false;
        entity.canAttack = false; 
        entity.isAttacking = false;
        entity.SetDredgeAttack(DredgeAttackVariations.Noone);
    }

}
