using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum LanternTargetType
{
    soul, dredge, hiddenItem
}

public class LanternTarget : MonoBehaviour
{
    public LanternTargetType targetType;
    public float lanternProgress;
    public float targetThreshold;
    [SerializeField] private float lanternRecovery;
    [SerializeField] private bool lanternImmune;
    [SerializeField] private float lanternBuffer;
    [SerializeField] private ParticleSystem vfxBurn;
    [SerializeField] private ParticleSystem vfxDeath;


    public void LanternGain(float effect)
    {
        if (lanternImmune)
            return;

        lanternBuffer = 1;
        vfxBurn.Play();
        lanternProgress = Mathf.Clamp(lanternProgress + effect * Time.deltaTime, 0, targetThreshold);

        if(lanternProgress >= targetThreshold)
        {
            LanternEffect();
        }
    }

    private void LanternLoss()
    {
        if (lanternBuffer > 0)
        {
            lanternBuffer = Mathf.Clamp(lanternBuffer - Time.deltaTime, 0, Mathf.Infinity);
            return;
        }

        if (lanternProgress > 0)
        {
            vfxBurn.Stop();
            lanternProgress = Mathf.Clamp(lanternProgress - (lanternRecovery * Time.deltaTime), 0, targetThreshold);
            return;
        }
    }

    public void LanternEffect()
    {
        lanternImmune = true;

        switch (targetType)
        {
            case LanternTargetType.soul:
                vfxDeath.Play();
                GetComponent<Renderer>().material.DOFloat(1,"_Fade", 1);

                break;
            case LanternTargetType.dredge:
                break;
            case LanternTargetType.hiddenItem:
                break;

        }

    }

    private void UpdateBurnProgressVfx()
    {
        GetComponent<Renderer>().material.SetFloat("_Burn", lanternProgress / targetThreshold);
    }

    private void Update()
    {
        LanternLoss();
        UpdateBurnProgressVfx();
    }

}
