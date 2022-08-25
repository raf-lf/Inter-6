using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameData GameDataRef;
    public static GameData GameData;
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
        GameData = GameDataRef;
    }
}
