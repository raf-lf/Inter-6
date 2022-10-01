using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyOnContact : MonoBehaviour
{
    public float energy;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponentInChildren<PlayerAtributes>())
        {
            other.gameObject.GetComponentInChildren<PlayerAtributes>().EnergyChange(energy * Time.deltaTime);
        }
    }
}
