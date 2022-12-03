using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action__;
using DredgeAttack;

public class AIDredgeAttack : MonoBehaviour, IEnemy
{

    Enemy entity;
    BehaviourState classState = BehaviourState.Attack;
    [SerializeField] ParticleSystem pukeParticle;
    [SerializeField] DredgeAttackVariations actualAttack;
    [SerializeField] private float speedRotation;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float speedMultiplier;

    [SerializeField] private bool isSubmerging;
    [SerializeField] private float actualPreparationTime;

    [SerializeField] private float submergeTime;
    [SerializeField] private float chompChargeTime;
    [SerializeField] private float actualAttackTime;
    [SerializeField] private float pukeAttackTime;
    [SerializeField] private float chompAttackTime;

    [SerializeField] private float minDistanceToPuke;

    [SerializeField] private float pukePreparationTime;
    [SerializeField] private float actualTimeObserving;

    public Vector3 vectorOffset;

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
            if (state == BehaviourState.Teleport)
            {
                if (isSubmerging)
                    StopCoroutine(SubmergeToAttack());
                return;
            }

            if (entity.GetDredgeAttack() != DredgeAttackVariations.Noone)
            {
                ResetAttackVar();
                entity.ChangeState(classState);
            }
        }
    }

    void StartAttack()
    {
        if (entity.isPreparingAttack || entity.canAttack || entity.isAttacking)
            return;
        actualAttack = entity.GetDredgeAttack();
        entity.isPreparingAttack = true;
        AttackToStart();
    }

    void AttackToStart()
    {
        switch (actualAttack)
        {
            case DredgeAttackVariations.Puke:
                    StartCoroutine(PukeAttack());
                break;
        }
    }

    //bool CanAttack()
    //{
    //    if (Vector3.Distance(GameManager.PlayerInstance.transform.position, entity.encounterPoint.transform.position) > entity.rangeLeash)
    //        return false;
    //    else return true;
    //}

    IEnumerator AttackPreparation(float timeToAchieve)
    {
        while (actualPreparationTime < timeToAchieve)
        {
            actualPreparationTime = Mathf.MoveTowards(actualPreparationTime, timeToAchieve, Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }    


    IEnumerator PukeAttack()
    {
        yield return StartCoroutine(AttackPreparation(pukePreparationTime));
        if (entity.isTeleporting)
            yield break;
        entity.SetAnimationTrigger("attack");
        entity.SetAnimationBool("purge", true);
        while (actualAttackTime < pukeAttackTime)
        {
            if (!pukeParticle.isPlaying)
                pukeParticle.Play();
            else
            {
                float dist = Vector3.Distance(new Vector3(GameManager.PlayerInstance.transform.position.x, 0, GameManager.PlayerInstance.transform.position.z), new Vector3 (entity.transform.position.x, 0, entity.transform.position.z));
                if (dist < minDistanceToPuke)
                {
                    if (!isSubmerging)
                    {
                        ResetPukeAttack();
                        entity.SetDredgeAttack(DredgeAttackVariations.Submerge);
                        StartCoroutine(SubmergeToAttack());
                    }
                    yield break;
                }
            }
            actualAttackTime = Mathf.MoveTowards(actualAttackTime, pukeAttackTime, Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        ResetPukeAttack();
        entity.SetDredgeAttack(entity.attackSequenceCount == entity.GetMaxComboSequence() ? DredgeAttackVariations.Submerge : DredgeAttackVariations.Noone);
        if(entity.attackSequenceCount == entity.GetMaxComboSequence())
            StartCoroutine(SubmergeToAttack());
        entity.attackSequenceCount += 1;
    }

    void ResetPukeAttack()
    {
        actualAttackTime = 0;
        entity.SetAnimationBool("purge", false);
        if (pukeParticle.isPlaying)
            pukeParticle.Stop();
    }

    IEnumerator SubmergeToAttack()
    {
        if (entity.isTeleporting)
            yield break;
        isSubmerging = true;
        entity.SetAnimationBool("submerge", true);
        yield return new WaitForSeconds(submergeTime);

        isSubmerging = false;
        entity.SetDredgeAttack(DredgeAttackVariations.Chomp);
        StartCoroutine(ChompChargeAttack());
        entity.attackSequenceCount = 1;
    }

    IEnumerator ChompChargeAttack()
    {
        if (entity.isTeleporting)
            yield break;
        actualPreparationTime = 0;
        while(actualPreparationTime < chompChargeTime)
        {
            float multiplier = Vector3.Distance(entity.transform.position, GameManager.PlayerInstance.transform.position) > entity.rangeDetection ? speedMultiplier : 1;

            entity.transform.position = Vector3.MoveTowards(entity.transform.position, new Vector3(GameManager.PlayerInstance.transform.position.x, entity.transform.position.y, GameManager.PlayerInstance.transform.position.z), moveSpeed * multiplier * Time.deltaTime);
            actualPreparationTime = Mathf.MoveTowards(actualPreparationTime, chompChargeTime, Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        StartCoroutine(ChompAttack());

    }

    IEnumerator ChompAttack()
    {
        if (entity.isTeleporting)
            yield break;
        entity.SetAnimationTrigger("attack");
        while (actualAttackTime < chompAttackTime)
        {
            if(actualAttackTime <= chompAttackTime*0.2f)
            entity.transform.position = Vector3.MoveTowards(entity.transform.position, new Vector3(GameManager.PlayerInstance.transform.position.x, entity.transform.position.y, GameManager.PlayerInstance.transform.position.z), moveSpeed * Time.deltaTime);
            actualAttackTime = Mathf.MoveTowards(actualAttackTime, chompAttackTime, Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        Debug.Log("ChompFinish");
        if(entity.attackSequenceCount == entity.GetMaxComboSequence())
        {
            entity.attackSequenceCount  = 1;
            actualAttackTime = 0;
            entity.SetDredgeAttack(DredgeAttackVariations.Hide);
            entity.SetAnimationBool("submerge", true);
        }
        else
        {
            actualAttackTime = 0;
            entity.attackSequenceCount += 1;
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(ChompAttack());
        }
    }

    private void UpdateRotation()
    {
        Vector3 dir = GameManager.PlayerInstance.transform.position - entity.EnemyHolder.transform.position;
        dir.y = 0;
        Quaternion quaternion = Quaternion.LookRotation(dir, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, quaternion, speedRotation * Time.deltaTime);

    }

    private void ResetAttackVar()
    {
        actualPreparationTime = 0;
        isSubmerging = false;
        entity.isPreparingAttack = false;
        entity.canAttack = false; 
        entity.isAttacking = false;
    }
    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, Vector3.Distance(GameManager.PlayerInstance.transform.position, entity.transform.position));
    }
    */


}
