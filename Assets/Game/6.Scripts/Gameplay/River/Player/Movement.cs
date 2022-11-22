using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rigidbody;

    public float yPosition = 0f;

    [Header("SpeedAtributes")]
    private float acceleration = 0;
    public float movementSlowdown;
    [SerializeField]
    float actualSpeed = 0;
    [SerializeField]
    float speedForce = 0;
    [SerializeField]
    float actualMaxSpeed = 240f;
    [SerializeField]
    float maxCommonSpeed = 240f;
    [SerializeField]
    float dashMaxSpeedModfier = 40f;    
    [SerializeField]
    float minSpeed = -120f;

    [Header("InputAtributes")]
    [SerializeField]
    float inputVertical;
    [SerializeField]
    float inputHorizontal;

    [Header("RotationAtributes")]
    [SerializeField]
    float rotationSpeed = 10;
    [SerializeField]
    float gripForce;
    [SerializeField]
    float horizontalAngle;
    [SerializeField]
    float rotationAngle;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        acceleration = GameManager.GameData.GetMoveSpeed();
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
        transform.position = new Vector3 (transform.position.x, Mathf.MoveTowards(transform.position.y, yPosition, Time.deltaTime), transform.position.z);
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
        actualMaxSpeed = GameManager.PlayerInstance.IsDashing() ? maxCommonSpeed + dashMaxSpeedModfier : maxCommonSpeed;
        actualSpeed += actualSpeed > 0 && inputVertical < 0 || actualSpeed < 0 && inputVertical > 0  ? acceleration * gripForce * speedModifier * inputVertical * Time.deltaTime : acceleration * speedModifier * speedForce * inputVertical * Time.deltaTime;
        actualSpeed *= (1 - movementSlowdown);
        actualSpeed = Mathf.Clamp(actualSpeed, minSpeed, actualMaxSpeed);
    }

    void StopMovement()
    {
        actualSpeed = Mathf.MoveTowards(actualSpeed, 0, Time.deltaTime * 30);
    }

    void NormalizeSlopeAngle()
    {
        rotationAngle = Mathf.MoveTowards(rotationAngle, 0, Time.deltaTime * rotationSpeed);
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
        rotationAngle += rotationSpeed * inputHorizontal * Time.deltaTime;
        rotationAngle = Mathf.Clamp(rotationAngle, -15 , 15);
    }
    void RotationAngleUpdate()
    {
        horizontalAngle += 20 * inputHorizontal * Time.deltaTime;
    }

}