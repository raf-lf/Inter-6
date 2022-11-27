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

[Serializable]
public class MenuScreen
{
    public CanvasGroup canvasGroup;
    public Button buttonBack;
    public CinemachineVirtualCameraBase camera;
    public bool hideTitle;

}
public class MainMenu : MonoBehaviour
{
    [SerializeField] private MenuScreen[] screens = new MenuScreen[0];
    [SerializeField] private CanvasGroup titleCG;
    [SerializeField] private MenuScreen lastScreen;
    
    public Button buttonNewGame;
    public Button buttonLoadGame;
    public Button buttonConfig;
    public Button buttonCredits;
    public Button buttonExit;

    private bool pastTitle;
    private float startTargetTime;

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

    public void OpenScreen(MenuScreen screen)
    {
        foreach (var item in screens)
        {
            if (item.camera != null)
                item.camera.enabled = screen == item ? true : false;

            item.canvasGroup.interactable = screen == item ? true : false;

            item.canvasGroup.blocksRaycasts = screen == item ? true : false;

            float endValue = screen == item ? 1 : 0;

            item.canvasGroup.DOFade(endValue, .5f);

        }

        titleCG.DOFade(screen.hideTitle ? 0 : 1, .5f);

    }

    public void CloseScreens()
    {
        foreach (var item in screens)
        {
            item.camera.enabled = false;

            item.canvasGroup.interactable = false;

            item.canvasGroup.blocksRaycasts = false;

            item.canvasGroup.DOFade(0, .5f);

        }
    }
    public void GoBack()
    {
        RuntimeManager.PlayOneShot("event:/UI/back");
        OpenScreen(screens[1]);
    }

    private void Start()
    {
        startTargetTime = Time.time + 5;

        buttonNewGame.onClick.AddListener(delegate
        {
            RuntimeManager.PlayOneShot("event:/UI/enter");
            CloseScreens();
                StartCoroutine(GameSequence());
        });

        buttonConfig.onClick.AddListener( delegate 
        {
            RuntimeManager.PlayOneShot("event:/UI/click"); 
            OpenScreen(screens[2]); 
        });

        buttonCredits.onClick.AddListener( delegate 
        {
            RuntimeManager.PlayOneShot("event:/UI/click"); 
            OpenScreen(screens[3]); 
        });

        buttonExit.onClick.AddListener(delegate
        {
            RuntimeManager.PlayOneShot("event:/UI/back");
            CloseScreens();
            StartCoroutine(ExitSequence());
        });

        foreach (var item in screens)
        {
            if(item.buttonBack != null)
                item.buttonBack.onClick.AddListener(delegate { GoBack(); });
        }
        sfxVCA = RuntimeManager.GetVCA("vca:/SFX");
        bGVCA = RuntimeManager.GetVCA("vca:/MUSICA");
        soundScapeVCA = RuntimeManager.GetVCA("vca:/SOUNDSCAPE");

        sfxSlider.onValueChanged.AddListener(delegate { sfxVCA.setVolume(DecibelToLinear(sfxSlider.value)); });
        mSlider.onValueChanged.AddListener(delegate { bGVCA.setVolume(DecibelToLinear(mSlider.value)); });
        mSlider.onValueChanged.AddListener(delegate { soundScapeVCA.setVolume(DecibelToLinear(mSlider.value)); });

        StartCoroutine(StartSequence());
    
    }

    private IEnumerator StartSequence()
    {
        foreach (var item in screens)
        {
            item.canvasGroup.alpha = 0;
            item.canvasGroup.interactable = false;
        }

        titleCG.alpha = 0;
        GameManager.CanvasManager.AnimateOverlay(OverlayAnimation.Black, 0);
        yield return new WaitForSeconds(1);
        GameManager.CanvasManager.AnimateOverlay(OverlayAnimation.Off, 2);
        yield return new WaitForSeconds(2);
        titleCG.DOFade(1, 2);
        yield return new WaitForSeconds(2);
        OpenScreen(screens[0]);
    }

    private IEnumerator GameSequence()
    {
        RuntimeManager.PlayOneShot("event:/UI/enter");
        GameManager.CanvasManager.AnimateOverlay(OverlayAnimation.Black, 2);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("Tutorial");

    }

    private IEnumerator ExitSequence()
    {
        RuntimeManager.PlayOneShot("event:/UI/back");
        GameManager.CanvasManager.AnimateOverlay(OverlayAnimation.Black, 2);
        yield return new WaitForSeconds(2);
        Application.Quit();

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
        if (Input.anyKeyDown && !pastTitle && Time.time >= startTargetTime)
        {
            RuntimeManager.PlayOneShot("event:/UI/click");
            OpenScreen(screens[1]);
            pastTitle = true;
        }

    }

}
