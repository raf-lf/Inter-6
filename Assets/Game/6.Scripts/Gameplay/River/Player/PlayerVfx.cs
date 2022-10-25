using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVfx : MonoBehaviour
{
    [SerializeField] public Animator boatAnimator;
    [SerializeField] private ParticleSystem vfxDamageLow;
    [SerializeField] private ParticleSystem vfxDamageHigh;

    FMOD.Studio.EventInstance collisionSFX;
         
    private void Start()
    {
        collisionSFX = GameManager.PlayerInstance.playerSfx.collisionEvent;
    }

    public void VfxDamageLow()
    {
        boatAnimator.SetTrigger("damageLow");
        vfxDamageLow.Play();

        collisionSFX.setParameterByName("rpm", 0);
        collisionSFX.start();
    }

    public void VfxDamageHigh()
    { 
        boatAnimator.SetTrigger("damageHigh");
        vfxDamageHigh.Play();

        collisionSFX.setParameterByName("rpm", 1.6f);
        collisionSFX.start();
    }

}
