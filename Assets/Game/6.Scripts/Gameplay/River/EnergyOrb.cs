using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class EnergyOrb : MonoBehaviour
{
    [SerializeField] private int energyGain;
    [SerializeField] private float seekDistance;
    private float currentSpeed;
    [SerializeField] private float baseSpeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float seekingInterval = 5;
    //[SerializeField] private bool seeking;
    [SerializeField] private bool chasing;
    [SerializeField] private bool collected;
    [SerializeField] private float chaseStartTime;
    [SerializeField] private StudioEventEmitter sfxCollect;

    private void SeekPlayer()
    {
        //if (Time.frameCount % seekingInterval == 0)
        //{
            if (Vector3.Distance(GameManager.PlayerInstance.transform.position, transform.position) <= seekDistance)
                ChasePlayer();
            else
                SlowDown();
       // }
    }
    private void ChasePlayer()
    {
        if (!chasing)
        {
            chaseStartTime = Time.time;
            chasing = true;
        }

        currentSpeed = Mathf.Clamp(baseSpeed + acceleration * Time.deltaTime * (Time.time - chaseStartTime) , 0, Mathf.Infinity);
        transform.position = Vector3.MoveTowards(transform.position, GameManager.PlayerInstance.transform.position,currentSpeed * Time.deltaTime);
    }

    private void SlowDown()
    {
        if (chasing)
            chasing = false;

        currentSpeed = Mathf.Clamp(baseSpeed - acceleration * Time.deltaTime * (Time.time - chaseStartTime), 0, Mathf.Infinity);
        

        if(currentSpeed > 0)
            transform.position = Vector3.MoveTowards(transform.position, GameManager.PlayerInstance.transform.position, currentSpeed * Time.deltaTime);

    }

    private void AttemptCollection()
    {
        if (Vector3.Distance(GameManager.PlayerInstance.transform.position, transform.position) <= .5f)
        {
            GameManager.PlayerInstance.atributes.EnergyChange(energyGain);
            GetComponent<Animator>().SetTrigger("collect");
            collected = true;
            sfxCollect.Play();

            Destroy(gameObject,5);
        }
    }

    private void FixedUpdate()
    {
        if (collected)
            return;

        SeekPlayer();
        AttemptCollection();

    }

}
