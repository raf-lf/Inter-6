using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{ [SerializeField] private KeyCode interactionKey;
    [SerializeField] private Animator interactibleFeedbackAnimator;
    [SerializeField] private bool canUse;
    [SerializeField] private bool currentlyUsing;
    
    
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
    
    public virtual void Interact()
    {
        currentlyUsing = true;
        
        //SHOULD ADD THE POSSIBILITY FOR FLAG CHECKING IN THE FUTURE
        
    }
}
