using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Collision : MonoBehaviour
{
    public float velocity;
    public CharacterController controller;
    public LayerMask collisionMask;
    private List<RaycastHit> hits = new List<RaycastHit>();
    public GameObject dmg;
    public PlayerAtributes playerHp;




    FMOD.Studio.EventInstance collisionSFX;

    private void Start()
    {
        collisionSFX = GameManager.PlayerInstance.playerSfx.collisionEvent;
    }



    private void Update()
    {
        velocity = controller.velocity.magnitude;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        if(velocity > GameManager.GameData.velocityHigh)
        {
            collisionSFX.setParameterByName("rpm", 1.6f);
            collisionSFX.start();
           
            playerHp.Damage(GameManager.GameData.damageHighVelocity, -1);
            /*
            var thing = Instantiate(dmg);
            thing.transform.position = hit.point;
            */
        }
        else if(velocity > GameManager.GameData.velocityLow)
        {

            collisionSFX.setParameterByName("rpm", 0);
            collisionSFX.start();

            playerHp.Damage(GameManager.GameData.damageLowVelocity, -1);
            /*
            var thing = Instantiate(dmg);
            thing.transform.position = hit.point;
            */
        }
    }

}
