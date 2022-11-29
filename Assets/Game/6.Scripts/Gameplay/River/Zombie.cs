using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

[RequireComponent(typeof(LanternTarget))]
public class Zombie : MonoBehaviour
{
    [Tooltip("If encounter point is NOT selected, zombie will define start position on self.")]
    [SerializeField] private Transform encounterPoint;
    [SerializeField] private float rangeDetection;
    [SerializeField] private float rangeLeash;
    [SerializeField] private float rangeAttack;
    [SerializeField] private float moveSpeed;
    private Vector3 startPosition;
    private Animator animator;
    private CharacterController controller;
    private LanternTarget lanternTarget;
    private bool banished;

    FMOD.Studio.EventInstance soundsFX;

    public enum StateZombie { resting, chasing, attacking, banishing, banished  }
    public StateZombie currentState;

    private void Awake()
    {
        startPosition = transform.position;

        if (encounterPoint == null)
        {
            var point = new GameObject();
            point.transform.position = transform.position;
            point.name = gameObject.name + " encounterPoint";
            encounterPoint = point.transform;
        }


        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        lanternTarget = GetComponent<LanternTarget>();
        soundsFX = RuntimeManager.CreateInstance("event:/SFX/MOB/zombies");


        if (PlayerData.buffStealth)
            rangeDetection /= 2;
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
            SoundManager.hazardNumber--;
            if (SoundManager.hazardNumber == 0) SoundManager.HazardOnOff(false);
            transform.position = startPosition;
        }
    }

    private void StateBehaviour()
    {
        if (currentState != StateZombie.banished)
        {
            if (lanternTarget.lanternProgress >= lanternTarget.targetThreshold)
            {
                ChangeState(StateZombie.banished);
                banished = true;
               
                SoundManager.hazardNumber--;
                if(SoundManager.hazardNumber == 0)  SoundManager.HazardOnOff(false);
                
                return;
            }

            if (lanternTarget.lanternProgress > 0)
            {
                ChangeState(StateZombie.banishing);
                return;
            }
        }

        switch (currentState)
        {
            case StateZombie.resting:
                if (Vector3.Distance(encounterPoint.position, GameManager.PlayerInstance.transform.position) <= rangeDetection) 
                {
                    SoundManager.hazardNumber++;
                    SoundManager.HazardOnOff(true);
                    ChangeState(StateZombie.chasing);
                
                }
                break;

            case StateZombie.chasing:
                transform.position = Vector3.MoveTowards(transform.position, GameManager.PlayerInstance.transform.position, moveSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, GameManager.PlayerInstance.transform.position) <= rangeAttack)
                    ChangeState(StateZombie.attacking);
                break;

            case StateZombie.attacking:
                transform.position = Vector3.MoveTowards(transform.position, GameManager.PlayerInstance.transform.position, moveSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, GameManager.PlayerInstance.transform.position) > rangeAttack)
                    ChangeState(StateZombie.chasing);
                break;

            case StateZombie.banishing:
                if (lanternTarget.lanternProgress <= 0)
                {
                    ChangeState(StateZombie.chasing);
                }
                break;

        }
    }

    public void ChangeState(StateZombie state)
    {
        switch (state)
        {
            case StateZombie.resting:
                animator.SetBool("chasing", false);
                animator.SetBool("attacking", false);
                animator.SetBool("banishing", false);
                animator.SetTrigger("resting");
                lanternTarget.lanternProgress = 0;
                soundsFX.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                break;
            case StateZombie.chasing:
                SoundManager.soundTrackEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                soundsFX.start();
                animator.SetBool("chasing", true);
                animator.SetBool("attacking", false);
                animator.SetBool("banishing", false);

                break;
            case StateZombie.attacking:
                soundsFX.setParameterByName("attack",1);
                animator.SetBool("attacking", true);
                
                break;
            case StateZombie.banishing:
                animator.SetBool("banishing", true);
                break;
            case StateZombie.banished:
                animator.SetBool("banishing", false);
                animator.SetTrigger("banished");
                break;
        }

        currentState = state;

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
