using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rigidbody;

    public float yPosition = 0f;
    GameData playerAtributes;

    [Header("SpeedAtributes")]
    private float acceleration = 0;
    public float movementSlowdown;
    float actualMaxSpeed = 240f;
    [SerializeField]
    float actualSpeed = 0;


    float inputVertical;
    float inputHorizontal;



    float horizontalAngle;
    float rotationAngle;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        acceleration = GameManager.GameData.GetMoveSpeed();
        playerAtributes = GameManager.GameData;
    }

    void FixedUpdate()
    {
        if (!GameManager.PlayerControl)
            return;
        rigidbody.angularVelocity = new Vector3(0, rigidbody.angularVelocity.y, rigidbody.angularVelocity.z);
        rigidbody.AddForce(transform.forward * actualSpeed * Time.fixedDeltaTime, ForceMode.Acceleration);
        RotationUpdate();
    }
    void Update()
    {
        if (!GameManager.PlayerControl)
            return;
#if UNITY_EDITOR
        GameManager.GameData.currentGas = 100;
#endif
        CheckInput();
        MovementUpdate();
        transform.position = new Vector3 (transform.position.x, Mathf.MoveTowards(transform.position.y, yPosition, 100*Time.deltaTime), transform.position.z);
        //rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0, rigidbody.angularVelocity.z);
    }

    public void ResetSpeed()
    {
        actualSpeed = actualSpeed*0.33f;
    }

    public float GetSpeed()
    {
        return actualSpeed;
    }

    void CheckInput()
    {
        inputHorizontal = Input.GetAxis("Horizontal");
        inputVertical = Mathf.Clamp(Input.GetAxis("Vertical"), -0.5f, 1f);
    }

    bool CanApplyMovement()
    {
        if (inputVertical != 0)
            return true;
        else
            return false;
    }    

    bool CanApplyRotation()
    {
        if (inputHorizontal != 0)
            return true;
        else
            return false;
    }

    void MovementUpdate()
    {
        if (!CanApplyMovement() && actualSpeed != 0)
            StopMovement();
        else if (CanApplyMovement())
        {
            SpeedUpdate();
        }

    }

    public void MoveTo(Transform destination)
    {
        transform.position = Vector3.MoveTowards(transform.position, destination.position, GameManager.GameData.moveSpeed * Time.deltaTime);
    }

    void SpeedUpdate()
    {
        float speedModifier = GameManager.PlayerInstance.GetSpeedModifier();
        actualMaxSpeed = GameManager.PlayerInstance.IsDashing() ? playerAtributes.maxCommonSpeed + playerAtributes.dashMaxSpeedModfier : playerAtributes.maxCommonSpeed;
        actualSpeed += actualSpeed > 0 && inputVertical < 0 || actualSpeed < 0 && inputVertical > 0  ? acceleration * playerAtributes.gripForce * speedModifier * inputVertical * Time.deltaTime : acceleration * speedModifier * playerAtributes.speedForce * inputVertical * Time.deltaTime;
        actualSpeed *= (1 - movementSlowdown);
        actualSpeed = Mathf.Clamp(actualSpeed, playerAtributes.minSpeed, actualMaxSpeed);
    }

    void StopMovement()
    {
        actualSpeed = Mathf.MoveTowards(actualSpeed, 0, Time.deltaTime * playerAtributes.gripForce);
    }

    void NormalizeSlopeAngle()
    {
        rotationAngle = Mathf.MoveTowards(rotationAngle, 0, Time.deltaTime * playerAtributes.rotationSpeed);
        transform.localRotation = Quaternion.Euler(0f, horizontalAngle * acceleration, rotationAngle);
    }

    void RotationUpdate()
    {
        if (!CanApplyRotation())
        {
            NormalizeSlopeAngle();
        }
        else if (CanApplyRotation() && actualSpeed != 0)
        {
            RotationAngleUpdate();
            SlopeAngleUpdate();
        }
        transform.localRotation = Quaternion.Euler(0f, horizontalAngle * acceleration, rotationAngle);
    }

    void SlopeAngleUpdate()
    {
        rotationAngle += playerAtributes.slopeSpeed * inputHorizontal * Time.deltaTime;
        rotationAngle = Mathf.Clamp(rotationAngle, -15 , 15);
    }
    void RotationAngleUpdate()
    {
        horizontalAngle += playerAtributes.rotationSpeed * inputHorizontal * Time.deltaTime;
    }

}