using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using FMODUnity;

public class MainMenu : MonoBehaviour
{
    int stage = 0;
    public TextMeshProUGUI pressAnyKey;
    public Button playBt;
    public Button setBt;
    public Button creditsBt;

    public GameObject settingsWindow;
    public GameObject creditsWindow;


    FMOD.Studio.VCA sfxVCA;
    FMOD.Studio.VCA bGVCA;
    FMOD.Studio.VCA soundScapeVCA;


    public Slider sfxSlider;
    public Slider mSlider;

    [SerializeField][Range(-80f,10f)]
    private float sfxVolume;

    [SerializeField][Range(-80f,10f)]
    private float backGroundVolume;

    private void Start()
    {


        playBt.onClick.AddListener(delegate { GoToGame();} );
        setBt.onClick.AddListener(delegate { ChangeStage(1); });
        creditsBt.onClick.AddListener(delegate { ChangeStage(2); });
        OpenButtons(false);
        settingsWindow.SetActive(false);
        creditsWindow.SetActive(false);

        sfxVCA = RuntimeManager.GetVCA("vca:/SFX");
        bGVCA = RuntimeManager.GetVCA("vca:/MUSICA");
        soundScapeVCA = RuntimeManager.GetVCA("vca:/SOUNDSCAPE");

        sfxSlider.onValueChanged.AddListener(delegate { sfxVCA.setVolume(DecibelToLinear(sfxSlider.value)); });
        mSlider.onValueChanged.AddListener(delegate { bGVCA.setVolume(DecibelToLinear(mSlider.value)); });
        mSlider.onValueChanged.AddListener(delegate { soundScapeVCA.setVolume(DecibelToLinear(mSlider.value)); });
    
    }


    private void Update()
    {
        if(stage == 0) 
        {
            if (Input.anyKeyDown) ChangeStage(1);
        }

      


    }

    float DecibelToLinear (float db) {return Mathf.Pow(10.0f, db / 20f);}


    public void ChangeSfxVolume() 
    {
        sfxVCA.setVolume(DecibelToLinear(sfxSlider.value));
    
    }
    public void ChangeMVolume() 
    {
        bGVCA.setVolume(DecibelToLinear(mSlider.value));
        soundScapeVCA.setVolume(DecibelToLinear(mSlider.value));
    
    
    } 




    void GoToGame() {SceneManager.LoadScene("Tutorial");}


    public void ChangeStage(int x) 
    {
        if(stage == 0) pressAnyKey.gameObject.SetActive(false);
        stage += x;
        
    
        if(stage == 1) 
        {
            OpenButtons(true);


        }
        else if (stage == 2) 
        {
            settingsWindow.SetActive(true);
            OpenButtons(false);
        


        }
        else if (stage == 3)
        {
            creditsWindow.SetActive(true);
            OpenButtons(false);

        }
                
                
    }

    void OpenButtons(bool open) 
    {
        playBt.gameObject.SetActive(open);
        setBt.gameObject.SetActive(open);
        creditsBt.gameObject.SetActive(open);

        if(open == true) 
        {
            settingsWindow.SetActive(false);
            creditsWindow.SetActive(false);

        }

    }
   


}
