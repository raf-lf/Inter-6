using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fog : MonoBehaviour
{
    private enum FogType { obscuring, ethereal}
    [SerializeField] private FogType fogType;
    [SerializeField] private float fovModifier;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            GameManager.PlayerInstance.onFogModifier++;
            
        CheckFog();

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            GameManager.PlayerInstance.onFogModifier--;

        CheckFog();
    }

    private void CheckFog()
    {
        if(GameManager.PlayerInstance.onFogModifier > 0)
        {
            GameManager.CameraManager.CameraCloseUp(fovModifier);

            switch(fogType)
            {
                case FogType.obscuring:
                    GameManager.PlayerInstance.lanternBlocked = true;
                    GameManager.CameraManager.ChangeOverlayEffect(OverlayEffectType.Fog);
                    break;
                case FogType.ethereal:
                    GameManager.CameraManager.ChangeOverlayEffect(OverlayEffectType.Ethereal);
                    break;
            }
        }
        else
        {
            GameManager.PlayerInstance.lanternBlocked = false;
            GameManager.CameraManager.CameraCloseUp(1);

            GameManager.CameraManager.ChangeOverlayEffect(OverlayEffectType.None);
        }

    }
}
