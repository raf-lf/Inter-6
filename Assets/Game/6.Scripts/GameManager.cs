using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Language { portuguese, english}

public static class GameManager
{
    public static GameData GameData;
    public static bool PlayerControl = true;
    public static bool PlayerClickControl = true;

    public static ScriptableManager ScriptableManager;

    public static Language gameLanguage = Language.english;

    [Header("Singletons")]
    public static PlayerData PlayerInstance;
    public static CameraManager CameraManager;
    public static DialogueSystem DialogueSystem;
    public static CanvasManager CanvasManager;
    public static Hud Hud;
    public static TaskManager TaskManager;
    public static GameplayManager GameplayManager;
    public static GameObject soundTrackManager;
    public static CombatState CombatState;

}
