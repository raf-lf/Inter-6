using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public float velocity;
    public CharacterController controller;
    public LayerMask collisionMask;
    public float collisionRadius;
    private List<RaycastHit> hits = new List<RaycastHit>();
    public GameObject dmg;


    private void Update()
    {
      // if(Time.frameCount % 5 == 0)
       //     CheckCollision();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        velocity = controller.velocity.magnitude;

        if (velocity > 5)
        {
            //PlayerData.PlayerInstance.gameData.hp.Damage(5, -1);

            Debug.Log(hit.collider.name + " hit");
            var thing = Instantiate(dmg);
            thing.transform.position = hit.point;

        }
    }

}
