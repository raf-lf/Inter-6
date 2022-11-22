using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Scriptables/Game Data")]
public class GameData : ScriptableObject
{
    [Header("Player")]
    public float currentHp;
    public float maxHp;
    public float invulnerabilitySeconds;
    public float currentGas;
    public float maxGas;

    [Header("Player Movement")]
    public float moveSpeed;
    public float multiplierUsingLantern;
    public float multiplierDashing;
    public float turnSpeed;

    [Header("Collision")]
    public float velocityLow;
    public float velocityHigh;
    public int damageLowVelocity;
    public int damageHighVelocity;
    public int damageCommomHit;

    public void ResetValues()
    {
        currentHp = maxHp;
        currentGas = maxGas;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }
}
