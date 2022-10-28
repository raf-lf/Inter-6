using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    int stage = 0;
    public TextMeshProUGUI pressAnyKey;
    public Button playBt;
    public Button setBt;
    public Button creditsBt;

    public GameObject settingsWindow;
    public GameObject creditsWindow;


    private void Start()
    {


        playBt.onClick.AddListener(delegate { GoToGame();} );
        setBt.onClick.AddListener(delegate { ChangeStage(1); });
        creditsBt.onClick.AddListener(delegate { ChangeStage(2); });
        OpenButtons(false);
        settingsWindow.SetActive(false);
        creditsWindow.SetActive(false);

    }


    private void Update()
    {
        if(stage == 0) 
        {
            if (Input.anyKeyDown) ChangeStage(1);
        }

    }

    void GoToGame() {SceneManager.LoadScene("Tutorial");}


    void ChangeStage(int x) 
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
        else if (stage == 2)
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
