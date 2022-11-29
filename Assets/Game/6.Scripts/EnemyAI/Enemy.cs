using System;
using System.Collections.Generic;
using UnityEngine;
using Action__;
using DredgeAttack;

public class Enemy : BehaviourManager
{
    public Transform EnemyHolder;
    [SerializeField]
    private Animator animator;

    public Transform encounterPoint;

    public int attackSequenceCount = 1;
    [SerializeField] int maxAttackCombo;

    public float rangeDetection;
    public float rangeLeash;


    /*[HideInInspector]*/ public static bool Stop;
    /*[HideInInspector]*/ public bool isTeleporting;
    [HideInInspector] public bool isBanished;
    [HideInInspector] public bool isPreparingAttack;
    [HideInInspector] public bool canBanish;
    [HideInInspector] public bool canAttack;
    [HideInInspector] public bool isAttacking;
    [HideInInspector] public bool isTakingDamage;

    public Action<BehaviourState> EnemyActions;

    [Header("ActualActions")]
    [SerializeField]
    private BehaviourState ActualState = BehaviourState.Patrol;
    [SerializeField]
    private DredgeAttackVariations dredgeAttack = DredgeAttackVariations.Puke;

    protected virtual void Awake()
    {
        stateManager = gameObject.GetComponent<StateManager>();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        if (encounterPoint == null)
        {
            var point = new GameObject();
            point.transform.position = transform.position;
            point.name = "SelfEncounterPoint";
            encounterPoint = point.transform;
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    // Update is called once per frame
    void Update()
    {
        if (Stop)
            return;
        if (!isBanished)
            EnemyActions?.Invoke(ActualState);
        attackSequenceCount = Mathf.Clamp(attackSequenceCount, 1, maxAttackCombo);

    }

    public override void ActionToExecute(BehaviourState state)
    {

    }

    public void ChangeState(BehaviourState state)
    {
        ActualState = state;
    }


    public void SetAnimationBool(string state, bool condition)
    {
        animator.SetBool(state, condition);
    }    
    
    public BehaviourState GetActualState()
    {
        return ActualState;
    }
    
    public int GetMaxComboSequence()
    {
        return maxAttackCombo;
    }

    public void PlayAnimation(string state)
    {
        animator.Play(state);
    }

    public void SetAnimationTrigger(string state)
    {
        animator.SetTrigger(state);
    }   
    
    public bool GetAnimationStatus(string state)
    {
        return animator.GetBool(state);
    }

    public DredgeAttackVariations GetDredgeAttack()
    {
        return dredgeAttack;
    }

    public void SetDredgeAttack(DredgeAttackVariations _dredgeAttack)
    {
        dredgeAttack = _dredgeAttack;
    }

    private void OnDrawGizmosSelected()
    {

        if (encounterPoint != null)
        {
            Gizmos.color = new Color(1, 0, 0, 0.1f);
            Gizmos.DrawSphere(encounterPoint.position, rangeDetection);
            Gizmos.color = new Color(1, 1, 0, 0.1f);
            Gizmos.DrawSphere(encounterPoint.position, rangeLeash);
        }
        else
        {
            Gizmos.color = new Color(1, 0, 0, 0.1f);
            Gizmos.DrawSphere(transform.position, rangeDetection);
            Gizmos.color = new Color(1, 1, 0, 0.1f);
            Gizmos.DrawSphere(transform.position, rangeLeash);

        }

    }
}
