using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action__;

public class AIChase : MonoBehaviour, IEnemy
{
    private Enemy entity;

    [SerializeField] private Transform encounterPoint;
    [SerializeField] private float moveSpeed;

    private ActionStatus status;
    private BehaviourState classState = BehaviourState.Rest;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AIActionExecuting(BehaviourState state)
    {
     
        if(state == classState)
        {
            transform.position = Vector3.MoveTowards(transform.position, GameManager.PlayerInstance.transform.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            if (Vector3.Distance(encounterPoint.position, GameManager.PlayerInstance.transform.position) <= entity.rangeDetection)
            {
                entity.ChangeState(status, classState);
                SetChaseAnimation();
            }
        }
    }

    void SetChaseAnimation()
    {
        entity.PlayAnimation("afflicted_chase");
    }

}
