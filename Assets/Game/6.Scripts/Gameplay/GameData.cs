using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Scriptables/Game Data")]
public class GameData : ScriptableObject
{
    [Header("Player")]
    public int currentHp;
    public int playerHp;
    public int iFrames;
    public int currentGas;
    public int maxGas;

    public float moveSpeed;
    public float turnSpeed;

    [Header("Collision")]
    public float velocityLow;
    public float velocityHigh;
    public int damageLowVelocity;
    public int damageHighVelocity;
}
