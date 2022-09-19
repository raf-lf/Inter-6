using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisPatrol : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform[] waypoints = new Transform[0];
    [SerializeField] private float waypointTolerance;
    private int currentWaypointIndex;

    void Update()
    {
        Patrol();
    }

    private void OnDrawGizmosSelected()
    {
        if (waypoints != null)
        {
            Gizmos.color = Color.yellow;
            for (int i = 0; i < waypoints.Length; i++)
            {
                Gizmos.DrawWireCube(waypoints[i].position, .3f * Vector3.one);

                if (i == waypoints.Length - 1)
                    Gizmos.DrawLine(waypoints[i].position, waypoints[0].position);
                else
                    Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
            }
        }
    }

    private void Patrol()
    {
        if(Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) <= waypointTolerance)
        {
            if(currentWaypointIndex == waypoints.Length - 1)
                currentWaypointIndex = 0;
            else
                currentWaypointIndex++;
        }
        else
            transform.position = Vector3.MoveTowards(transform.position,waypoints[currentWaypointIndex].position, moveSpeed * Time.deltaTime);

    }
}
