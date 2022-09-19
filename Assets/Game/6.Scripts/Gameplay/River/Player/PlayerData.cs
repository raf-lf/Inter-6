using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public PlayerHp scriptHp;

    private void Awake()
    {
        GameManager.PlayerInstance = GetComponent<PlayerData>();
        scriptHp = gameObject.GetComponent<PlayerHp>();
    }
}
