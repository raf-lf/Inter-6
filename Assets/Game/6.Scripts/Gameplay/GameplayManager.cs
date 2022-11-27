using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        ConfigureCursor();
        SetPlayerStartPosition();
        Invoke(nameof(DelayedClearOverlay), 1);
    }


    public void ConfigureCursor()
    {
        //Cursor.lockState = CursorLockMode.Confined;

        if (FindObjectOfType<IslandManager>() || SceneManager.GetActiveScene().name == "Menu")
            Cursor.visible = true;
        else
            Cursor.visible = false;

    }
    private void SetPlayerStartPosition()
    {
        if (!FindObjectOfType<Interactable_Island>())
            return;

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
    private void DelayedClearOverlay()
    {
        //After 1s has passed, this clears the overlay if the player has control, which doesn't happen if you're in a cutscene.
        if (GameManager.PlayerControl)
            GameManager.CanvasManager.AnimateOverlay(OverlayAnimation.Off, 1);
    }

}
