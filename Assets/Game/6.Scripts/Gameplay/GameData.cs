using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Scriptables/Game Data")]
public class GameData : ScriptableObject
{
    [Header("Player")]
    public float currentHp;
    [HideInInspector] public float maxHp;
    public float startingHp;
    public float hpBoostPerMilk;
    public float invulnerabilitySeconds;
    public float currentGas;
    public float maxGas;
    public InventoryItem milkItem;

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


    public void ResetValues()
    {
        maxHp = startingHp;
        currentHp = startingHp;
        currentGas = 0;

        UpdateHpBoosts();
    }

    public void UpdateHpBoosts()
    {
        maxHp = startingHp + (hpBoostPerMilk * milkItem.quantity);

        if(GameManager.PlayerInstance)
            GameManager.PlayerInstance.atributes.HpChange(maxHp);

    }
}
