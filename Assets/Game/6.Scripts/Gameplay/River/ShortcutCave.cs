using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ShortcutCave : MonoBehaviour
{
    [SerializeField] private ShortcutCave connectedShortcut;
    [SerializeField] private Transform exitPoint1;
    [SerializeField] private Transform exitPoint2;
    public bool shortCutOpen;
    public bool useable = true;
    private Animator animator;
    [SerializeField] private ParticleSystem vfxDestruction;
    [SerializeField] private CinemachineVirtualCamera focusCamera;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if(shortCutOpen)
            animator.SetBool("open", true);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player") || !shortCutOpen || !useable)
            return;

        StopAllCoroutines();
        StartCoroutine(TransitionSequence());

    }
    private IEnumerator MovePlayer(Transform position)
    {
        while (Vector3.Distance(GameManager.PlayerInstance.transform.position, position.position) > 0f)
        {
            GameManager.PlayerInstance.movement.MoveTo(position);
            yield return new WaitForEndOfFrame();
        }

    }

    private IEnumerator TransitionSequence()
    {
        GameManager.CameraManager.EnableDisableBaseCams(false);
        //GameManager.DialogueSystem.CinematicMode(true);
        useable = false;
        connectedShortcut.useable = false;
        GameManager.PlayerControl = false;

        StartCoroutine(MovePlayer(exitPoint1));

        GameManager.CanvasManager.AnimateOverlay(OverlayAnimation.Black, .5f);
        yield return new WaitForSeconds(.5f);
        GameManager.CameraManager.brain.m_DefaultBlend.m_Time = 0;
        connectedShortcut.focusCamera.enabled = true;
        GameManager.CameraManager.brain.m_DefaultBlend.m_Time = GameManager.CameraManager.standardBlendTime;
        StopCoroutine(MovePlayer(exitPoint1));
        GameManager.PlayerInstance.transform.position = connectedShortcut.exitPoint1.position;
        GameManager.PlayerInstance.transform.rotation = connectedShortcut.exitPoint1.rotation;
        yield return new WaitForSeconds(.5f);
        GameManager.CanvasManager.AnimateOverlay(OverlayAnimation.Off, .5f);

        if (!connectedShortcut.shortCutOpen)
            connectedShortcut.BreakOpenShortcut();

        yield return MovePlayer(connectedShortcut.exitPoint2);

        useable = true;
        connectedShortcut.useable = true;
        GameManager.PlayerControl = true;
        //GameManager.DialogueSystem.CinematicMode(false);
        connectedShortcut.focusCamera.enabled = false;
        GameManager.CameraManager.EnableDisableBaseCams(true);

    }

    public void BreakOpenShortcut()
    {
        Saveable saveable = GetComponent<Saveable>();
        if (saveable != null)
            saveable.Save(true);

        vfxDestruction.Play();
        OpenShortcut();
    }

    public void OpenShortcut()
    {
        shortCutOpen = true;
        animator.SetBool("open", true);
    }
}
