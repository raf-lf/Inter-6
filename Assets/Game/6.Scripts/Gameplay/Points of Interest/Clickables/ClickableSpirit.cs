using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableSpirit : PointClickTarget
{
    [Header("Spirit")] 
    [SerializeField] private Spirit spirit;

    [SerializeField]
    private ParticleSystem vfxActive;
    [SerializeField]
    private ParticleSystem vfxCollect;

    private void Awake()
    {
        if(spirit.found)
            Destroy(gameObject);
        
    }
    
    public override void Click()
    {
        base.Click();
        spirit.FindSpirit();
        vfxActive.Stop();
        vfxCollect.Play();
        renderer.enabled = false;
        interactable = false;

    }
}
