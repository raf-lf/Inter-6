using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private CharacterController controller;
    private EnergyDashing energyDashing;
    public float movement;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        energyDashing = GetComponent<EnergyDashing>();
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
            return;

        float rotateMove = GameManager.GameData.turnSpeed * Input.GetAxis("Horizontal");
        transform.Rotate(new Vector3(0, rotateMove * Time.deltaTime, 0));

        movement = 0;

        movement = GameManager.GameData.moveSpeed * GameManager.PlayerInstance.GetSpeedModifier() * Input.GetAxis("Vertical") * Time.deltaTime;

        controller.Move(movement * transform.forward);
        
        

    }
}
