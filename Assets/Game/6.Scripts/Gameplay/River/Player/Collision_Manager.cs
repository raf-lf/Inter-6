using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision_Manager : MonoBehaviour
{
    public float velocity;

    Rigidbody rigidbody;
    public PlayerAtributes playerHp;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        playerHp = GetComponent<PlayerAtributes>();
    }

    private void Update()
    {
        velocity = rigidbody.velocity.magnitude;

    }

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if(collision.gameObject.CompareTag("Puke"))
            return;
        bool _hittedByObject;
        if (collision.gameObject.CompareTag("DebrisPatrol"))
            _hittedByObject = true;
        else
            _hittedByObject = false;

        TakeVelocityDamage(_hittedByObject);
    }

    public void TakeDamage(int damage)
    {
        playerHp.Damage(damage, -1);
    }

    void TakeVelocityDamage(bool hittedByObject)
    {
        int damageHittenObject = velocity > GameManager.GameData.velocityHigh ? GameManager.GameData.damageHighVelocity : velocity > GameManager.GameData.damageLowVelocity ? GameManager.GameData.damageLowVelocity : 0;
        if (PlayerData.buffResistance)
            return;
        if (!hittedByObject)
        {
            playerHp.Damage(damageHittenObject, -1);
        }
        else
            playerHp.Damage(GameManager.GameData.damageCommomHit, -1);

        GameManager.PlayerInstance.movement.ResetSpeed();

    }

}
