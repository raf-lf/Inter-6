using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IslandManager : MonoBehaviour
{
    [Header("Components")] [SerializeField]
    private GameObject player;

    [SerializeField] private float islandSpeed;

    [SerializeField] private Transform pointEntry;
    [SerializeField] private Transform pointExit;
    [SerializeField] private SceneTransition transition;

    private void Start()
    {
        //StartCoroutine(MovePlayer(deckPosition, false));

        GameManager.CanvasManager.AnimateOverlay(OverlayAnimation.Black, 0);
        GameManager.CanvasManager.AnimateOverlay(OverlayAnimation.Off, 1);

        StopAllCoroutines();
        StartCoroutine(PlayerEntryExit(pointEntry, false));
    }

    public void LeaveIsland()
    {
        StopAllCoroutines();
        StartCoroutine(PlayerEntryExit(pointExit, true));
    }

    IEnumerator PlayerEntryExit(Transform position, bool leaving)
    {
        GameManager.PlayerControl = false;

        while (player.transform.position != position.position)
        {
            player.transform.position =
                Vector3.MoveTowards(player.transform.position, position.position, islandSpeed * 0.01f);
            yield return null;
        }

        GameManager.PlayerControl = true;

        if (leaving)
            transition.StartSceneTransition();
    }
}