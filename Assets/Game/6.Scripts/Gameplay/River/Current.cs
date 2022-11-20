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
        if (other.gameObject.GetComponent<CharacterController>())
            MoveWithCurrent(other.gameObject.GetComponent<CharacterController>());
            
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
