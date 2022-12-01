using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using FMODUnity;
using DG.Tweening;
using Cinemachine;

public class OptionMenu : MonoBehaviour
{
    private bool menuOpen;

    [SerializeField] private CanvasGroup cgMaster;
    [SerializeField] private CanvasGroup cgOptions;
    [SerializeField] private CanvasGroup cgLeave;

    [SerializeField] private Button buttonBack;
    [SerializeField] private Button buttonExit;
    [SerializeField] private Button buttonExitModalYes;
    [SerializeField] private Button buttonExitModalNo;

    [Header("Audio")]
    FMOD.Studio.VCA sfxVCA;
    FMOD.Studio.VCA bGVCA;
    FMOD.Studio.VCA soundScapeVCA;

    public Slider sfxSlider;
    public Slider mSlider;

    [SerializeField][Range(-80f,10f)]
    private float sfxVolume;

    [SerializeField][Range(-80f,10f)]
    private float backGroundVolume;

    float DecibelToLinear(float db) { return Mathf.Pow(10.0f, db / 20f); }

    private void Start()
    {
        buttonBack.onClick.AddListener(delegate
        {
            RuntimeManager.PlayOneShot("event:/UI/back");
            OpenCloseMenu();
        });

        buttonExit.onClick.AddListener(delegate
        {
            RuntimeManager.PlayOneShot("event:/UI/click");
            OpenCloseModal(cgOptions, false);
            OpenCloseModal(cgLeave, true);
        });

        buttonExitModalYes.onClick.AddListener( delegate
        {
            RuntimeManager.PlayOneShot("event:/UI/enter");

            StartCoroutine(LeaveSequence());
        });

        buttonExitModalNo.onClick.AddListener( delegate 
        {
            RuntimeManager.PlayOneShot("event:/UI/back");
            OpenCloseModal(cgLeave, false);
            OpenCloseModal(cgOptions, true);
        });

        sfxVCA = RuntimeManager.GetVCA("vca:/SFX");
        bGVCA = RuntimeManager.GetVCA("vca:/MUSICA");
        soundScapeVCA = RuntimeManager.GetVCA("vca:/SOUNDSCAPE");

        sfxSlider.onValueChanged.AddListener(delegate { sfxVCA.setVolume(DecibelToLinear(sfxSlider.value)); });
        mSlider.onValueChanged.AddListener(delegate { bGVCA.setVolume(DecibelToLinear(mSlider.value)); });
        mSlider.onValueChanged.AddListener(delegate { soundScapeVCA.setVolume(DecibelToLinear(mSlider.value)); });
            
    }

    private void OpenCloseMenu()
    {
        menuOpen = !menuOpen;

        OpenCloseModal(cgMaster, menuOpen);

        Time.timeScale = menuOpen ? 0 : 1;

        if (menuOpen)
        {
            OpenCloseModal(cgOptions, true);
            OpenCloseModal(cgLeave, false);
            RuntimeManager.PlayOneShot("event:/UI/click");
        }
        else
        {
            RuntimeManager.PlayOneShot("event:/UI/back");
        }

    }

    private void OpenCloseModal(CanvasGroup cg, bool open)
    {
        cg.interactable = open;

        cg.blocksRaycasts = open;

        cg.DOFade(open? 1 : 0, .5f).SetUpdate(true);
    }

    private IEnumerator LeaveSequence()
    {
        OpenCloseModal(cgMaster, false);

        PlayerSfx.engineEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        GameManager.CanvasManager.AnimateOverlay(OverlayAnimation.Black, 2);
        yield return new WaitForSecondsRealtime(2);


        Time.timeScale = 1;
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }


    public void ChangeSfxVolume() 
    {
        sfxVCA.setVolume(DecibelToLinear(sfxSlider.value));
    
    }
    public void ChangeMVolume() 
    {
        bGVCA.setVolume(DecibelToLinear(mSlider.value));
        soundScapeVCA.setVolume(DecibelToLinear(mSlider.value));
    
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.PlayerControl)
        {
            OpenCloseMenu();
        }

    }

}
