using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVfx : MonoBehaviour
{
    [SerializeField] public Animator boatAnimator;
    [SerializeField] public Animator ferrymanAnimator;
    [SerializeField] private ParticleSystem vfxDamageLow;
    [SerializeField] private ParticleSystem vfxDamageHigh;
    [SerializeField] private ParticleSystem vfxImmune;

    FMOD.Studio.EventInstance collisionSFX;




    private void Start()
    {
        collisionSFX = PlayerSfx.collisionEvent; 


    }

    public void AnimateLantern(bool active)
    {

        ferrymanAnimator.SetBool("lantern", active);
    }

    public void VfxInvulnerability()
    {
        boatAnimator.SetTrigger("damageLow");
        vfxImmune.Play();

        collisionSFX.setParameterByName("rpm", 0);
        collisionSFX.start();
    }

    public void VfxDamageLow()
    {
        ferrymanAnimator.SetTrigger("damageLow");
        boatAnimator.SetTrigger("damageLow");
        vfxDamageLow.Play();

        collisionSFX.setParameterByName("rpm", 0);
        collisionSFX.start();
    }

    public void VfxDamageHigh()
    { 
        ferrymanAnimator.SetTrigger("damageLow");
        boatAnimator.SetTrigger("damageHigh");
        vfxDamageHigh.Play();

        collisionSFX.setParameterByName("rpm", 1.6f);
        collisionSFX.start();
    }

}
