using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour
{
    [SerializeField] private float lanternPower;
    [SerializeField] private float lerpTransitionSpeed;
    [SerializeField] private Animator lanternAnimator;
    [HideInInspector] public bool usingLantern;

    [Header("AoE")]
    [SerializeField] private int AoeRepetitions;
    [SerializeField] private float AoeDistanceIncrement;
    [SerializeField] private float AoeSizeIncrement;
    [SerializeField] private List<LanternTarget> targetsAffected = new List<LanternTarget>();

    FMOD.Studio.EventInstance lanterLightSfx;

    private void Start()
    {
        lanterLightSfx = GameManager.PlayerInstance.playerSfx.lanternEvent;
    }

    private void UseLantern(bool active)
    {
        usingLantern = active;

        if (active)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation,Camera.main.transform.rotation,lerpTransitionSpeed);
            GameManager.CameraManager.SwitchCamera(CameraType.Lantern);
            lanternAnimator.SetBool("focus", true);
            CheckLanternArea();
            lanterLightSfx.start();
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, transform.parent.rotation, lerpTransitionSpeed);
            GameManager.CameraManager.SwitchCamera(CameraType.Ship);
            lanternAnimator.SetBool("focus", false);
            lanterLightSfx.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        }
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
                    lanterLightSfx.setParameterByName("alma", 1);
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
        
        if(targetsAffected.Count == 0) 
        {
            lanterLightSfx.setParameterByName("alma", 0);
        }
    
    
    }

    void Update()
    {
        if (!GameManager.PlayerControl || GameManager.PlayerInstance.lanternBlocked)
        {
            UseLantern(false);
            return;
        }

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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, .8f, .6f, .1f);

        for (int i = AoeRepetitions; i > 0; i--)
        {
            Gizmos.DrawSphere(transform.position + transform.forward * (i * AoeDistanceIncrement), i * AoeSizeIncrement);
        }
    }
}
