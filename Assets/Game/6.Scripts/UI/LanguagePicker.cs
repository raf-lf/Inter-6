using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using FMODUnity;
using UnityEngine.SceneManagement;

public class LanguagePicker : MonoBehaviour
{
    [SerializeField] private CanvasGroup cg;
    Sequence seq;

    private void Start()
    {
        FadeIn();
    }

    public void FadeIn()
    {
        seq = DOTween.Sequence();
        seq.Insert(0, cg.DOFade(0, 0));
        seq.Insert(2, cg.DOFade(1, 1f));
        seq.Play();
        cg.interactable = true;
    }

    public void SelectLanguage(int index)
    {
        switch(index)
        {
            default:
            case 0:
                GameManager.gameLanguage = Language.portuguese; 
                break;
            case 1:
                GameManager.gameLanguage = Language.english; 
                break;
        }

        RuntimeManager.PlayOneShot("event:/UI/click");
        seq.Kill();
        seq.Insert(0,cg.DOFade(0, 1f));
        cg.interactable = false;
        Invoke(nameof(NextScene),2);

    }
    public void NextScene()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}
