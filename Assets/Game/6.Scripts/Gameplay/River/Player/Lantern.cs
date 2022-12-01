using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Lantern : MonoBehaviour
{
    [SerializeField] private float lanternPower;
    [SerializeField] private float lerpTransitionSpeed;
    [SerializeField] private Animator lanternAnimator;
    [HideInInspector] public bool usingLantern;
    [SerializeField] private Renderer boatRenderer;

    [Header("AoE")]
    [SerializeField] private int AoeRepetitions;
    [SerializeField] private float AoeDistanceIncrement;
    [SerializeField] private float AoeSizeIncrement;
    [SerializeField] private List<LanternTarget> targetsAffected = new List<LanternTarget>();

    [Header("Components")]
    [SerializeField] private StudioEventEmitter sfxLantern;

    private void UseLantern(bool active)
    {
        usingLantern = active;

        if (active)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation,Camera.main.transform.rotation,lerpTransitionSpeed);
            GameManager.CameraManager.SwitchCamera(CameraType.Lantern);
            lanternAnimator.SetBool("focus", true);
            CheckLanternArea();

            if(!sfxLantern.IsPlaying())
                sfxLantern.Play();
        
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, transform.parent.rotation, lerpTransitionSpeed);
            GameManager.CameraManager.SwitchCamera(CameraType.Ship);
            lanternAnimator.SetBool("focus", false);

            if (sfxLantern.IsPlaying())
                sfxLantern.Stop();
        
        }

        boatRenderer.material.DOFloat(active ? 1.5f : 0, "_MaskRadius",.5f);
    }

    private void CheckLanternArea()
    {
        targetsAffected.Clear();

        for (int i = AoeRepetitions; i > 0 ; i--)
        {
            RaycastHit[] hit = Physics.SphereCastAll(transform.position + transform.forward * (i * AoeDistanceIncrement), i * AoeSizeIncrement, Vector3.forward) ;

            foreach (var item in hit)
            {
                if (item.collider.gameObject.GetComponentInChildren<LanternTarget>())
                {
                 
                    LanternTarget target = item.collider.gameObject.GetComponentInChildren<LanternTarget>();
                    if(!targetsAffected.Contains(target))
                        targetsAffected.Add(target);
                }
            }
        }
                   

        foreach (var item in targetsAffected)
        {
            item.LanternGain(lanternPower);
        }
        
      
    
    
    }

    void Update()
    {
        if (!GameManager.PlayerControl || GameManager.PlayerInstance.lanternBlocked)
        {
            UseLantern(false);
            return;
        }

        if (Input.GetMouseButton(1) || Input.GetKey(KeyCode.Space))
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, .8f, .6f, .1f);

        for (int i = AoeRepetitions; i > 0; i--)
        {
            Gizmos.DrawSphere(transform.position + transform.forward * (i * AoeDistanceIncrement), i * AoeSizeIncrement);
        }
    }
}
