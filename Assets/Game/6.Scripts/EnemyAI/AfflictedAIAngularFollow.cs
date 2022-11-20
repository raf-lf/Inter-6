using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action__;

public class AfflictedAIAngularFollow : MonoBehaviour, IEnemy
{
    private Enemy entity;

    [SerializeField] private float moveSpeed;



    //public Transform Target;
    new Vector3 targetPosition;

    public float CircleRadius = 1;

    public float RotationSpeed = 1;

    public float ElevationOffset = 0;

    [Range(0, 360)]
    public float StartAngle = 0;

    public bool LookAtTarget = false;

    private float angle;
    private float sign = 1;

    private void Awake()
    {
        angle = StartAngle;
    }


    private ActionStatus status;
    private BehaviourState classState = BehaviourState.Chase;

    // Start is called before the first frame update
    void Start()
    {
        entity = GetComponent<Enemy>();
        entity.EnemyActions += AIActionExecuting;
    }

    void OnDestroy()
    {
        entity.EnemyActions -= AIActionExecuting;
    }

    public void AIActionExecuting(BehaviourState state)
    {
        if(state == BehaviourState.Attack && entity.canAttack)
        {
            Vector3 position = GameManager.PlayerInstance.transform.position;
            Vector3 positionOffset = GeneratePositionOffset(angle);
            targetPosition = position + positionOffset;
            entity.EnemyHolder.transform.position = Vector3.Lerp(entity.EnemyHolder.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            entity.EnemyHolder.transform.rotation = Quaternion.LookRotation(position - entity.EnemyHolder.transform.position, GameManager.PlayerInstance == null ? Vector3.up : GameManager.PlayerInstance.transform.up);
        }
        if (state == classState || state == BehaviourState.Attack && !entity.isAttacking && entity.isPreparingAttack && !entity.canAttack)
        {
            RotationMovement();
        }
        else
        {
            ResetChaseAnimation();
            if (entity.isTakingDamage || entity.canAttack || entity.isAttacking)
            {
                return;
            }
            if (Vector3.Distance(entity.EnemyHolder.transform.position, GameManager.PlayerInstance.transform.position) <= entity.rangeDetection)
            {
                Debug.Log("oi");
                entity.ChangeState(status, classState);
                SetChaseAnimation();
            }
        }
    }

    public void RotationMovement(){
        RaycastHit hit;

        Vector3 position = GameManager.PlayerInstance.transform.position;
        Vector3 positionOffset = GeneratePositionOffset(angle);
        targetPosition = position + positionOffset;

        //if (Physics.BoxCast(transform.position, new Vector3(2, 1, 2), transform.right, transform.rotation, 4) && Mathf.Sign(sign) > 0)
        //{
        //    sign *= -1;
        //}
        //else if (Physics.BoxCast(transform.position, new Vector3(2, 1, 2), -transform.right, transform.rotation, 4) && Mathf.Sign(sign) < 0)
        //{
        //    sign *= -1;
        //}

        if (Physics.Raycast(entity.EnemyHolder.transform.position, entity.EnemyHolder.transform.right, 4) && Mathf.Sign(sign) > 0)
        {
            sign *= -1;
        }
        else if(Physics.Raycast(entity.EnemyHolder.transform.position, -entity.EnemyHolder.transform.right, 4) && Mathf.Sign(sign) < 0)
        {
            sign *= -1;
        }

        entity.EnemyHolder.transform.position = Vector3.Lerp(entity.EnemyHolder.transform.position, targetPosition, moveSpeed * Time.deltaTime);
        entity.EnemyHolder.transform.rotation = Quaternion.LookRotation(position - entity.EnemyHolder.transform.position, GameManager.PlayerInstance == null ? Vector3.up : GameManager.PlayerInstance.transform.up);

        angle += Time.deltaTime* RotationSpeed * sign;
    }

    private void ResetChaseAnimation()
    {
        entity.SetAnimationBool("isChasing", false);
    }

    private void SetChaseAnimation()
    {
        entity.SetAnimationBool("isChasing", true);
    }

    private Vector3 GeneratePositionOffset(float a)
    {
        a *= Mathf.Deg2Rad;

        Vector3 positionOffset = new Vector3(
            Mathf.Cos(a) * CircleRadius,
            ElevationOffset,
            Mathf.Sin(a) * CircleRadius
        );
        return positionOffset;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //if(entity.EnemyHolder != null)
        //Gizmos.DrawLine(entity.EnemyHolder.transform.position, targetPosition);
    }
}
