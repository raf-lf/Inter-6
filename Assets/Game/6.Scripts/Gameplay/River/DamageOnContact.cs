using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnContact : MonoBehaviour
{
    public int damage;
    public bool randomizeId = true;
    public int id;

    private void Awake()
    {
        if(randomizeId)
            id = Random.Range(0, 1000);
    }

    protected virtual void OnTriggerStay(Collider other)
    {
        if(other.gameObject.GetComponentInChildren<PlayerAtributes>())
        {
            other.gameObject.GetComponentInChildren<PlayerAtributes>().Damage(damage,id);
        }
    }

}
