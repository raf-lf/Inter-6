using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Island : MonoBehaviour
{
    [SerializeField] private KeyCode interactionKey;
    [SerializeField] private Animator interactibleFeedbackAnimator;
    [SerializeField] private bool canUse;
    [SerializeField] private bool currentlyUsing;
    public Transform islandExit;
    public string islandConnected;


    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("Player") == false)
            return;
        
        if (!GameManager.PlayerControl || currentlyUsing)
            ToggleInteraction(false);
        else
            ToggleInteraction(true);
        
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player") == false)
            return;
        
        ToggleInteraction(false);
    }

    private void ToggleInteraction(bool active)
    {
        canUse = active;
        interactibleFeedbackAnimator.SetBool("active", active);
    }
    
    private void Update()
    {
        if (canUse && Input.GetKeyDown(interactionKey))
            Interact();
    }
    
    private void Interact()
    {
        currentlyUsing = true;
        
        //SHOULD ADD THE POSSIBILITY FOR FLAG CHECKING IN THE FUTURE
        
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
