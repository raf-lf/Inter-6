using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PointClickTarget : MonoBehaviour
{
    [SerializeField] private bool mouseOver;
    [SerializeField] private ParticleSystem vfxMouseOver;
    [SerializeField] private ParticleSystem vfxMouseClick;
    [SerializeField] private ParticleSystem.EmissionModule mouseOverEm;
    [SerializeField] private Renderer renderer;

    private void Awake()
    {
        mouseOverEm = vfxMouseOver.emission;
    }

    private void Update()
    {
        if (mouseOver && GameManager.PlayerControl && Input.GetMouseButtonDown(0))
            Click();
    }

    public virtual void Click()
    {
        vfxMouseClick.Play();
    }

    private void OnMouseEnter()
    {
        MouseOver(true);
    }
    
    private void OnMouseExit()
    {
        MouseOver(false);
    }
    
    private void MouseOver(bool active)
    {
        if (!GameManager.PlayerControl)
            return;
        
        mouseOver = active;
        
        float value = active ? .5f : 0f;
        renderer.material.SetFloat("_BlinkIntensity", value);
    }
    
}
