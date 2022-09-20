using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    [Tooltip("If encounter point is NOT selected, zombie will define start position on self.")]
    [SerializeField] private Transform encounterPoint;
    [SerializeField] private float rangeDetection;
    [SerializeField] private float rangeLeash;
    [SerializeField] private float rangeAttack;
    [SerializeField] private float moveSpeed;
    private Animator animator;
    private CharacterController controller;
    private LanternTarget lanternTarget;
    private bool banished;

    public enum StateZombie { resting, chasing, attacking, banishing, banished  }
    public StateZombie currentState;

    private void Awake()
    {
        if (encounterPoint == null)
            encounterPoint = transform;

        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        lanternTarget = GetComponent<LanternTarget>();
    }

    private void FixedUpdate()
    {
        if (banished)
            return;

        CheckLeash();
        StateBehaviour();
    }

    private void CheckLeash()
    {
        switch(currentState)
        {
            case StateZombie.resting:
            case StateZombie.banished:
                return;
        }

        if (Vector3.Distance(transform.position, encounterPoint.position) > rangeLeash)
        {
            ChangeState(StateZombie.resting);
            transform.position = encounterPoint.position;
        }
    }

    private void StateBehaviour()
    {
        /*
        if (lanternTarget.lanternProgress >= lanternTarget.targetThreshold)
        {
            ChangeState(StateZombie.banished);
            banished = true;
            return;
        }

        if (lanternTarget.lanternProgress > 0)
        {
            ChangeState(StateZombie.banishing);
            return;        
        }
        */

        switch (currentState)
        {
            case StateZombie.resting:
                if (Vector3.Distance(encounterPoint.position, GameManager.PlayerInstance.transform.position) <= rangeDetection)
                    ChangeState(StateZombie.chasing);
                break;

            case StateZombie.chasing:
                if (Vector3.Distance(transform.position, GameManager.PlayerInstance.transform.position) <= rangeAttack)
                    ChangeState(StateZombie.attacking);
                else
                    controller.transform.position = Vector3.MoveTowards(transform.position,GameManager.PlayerInstance.transform.position, moveSpeed * Time.deltaTime);
                break;

            case StateZombie.attacking:
                if (Vector3.Distance(transform.position, GameManager.PlayerInstance.transform.position) > rangeAttack)
                    ChangeState(StateZombie.chasing);
                break;

            case StateZombie.banishing:
                break;

            case StateZombie.banished:
                break;
        }
    }

    public void ChangeState(StateZombie state)
    {
        switch (state)
        {
            case StateZombie.resting:
                animator.SetTrigger("resting");
                animator.SetBool("chasing", false);
                animator.SetBool("attacking", false);
                animator.SetBool("banishing", false);
                lanternTarget.lanternProgress = 0;
                break;
            case StateZombie.chasing:
                animator.SetBool("chasing", true);
                animator.SetBool("attacking", false);
                animator.SetBool("banishing", false);
                break;
            case StateZombie.attacking:
                animator.SetBool("attacking", true);
                break;
            case StateZombie.banishing:
                animator.SetBool("banishing", true);
                break;
            case StateZombie.banished:
                animator.SetTrigger("banished");
                break;
        }

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
