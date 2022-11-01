using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using DG.Tweening;
using UnityEditor.Rendering;

public class ClearAllFog : MonoBehaviour
{
    [SerializeField] private Flag fogClearedFlag;
    [SerializeField] private List<Fog> allFogs = new List<Fog>();
    [SerializeField] private List<ParticleSystem> nonGameplayFogParticles = new List<ParticleSystem>();

    private void Start()
    {
        allFogs.AddRange(FindObjectsOfType<Fog>());
        
        if(fogClearedFlag.flagActive)
            DisableFog();
            
    }

    public void ClearFog()
    {
        DOTween.To(()=> RenderSettings.fogDensity, x=> RenderSettings.fogDensity = x, .005f, 3f);
        
        foreach (var item in allFogs)
        {
            item.ClearFog(false);
        }
        foreach (var item in nonGameplayFogParticles)
        {
            item.Stop();
        }
    }

    private void DisableFog()
    {
        RenderSettings.fogDensity = 0.005f;
        
        if(allFogs.Count > 0)
        {
            foreach (var item in allFogs)
            {
                item.ClearFog(true);
            }
        }
        
        if (nonGameplayFogParticles.Count > 0)
        {
            foreach (var item in nonGameplayFogParticles)
            {
                item.gameObject.SetActive(false);
            }
        }
    }
}
