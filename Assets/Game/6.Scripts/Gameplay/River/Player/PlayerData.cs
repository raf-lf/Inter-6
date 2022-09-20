using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] public PlayerAtributes atributes;
    [SerializeField] public Movement movement;
    private EnergyDashing energyDashing;
    private Lantern lantern;

    private void Awake()
    {
        GameManager.PlayerInstance = GetComponent<PlayerData>();
        atributes = gameObject.GetComponent<PlayerAtributes>();
        movement = gameObject.GetComponent<Movement>();
        energyDashing = gameObject.GetComponent<EnergyDashing>();
        lantern = gameObject.GetComponentInChildren<Lantern>();
    }

    public float GetSpeedModifier()
    {
        float modifier = 1;

        modifier += energyDashing.dashing ? GameManager.GameData.multiplierDashing : 0;
        modifier += lantern.usingLantern? GameManager.GameData.multiplierUsingLantern : 0;

        return modifier;
    }
}
