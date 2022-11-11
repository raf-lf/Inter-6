using System;
using System.Collections.Generic;
using UnityEngine;
using Action__;

public class Enemy : BehaviourManager
{
    public IEnemy enemy;
    //[SerializeField]
    //List<EnemyBehaviour> enemyBehaviour = new List<EnemyBehaviour>();
    //Dictionary<string, EnemyInterchangeState> enemyActions;


    public Transform EnemyHolder;
    [SerializeField]
    private Animator animator;

    public Transform encounterPoint;

    [HideInInspector]
    public bool isBanished;
    [HideInInspector]
    public bool canBanish;
    [HideInInspector]
    public bool canAttack;
    [HideInInspector]
    public bool isAttacking;
    [HideInInspector]
    public bool isTakingDamage;

    public Action<BehaviourState> EnemyActions;

    [Header("ActualActions")]
    [SerializeField]
    private BehaviourState ActualState = BehaviourState.Patrol;
    //private BehaviourNodes ActualNode = BehaviourNodes.Wait;

    protected virtual void Awake()
    {
        stateManager = gameObject.GetComponent<StateManager>();
        enemy = gameObject.GetComponent<IEnemy>();
        animator = gameObject.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();


        if (encounterPoint == null)
        {
            var point = Instantiate(new GameObject());
            point.transform.position = transform.position;
            point.name = "SelfEncounterPoint";
            encounterPoint = point.transform;
        }
        //enemyActions = new Dictionary<string, EnemyInterchangeState>();
        //for (int index = 0; index < enemyBehaviour.Count; index++)
        //{
        //    enemyActions.Add(enemyBehaviour[index].ActualState.ToString(), enemyBehaviour[index].NextActions);
        //}
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isBanished)
            EnemyActions?.Invoke(ActualState);
    }

    public override void ActionToExecute(ActionStatus status, BehaviourState state)
    {
        //if (enemyActions.ContainsKey(state.ToString()))
        //{
        //    if (ActualState == state)
        //        ChangeNode(status, state);
        //    else
        //        ChangeState(status, state);
        //}
    }
    public void ChangeState(ActionStatus status, BehaviourState state)
    {
        ActualState = state;
        //ActualState = enemyActions[state.ToString()].GetNextState(status);
        //ActualNode = enemyActions[state.ToString()].ResetNodesIndex();
    }

    public void ChangeNode(ActionStatus status, BehaviourState state)
    {
        //ActualNode = enemyActions[state.ToString()].GetNextNode(status);
    }


    public void SetAnimationBool(string state, bool condition)
    {
        //animator.SetBool(state, condition);
    }

    public void SetAnimationTrigger(string state)
    {
        //animator.SetTrigger(state);
    }
}
[System.Serializable]
[RequireComponent(typeof(EnemyInterchangeState))]
public class EnemyBehaviour
{
    public BehaviourState ActualState;
    public EnemyInterchangeState NextActions;

}
