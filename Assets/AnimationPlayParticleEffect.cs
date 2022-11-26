using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPlayParticleEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem particleSystem;

    public void PlayParticleSystem()
    {
        particleSystem.Play();
    }
    public void StopParticleSystem()
    {
        particleSystem.Stop();
    }
}
