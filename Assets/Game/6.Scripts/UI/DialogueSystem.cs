using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class DialogueSystem : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public CanvasGroup canvasGroupBlinds;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Image portraitImage;
    public Button clickOverlay;

    private CE_Dialogue currentDialogue;
    private int currentDialogueIndex;

    private bool lineEnded;

    private void Awake()
    {
        GameManager.DialogueSystem = this;
        canvasGroup.alpha = 0;
    }
    public void CinematicMode(bool active)
    {
        float endValue = active ? 1 : 0;

        canvasGroupBlinds.DOFade(endValue, .5f);

    }

    public void StartDialogue(CE_Dialogue dialogue)
    {
        canvasGroup.DOFade(1, .33f);
        clickOverlay.interactable = true;
        currentDialogue = dialogue;
        currentDialogueIndex = 0;
        PlayDialogue();
    }

    public void PlayDialogue()
    {
        if (currentDialogueIndex >= currentDialogue.scenes.Length)
        {
            EndDialogue();
            return;
        }

        PlayLine(currentDialogue.scenes[currentDialogueIndex].actor, currentDialogue.scenes[currentDialogueIndex].emotion, currentDialogue.scenes[currentDialogueIndex].line);
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
    }

    public void EndDialogue()
    {
        clickOverlay.interactable = false;

        currentDialogue.NextEvent();
        currentDialogue = null;

        canvasGroup.DOFade(0, .33f);
    }

    public void PlayLine(ActorData actor, ActorEmotion emotion, string line)
    {
        nameText.text = actor.ReturnName();
        portraitImage.sprite = actor.ReturnPortrait(emotion);
        dialogueText.text = line;
    }
}
