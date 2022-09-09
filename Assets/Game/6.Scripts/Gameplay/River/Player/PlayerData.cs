using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{

    private void Awake()
    {
        GameManager.PlayerInstance = GetComponent<PlayerData>();
    }
}
