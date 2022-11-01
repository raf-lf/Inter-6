using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public GameData GameDataRef;
    public ScriptableManager ScriptableManagerRef;

    public static string currentIsland;
    private void Awake()
    {
        GameManager.GameplayManager = this;
        GameManager.GameData = GameDataRef;
        GameManager.ScriptableManager = ScriptableManagerRef;
        GameManager.ScriptableManager.PopulateLists();
    }

    private void Start()
    {
        SetPlayerStartPosition();
    }

    private void SetPlayerStartPosition()
    {
        foreach (var item in FindObjectsOfType<Interactable_Island>())
        {
            if (item.islandConnected == currentIsland)
            {
                GameManager.PlayerInstance.transform.position = item.islandExit.position;
                GameManager.PlayerInstance.transform.rotation = item.islandExit.rotation;
            }
        }

        if (currentIsland == null)
            return;

    }
}
