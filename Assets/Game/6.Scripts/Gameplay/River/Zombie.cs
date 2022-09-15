using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField] private float rangeDetection;
    [SerializeField] private float rangeLeash;
    [SerializeField] private float moveSpeed;
    private Vector3 startPosition;

    private enum BehaviorZombie { resting, chasing,  }

    private void Awake()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        
    }

    private void CheckLeash()
    {
        if(Vector3.Distance(transform.position, startPosition) > rangeLeash)
    }

}
