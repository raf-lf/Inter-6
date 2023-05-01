using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using FMODUnity;

public enum PortraitPosition { left, right }

public class DialogueSystem : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public CanvasGroup canvasGroupBlinds;
    public RectTransform nameTagRect;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Image portraitLeft;
    public Image portraitRight;

    private ES_Dialogue currentDialogue;
    private int currentDialogueIndex;

    private bool lineEnded;

    private void Awake()
    {
        GameManager.DialogueSystem = this;
        canvasGroup.alpha = 0;
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Continue();
        }
    }
    public void CinematicMode(bool active)
    {
        float endValue = active ? 1 : 0;
        
        canvasGroupBlinds.DOFade(endValue, .5f).SetUpdate(true);
        canvasGroupBlinds.blocksRaycasts = active;

    }

    public void StartDialogue(ES_Dialogue dialogue)
    {
        portraitLeft.color = new Color(portraitLeft.color.r, portraitLeft.color.g, portraitLeft.color.b, 0);
        portraitRight.color = new Color(portraitRight.color.r, portraitRight.color.g, portraitRight.color.b, 0);
        canvasGroup.DOFade(1, .33f).SetUpdate(true);
        currentDialogue = dialogue;
        currentDialogueIndex = 0;
        PlayDialogue();
    }

    public void PlayDialogue()
    {
        if (currentDialogueIndex >= currentDialogue.dialogue.lines.Length)
        {
            EndDialogue();
            return;
        }

        PlayLine(currentDialogue.dialogue.lines[currentDialogueIndex].actor, 
            currentDialogue.dialogue.lines[currentDialogueIndex].emotion, 
            currentDialogue.dialogue.lines[currentDialogueIndex].RetrieveLine(),
            currentDialogue.dialogue.lines[currentDialogueIndex].portraitPosition);

        currentDialogueIndex++;

    }
    public void Continue()
    {
        if (currentDialogue == null)
            return;

        if (lineEnded)
            PlayDialogue();
        else
            lineEnded = true;

        RuntimeManager.PlayOneShot("event:/UI/hover");



    }

    public void EndDialogue()
    {
        StartCoroutine(currentDialogue.NextEvent());
        currentDialogue = null;

        canvasGroup.DOFade(0, .33f).SetUpdate(true);
    }

    public void PlayLine(ActorData actor, ActorEmotion emotion, string line, PortraitPosition position)
    {
        if(GameManager.gameLanguage == Language.portuguese) 
        {
            if (actor.ReturnPTName() == "")
            {
                nameTagRect.gameObject.SetActive(false);
                nameText.text = actor.ReturnPTName();
            }
            else
            {
                nameTagRect.gameObject.SetActive(true);
                nameText.text = actor.ReturnPTName();
            }        
        }
        else 
        {

            if (actor.ReturnEnName() == "")
            {
                nameTagRect.gameObject.SetActive(false);
                nameText.text = actor.ReturnEnName();
            }
            else
            {
                nameTagRect.gameObject.SetActive(true);
                nameText.text = actor.ReturnEnName();
            }

        }

        Image currentPortrait = null;

        switch(position)
        {
            case PortraitPosition.left:
                currentPortrait = portraitLeft;
                nameTagRect.DOAnchorMin(new Vector3(0, 0), .15f).SetUpdate(true);
                nameTagRect.DOAnchorMax(new Vector3(0, 0), .15f).SetUpdate(true);
                nameTagRect.DOPivotX(0, .15f).SetUpdate(true);
                break;
            case PortraitPosition.right:
                currentPortrait = portraitRight;
                nameTagRect.DOAnchorMin(new Vector3(1, 0), .15f).SetUpdate(true);
                nameTagRect.DOAnchorMax(new Vector3(1, 0), .15f).SetUpdate(true);
                nameTagRect.DOPivotX(1, .15f).SetUpdate(true);
                break;
        }

        currentPortrait.sprite = actor.ReturnPortrait(emotion);

        if(currentPortrait.sprite == null)
            currentPortrait.enabled = false;
        else
            currentPortrait.enabled = true;

        if (currentPortrait.color.a == 0)
            currentPortrait.DOFade(1, .3f).SetUpdate(true);

        dialogueText.text = line;
    }
}
