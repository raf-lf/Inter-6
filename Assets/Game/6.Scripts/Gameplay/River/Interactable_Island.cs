using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interactable_Island : Interactable
{
    public Transform islandExit;
    public string islandConnected;

    public override void Interact()
    {
        base.Interact();
        StartSceneTransition();
        GameplayManager.currentIsland = islandConnected;
    }

    public void StartSceneTransition()
    {
        StartCoroutine(Transition());

    }

    IEnumerator Transition()
    {
        GameManager.CanvasManager.AnimateOverlay(OverlayAnimation.Black, 1);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(islandConnected, LoadSceneMode.Single);

    }
}
