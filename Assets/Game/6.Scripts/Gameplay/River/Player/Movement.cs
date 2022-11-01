
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
    float sfxRPM;
    bool acelerating;
  



    private void Awake()
    {

        controller = GetComponent<CharacterController>();
        energyDashing = GetComponent<EnergyDashing>();
        PlayerSfx.engineEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(this.gameObject));
        PlayerSfx.engineEvent.start();
        PlayerSfx.engineEvent.setParameterByName("rpm",0.4f);
    }

    private void Update()
    {
        if (GameManager.PlayerControl) 
        {
                if (Input.GetAxis("Vertical") > 0)
            {
                Acelerate();
                if(energyDashing.dashing == false) sfxRPM = 1.3f;
                else sfxRPM = 1.6f;

            }
            else 
            {
        
                sfxRPM = 1;
            }
        
        
            if (Input.GetAxis("Horizontal") != 0) Yaw();
        
        }
        else   sfxRPM = 0.4f;

        
        
        
        PlayerSfx.engineEvent.setParameterByName("rpm",sfxRPM);
        if(transform.position.y != 0)   controller.transform.position = new Vector3(transform.position.x,0,transform.position.z);
    }
    public void MoveTo(Transform destination)
    {
        transform.position = Vector3.MoveTowards(transform.position, destination.position, GameManager.GameData.moveSpeed * Time.deltaTime);

    }
    private void HigherEngineRPM() 
    {
        sfxRPM = Mathf.MoveTowards(1, 1.3f, 2* Time.deltaTime);
        acelerating = true;
    }
    private void LowerEngineRPM() 
    {

        sfxRPM = Mathf.MoveTowards(1.3f, 1, 2 * Time.deltaTime);
        acelerating = false;

    }
    private void Acelerate() 
    {
       

        if(acelerating == false) 
        {
            HigherEngineRPM();
        
        }
        movement = 0;
        movement = GameManager.GameData.moveSpeed * GameManager.PlayerInstance.GetSpeedModifier() * Input.GetAxis("Vertical") * Time.deltaTime;
        movement *= 1 - movementSlowdown;
        PlayerSfx.engineEvent.setParameterByName("rpm",sfxRPM);
        controller.Move(movement * transform.forward);
     }

    private void Yaw()
    {
       
        float rotateMove = GameManager.GameData.turnSpeed * Input.GetAxis("Horizontal");
        transform.Rotate(new Vector3(0, rotateMove * Time.deltaTime, 0));


    }
}
