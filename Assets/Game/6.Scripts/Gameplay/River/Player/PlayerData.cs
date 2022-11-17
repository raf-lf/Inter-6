using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [Header("Flags")]
    public bool lanternBlocked;
    public int onFogModifier;
    public bool movementSlowed;
    public float extraMovementModifier;
    public static bool buffResistance;
    public static bool buffEfficiency;
    public static bool buffStealth;

    [Header("Scripts")]
    public Transform cameraTargetPlayer;
    public Transform cameraTargetLantern;
    [HideInInspector] public PlayerAtributes atributes;
    [HideInInspector] public Movement movement;
    [HideInInspector] public PlayerSfx playerSfx;
    [HideInInspector] public PlayerVfx playerVfx;
    private EnergyDashing energyDashing;
    private Lantern lantern;


    private void Awake()
    {
        GameManager.PlayerInstance = GetComponent<PlayerData>();

        atributes = gameObject.GetComponent<PlayerAtributes>();
        movement = gameObject.GetComponent<Movement>();
        playerSfx = gameObject.GetComponent<PlayerSfx>();
        playerVfx = gameObject.GetComponent<PlayerVfx>();
        energyDashing = gameObject.GetComponent<EnergyDashing>();
        lantern = gameObject.GetComponentInChildren<Lantern>();
    }

    public float GetSpeedModifier()
    {
        float modifier = 1;

        modifier += energyDashing.dashing ? GameManager.GameData.multiplierDashing : 0;
        modifier += lantern.usingLantern? GameManager.GameData.multiplierUsingLantern : 0;
      //  modifier += movementSlowed? GameManager.GameData.multiplierUsingLantern : 0;

        return modifier;
    }
}
