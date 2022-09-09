using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasFillControl : MonoBehaviour
{
    [SerializeField] private Material GasFillMaterial;

    private void Update()
    {
        float ratio = (float)GameManager.GameData.currentGas / (float)GameManager.GameData.maxGas;
        
        GasFillMaterial.SetFloat("_Fill", ratio);
    }
}
