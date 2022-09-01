using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour
{
    [SerializeField] private float lerpTransitionSpeed;
    [SerializeField] private Animator lanternAnimator;

    private void UseLantern(bool active)
    {
        if (active)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation,Camera.main.transform.rotation,lerpTransitionSpeed);
            GameManager.CameraManager.SwitchCamera(CameraType.Lantern);
            lanternAnimator.SetBool("focus", true);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, transform.parent.rotation, lerpTransitionSpeed);
            GameManager.CameraManager.SwitchCamera(CameraType.Ship);
            lanternAnimator.SetBool("focus", false);
        }
    }

    private void CheckLanternArea()
    {

    }

    void Update()
    {
        if (Input.GetMouseButton(1))
            UseLantern(true);
        else
            UseLantern(false);

        
        /*
        RaycastHit hit;

        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit);

        Vector3 target = hit.point;

        transform.LookAt(target);
        */
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, .8f, .6f, .1f);
        Gizmos.DrawSphere(transform.position + transform.forward * 0, .25f);
        Gizmos.DrawSphere(transform.position + transform.forward * 1, .5f);
        Gizmos.DrawSphere(transform.position + transform.forward * 2, .75f);
        Gizmos.DrawSphere(transform.position + transform.forward * 3,  1f);
        Gizmos.DrawSphere(transform.position + transform.forward * 4,  1.25f);
        Gizmos.DrawSphere(transform.position + transform.forward * 5,  1.5f);
        Gizmos.DrawSphere(transform.position + transform.forward * 6,  1.75f);
        Gizmos.DrawSphere(transform.position + transform.forward * 7,  2f);
    }
}
