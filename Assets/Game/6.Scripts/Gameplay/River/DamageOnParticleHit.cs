using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnParticleHit : DamageOnContact
{
    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    protected override void OnTriggerStay(Collider other)
    {

    }

    private void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

        Rigidbody rb = other.GetComponent<Rigidbody>();
        int i = 0;

        if (other.gameObject.GetComponentInChildren<PlayerAtributes>())
        {
            other.gameObject.GetComponentInChildren<PlayerAtributes>().Damage(damage, id);
        }
    }
}


       
