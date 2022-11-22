using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDashing : MonoBehaviour
{
    [SerializeField] private KeyCode dashKey;
    [SerializeField] private float dashAttemptBuffer = 1;
    private float dashAttemptBufferTargetTime;
    [HideInInspector] public bool dashing;
    [SerializeField] private ParticleSystem[] pfxDash = new ParticleSystem[2];
    [SerializeField] private ParticleSystem.EmissionModule[] pfxDashEm;

    private void Awake()
    {
        pfxDashEm = new ParticleSystem.EmissionModule[pfxDash.Length];
        for (int i = 0; i < pfxDash.Length; i++)
        {
            pfxDashEm[i] = pfxDash[i].emission;
        }
    }

    private void Update()
    {
        TryDash();
    }

    private void TryDash()
    {
        if (Input.GetKey(dashKey) && GameManager.PlayerInstance.movement.GetSpeed() > 0)
        {
            if (Time.time < dashAttemptBufferTargetTime)
            {
                dashing = false;
            }
            else if (GameManager.GameData.currentGas > 0)
            {
                dashing = true;

                if(PlayerData.buffEfficiency)
                    GameManager.PlayerInstance.atributes.EnergyChange(-Time.deltaTime/2);
                else
                    GameManager.PlayerInstance.atributes.EnergyChange(-Time.deltaTime);
            }
            else
            {
                dashing = false;
                dashAttemptBufferTargetTime = Time.time + dashAttemptBuffer;
            }
        }
        else
            dashing = false;


        for (int i = 0; i < pfxDashEm.Length; i++)
        {
            pfxDashEm[i].enabled = dashing;
        }



    }
}
