using UnityEngine;
using TMPro;
public class UITextChanger : MonoBehaviour
{
    public TextMeshProUGUI[] texts;
    public TextHolder textHolderScriptableObject;


    private void Awake()
    {
        if(GameManager.gameLanguage == Language.portuguese)
        {
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].text = textHolderScriptableObject.pt[i];
            }
        }
        else if(GameManager.gameLanguage == Language.english) 
        {
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].text = textHolderScriptableObject.en[i];
                Debug.Log("Setting Text"); ////////////////////////
            }
        }

    }
}
