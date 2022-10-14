using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private CharacterController controller;
    private EnergyDashing energyDashing;
    public float movement;
    public float movementSlowdown;


    [Header("SfxRPM")]
    public float defaultRPM;
    public float maxRPM;
     float engineSfxRpm;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        energyDashing = GetComponent<EnergyDashing>();
        engineSfxRpm = defaultRPM;
    }

    private void Update()
    {
        Move();

        if(transform.position.y > 0)
            controller.transform.position = new Vector3(transform.position.x,0,transform.position.z);
    }

    private void Move()
    {
        if (!GameManager.PlayerControl) 
        {
            GameManager.PlayerInstance.playerSfx.engineEvent.setParameterByName("rpm", defaultRPM);
            return;
        }

        float rotateMove = GameManager.GameData.turnSpeed * Input.GetAxis("Horizontal");
        transform.Rotate(new Vector3(0, rotateMove * Time.deltaTime, 0));

        movement = 0;

        movement = GameManager.GameData.moveSpeed * GameManager.PlayerInstance.GetSpeedModifier() * Input.GetAxis("Vertical") * Time.deltaTime;
        movement *= 1 - movementSlowdown;

        controller.Move(movement * transform.forward);

        if (movement == 0)
        {
            if (engineSfxRpm > defaultRPM) engineSfxRpm = Mathf.MoveTowards(engineSfxRpm, defaultRPM, 2);
        }
        else
        {
            if (engineSfxRpm < maxRPM) engineSfxRpm = Mathf.MoveTowards(engineSfxRpm, maxRPM, 2);
        }
        GameManager.PlayerInstance.playerSfx.engineEvent.setParameterByName("rpm",engineSfxRpm);

    }
}
