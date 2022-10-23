using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IslandManager : MonoBehaviour
{
    [Header("Components")]

    [SerializeField] private float islandSpeed;

    [SerializeField] private Transform pointEntry;
    [SerializeField] private Transform pointExit;
    [SerializeField] private SceneTransition transition;
    public CanvasIslandManager canvasIslandManager;
    public static IslandManager CurrentIslandManager;

    private void Awake()
    {
        CurrentIslandManager = this;
        canvasIslandManager = GetComponentInChildren<CanvasIslandManager>();
    }

    private void Start()
    {
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
        yield return new WaitForEndOfFrame();
        GameManager.PlayerControl = false;
        canvasIslandManager.UpdateAuxiliaryButtons();
        GameManager.CameraManager.brain.m_DefaultBlend.m_Time = 0;
        GameManager.CameraManager.FocusCamera(canvasIslandManager.clickableAnchor.focusCamera);
        while (GameManager.PlayerInstance.transform.position != position.position)
        {
            GameManager.PlayerInstance.transform.position =
                Vector3.MoveTowards(GameManager.PlayerInstance.transform.position, position.position, islandSpeed * Time.deltaTime);
            yield return null;
        }

        GameManager.PlayerControl = true;

        if (leaving)
            transition.StartSceneTransition();
        else
        {
            yield return new WaitForEndOfFrame();
            GameManager.CameraManager.brain.m_DefaultBlend.m_Time = 2;
            GameManager.CameraManager.ReturnCamera();
            yield return new WaitForEndOfFrame();
            GameManager.CameraManager.brain.m_DefaultBlend.m_Time = GameManager.CameraManager.standardBlendTime;
            canvasIslandManager.UpdateAuxiliaryButtons();

        }

    }
}