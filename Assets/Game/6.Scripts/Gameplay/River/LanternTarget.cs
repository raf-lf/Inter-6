using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using FMOD.Studio;
using FMODUnity;

public enum LanternTargetType
{
    soul, spiritLock, hiddenItem, afflicted
}

public class LanternTarget : MonoBehaviour
{
    public LanternTargetType targetType;
    public float lanternProgress;
    public float targetThreshold;
    [SerializeField] protected float lanternRecovery;
    public bool lanternImmune;
    [SerializeField] private float lanternBuffer;
    [SerializeField] protected ParticleSystem vfxBurn;
    [SerializeField] protected ParticleSystem vfxDeath;
    [SerializeField] private Renderer renderer;
    [SerializeField] private StudioEventEmitter sfxEmitter;

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

        if (!sfxEmitter)
            return;

        if (!sfxEmitter.IsPlaying())
            sfxEmitter.Play();
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

    public virtual void LanternEffect()
    {
        lanternImmune = true;
        if (vfxDeath)
            vfxDeath.Play();

        switch (targetType)
        {
            case LanternTargetType.soul:
                if(renderer)
                    renderer.material.DOFloat(1,"_Fade", 1);
                break;
            case LanternTargetType.spiritLock:
                GetComponent<SpiritLock>().OpenLock(false);
                break;

            case LanternTargetType.hiddenItem:
                RuntimeManager.PlayOneShot("event:/SFX/PERSONAGEM/item_pickup");
                break;            

            case LanternTargetType.afflicted:
                break;

        }

        RuntimeManager.PlayOneShot("event:/SFX/MOB/burst");

    }

    private void UpdateBurnProgressVfx()
    {
        if(renderer)
            renderer.material.SetFloat("_Burn", lanternProgress / targetThreshold);
    }

    protected virtual void Update()
    {
        LanternLoss();
        UpdateBurnProgressVfx();

        if (!sfxEmitter)
            return;

        if (sfxEmitter.IsPlaying())
        {
            if(!lanternImmune)
                sfxEmitter.SetParameter("luz", lanternProgress / targetThreshold);

            if (lanternProgress <= 0 || lanternImmune)
                sfxEmitter.Stop();
        }

    }

    public void ResetProgress()
    {
        lanternProgress = 0;
    }
}
