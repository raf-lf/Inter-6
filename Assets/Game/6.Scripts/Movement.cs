using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotSpeed;
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
        float rotateMove = rotSpeed * Input.GetAxis("Horizontal");
        transform.Rotate(new Vector3(0, rotateMove * Time.deltaTime, 0));

        float movement = speed * Input.GetAxis("Vertical") * Time.deltaTime;
        controller.Move(movement * transform.forward);

    }
}
