using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Current : MonoBehaviour
{
    [SerializeField] private float currentSpeed;
    [SerializeField] private float speedModifier;
    [SerializeField] private Renderer strip;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(-other.transform.forward * ((currentSpeed* speedModifier)*0.4f), ForceMode.Acceleration);
        }
    }

    private void MoveWithCurrent(CharacterController controller)
    {
        controller.Move(transform.forward * currentSpeed * Time.deltaTime);
    }

    private void Update()
    {
        strip.material.SetFloat("_Speed", currentSpeed/ speedModifier);
    }
}
