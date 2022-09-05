using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private CharacterController controller;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (!GameManager.PlayerControl)
            return;

        float rotateMove = GameManager.GameData.turnSpeed * Input.GetAxis("Horizontal");
        transform.Rotate(new Vector3(0, rotateMove * Time.deltaTime, 0));

        float movement = GameManager.GameData.moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
        controller.Move(movement * transform.forward);

    }
}
