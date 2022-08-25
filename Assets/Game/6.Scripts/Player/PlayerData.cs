using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData PlayerInstance;

    private void Awake()
    {
        PlayerInstance = GetComponent<PlayerData>();
    }
}
