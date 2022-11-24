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
    [SerializeField] GameObject DredgeHead;
    [SerializeField] GameObject DredgeHeadPuke;
    [SerializeField] DredgeAttackVariations actualAttack;
    [SerializeField] private float speedRotation;

    [SerializeField] private float actualPreparationTime;

    [SerializeField] private float actualPukeTime;
    [SerializeField] private float pukeAttackTime;

    [SerializeField] private float pukePreparationTime;
    [SerializeField] private float hungryPreparationTime;
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
            if (entity.GetDredgeAttack() != DredgeAttackVariations.Noone)
            {
                ResetAttackVar();
                entity.ChangeState(status, classState);
            }
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
        entity.SetAnimationTrigger("triggerAttack");

        while (actualPukeTime < pukeAttackTime)
        {
            if (!pukeParticle.isPlaying)
                pukeParticle.Play();
            actualPukeTime = Mathf.MoveTowards(actualPukeTime, pukeAttackTime, Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        entity.SetAnimationBool("attackingPurge", false);

        if (pukeParticle.isPlaying)
            pukeParticle.Stop();
        actualPukeTime = 0;
        entity.SetDredgeAttack(DredgeAttackVariations.Noone);
        //ResetAttackVar();
    }

    IEnumerator HungryAttack()
    {
        yield return new WaitForEndOfFrame();
    }

    private void UpdateRotation()
    {
        Vector3 dir = GameManager.PlayerInstance.transform.position - entity.EnemyHolder.transform.position;
        Vector3 dirPuke = dir;
        dir.y = 0;
        float dist = Vector3.Distance(GameManager.PlayerInstance.transform.position, entity.EnemyHolder.transform.position);
        float distY = GameManager.PlayerInstance.transform.position.y - entity.EnemyHolder.transform.position.y;
        Quaternion quaternion = Quaternion.LookRotation(dir, Vector3.up);
        Quaternion quaternionLook = Quaternion.LookRotation(dirPuke, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, quaternion, speedRotation * Time.deltaTime);
        //float vel = Mathf.Sqrt(dist * 9.81f);
        //pukeParticle.startSpeed = vel; 
        //float ai = Mathf.Sin(dist * 9.81f / vel * vel);

        //float teta = Mathf.Asin(ai) /2f;
        ////Debug.Log(" distY" + distY);
        ////Debug.Log("teta" + teta);
        ////DredgeHeadPuke.transform.rotation = Quaternion.RotateTowards(transform.rotation, quaternionLook, speedRotation * Time.deltaTime);
        //DredgeHeadPuke.transform.rotation = Quaternion.Euler(Mathf.Rad2Deg*teta, quaternionLook.eulerAngles.y, quaternionLook.eulerAngles.z);

    }

    private void ResetAttackVar()
    {
        actualPreparationTime = 0;
        entity.isPreparingAttack = false;
        entity.canAttack = false; 
        entity.isAttacking = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, Vector3.Distance(GameManager.PlayerInstance.transform.position, entity.EnemyHolder.transform.position));
    }


}
