using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSlow : MonoBehaviour
{
    [SerializeField] float movementSlow;
    private Movement playerMovement;

    private void Start()
    {
        playerMovement = GameManager.PlayerInstance.movement;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        playerMovement.movementSlowdown = Mathf.Clamp(movementSlow, .1f, 1);


    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;
        playerMovement.movementSlowdown = 0;
    }
}
