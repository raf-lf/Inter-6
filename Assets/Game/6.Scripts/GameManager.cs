using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameData GameDataRef;
    public static GameData GameData;
    public static GameManager Instance;
    public static bool PlayerControl = true;
    public static bool PlayerClickControl = true;

    [Header("Singletons")]
    public static PlayerData PlayerInstance;
    public static CameraManager CameraManager;
    public static DialogueSystem DialogueSystem;
    public static CanvasManager CanvasManager;

    private void Awake()
    {
        Instance = this;
        GameData = GameDataRef;
    }
}
