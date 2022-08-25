using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    public Image hpFill;
    public Image gasFill;


    public void UpdateHp()
    {
        hpFill.fillAmount = (float)GameManager.GameData.currentHp / (float)GameManager.GameData.playerHp;
    }
}
