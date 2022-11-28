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

    private void Update()
    {
        velocity = controller.velocity.magnitude;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(PlayerData.buffResistance)
            playerHp.Damage(0, -1);
        else if (velocity > GameManager.GameData.velocityHigh)
            playerHp.Damage(GameManager.GameData.damageHighVelocity, -1);
        else if (velocity > GameManager.GameData.velocityLow)
            playerHp.Damage(GameManager.GameData.damageLowVelocity, -1);
    }

}
