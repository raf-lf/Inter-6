using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public float velocity;
    public CharacterController controller;
    public LayerMask collisionMask;
    public float collisionRadius;
    private List<RaycastHit> hits = new List<RaycastHit>();
    public GameObject dmg;
    public PlayerHp playerHp;


    private void Update()
    {
        velocity = controller.velocity.magnitude;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        if(velocity > GameManager.GameData.velocityHigh)
        {
            playerHp.Damage(GameManager.GameData.damageHighVelocity, -1);

            var thing = Instantiate(dmg);
            thing.transform.position = hit.point;
        }
        else if(velocity > GameManager.GameData.velocityLow)
        {

            playerHp.Damage(GameManager.GameData.damageLowVelocity, -1);

            var thing = Instantiate(dmg);
            thing.transform.position = hit.point;
        }
    }

}
