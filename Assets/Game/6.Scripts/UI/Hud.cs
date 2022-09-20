using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Hud : MonoBehaviour
{
    public TextMeshProUGUI hpTextCurrent ;
    public TextMeshProUGUI hpTextMax ;
    public Image hpFill;
    public Image energyFill;

    private void Awake()
    {
        GameManager.Hud = this;
    }

    private void Start()
    {
        UpdateHp(0);
        UpdateEnergy(0);
    }


    public void UpdateHp(float value)
    {
        hpTextCurrent.text = GameManager.GameData.currentHp.ToString();
        hpTextMax.text = GameManager.GameData.maxHp.ToString();

        hpFill.DOFillAmount((float)GameManager.GameData.currentHp / (float)GameManager.GameData.maxHp, .15f);
    }
    public void UpdateEnergy(float value)
    {
        energyFill.DOFillAmount((float)GameManager.GameData.currentGas / (float)GameManager.GameData.maxGas, .15f);
    }
}
