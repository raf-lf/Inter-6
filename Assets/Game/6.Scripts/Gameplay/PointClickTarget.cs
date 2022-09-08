using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointClickTarget : MonoBehaviour
{
    public bool mouseOver;
    public ParticleSystem vfxMouseOver;
    public ParticleSystem vfxMouseClick;
    private ParticleSystem.EmissionModule mouseOverEm;

    private void Awake()
    {
        mouseOverEm = vfxMouseOver.emission;
    }
    public virtual void Click()
    {
        vfxMouseClick.Play();
    }

    private void MouseOver(bool active)
    {
        mouseOver = active;
        mouseOverEm.enabled = active;
    }

}
