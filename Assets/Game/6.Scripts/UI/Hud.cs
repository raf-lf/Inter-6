using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    public TextMeshProUGUI hpTextCurrent ;
    public TextMeshProUGUI hpTextMax ;
    public Image energyFill;

    private void Start()
    {
        UpdateHp();
        UpdateEnergy();
    }

    private void Update()
    {
        UpdateHp();
        UpdateEnergy();
    }

    public void UpdateHp()
    {
        hpTextCurrent.text = GameManager.GameData.currentHp.ToString();
        hpTextMax.text = GameManager.GameData.playerHp.ToString();
    }
    public void UpdateEnergy()
    {
        energyFill.fillAmount = (float)GameManager.GameData.currentGas / (float)GameManager.GameData.maxGas;
    }
}
