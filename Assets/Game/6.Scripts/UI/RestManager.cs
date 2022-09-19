using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class RestManager : MonoBehaviour
{
    [SerializeField] private RestItem[] restItems = new RestItem[0];
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI flavorText;
    private CanvasGroup cv;

    private void Awake()
    {
        cv = GetComponent<CanvasGroup>();
    }

    public void Rest(int restItemIndex)
    {
        StopAllCoroutines();
        StartCoroutine(RestSequence(restItemIndex));
    }

    public IEnumerator RestSequence(int restItemIndex)
    {
        OpenCloseModal(false);

        foreach (var item in restItems)
        {
            item.itemButtons.interactable = false;
        }

        GameManager.CanvasManager.AnimateOverlay(OverlayAnimation.Black, 3);
        yield return new WaitForSeconds(3);
        GameManager.CanvasManager.AnimateOverlay(OverlayAnimation.Off, 3);

        PlayerHp hp = GameManager.PlayerInstance.scriptHp;

        if (restItemIndex < 0 || restItemIndex > restItems.Length)
        {
            if (GameManager.GameData.currentHp < GameManager.GameData.playerHp / 2)
                hp.HpChange(GameManager.GameData.playerHp / 2 - GameManager.GameData.currentHp);
        }
        else
        {
            if (GameManager.GameData.currentHp < GameManager.GameData.playerHp)
                hp.HpChange(GameManager.GameData.playerHp - GameManager.GameData.currentHp);

            restItems[restItemIndex].itemReference.quantity--;

        }


        UpdateModal();



    }
    public void UpdateDescription(InventoryItem item, bool noItemSelected)
    {
        if(noItemSelected)
        {
            nameText.text = "Sem Item";
            descriptionText.text = "Descansar sem um item de descanso recupera apenas a matade de sua integridade.";
            flavorText.text = "A vida no rio São Francisco é difícil. Muitas vezes, a escassez é apenas o padrão.";
        }
        else if(item == null)
        {
            nameText.text = "";
            descriptionText.text = "";
            flavorText.text = "";
        }
        else
        {
            nameText.text = item.itemName;
            descriptionText.text = item.description;
            flavorText.text = item.flavorText;
        }
    }

    private void UpdateModal()
    {
        foreach (var item in restItems)
        {
            item.itemQtyText.text = item.itemReference.quantity.ToString();
            item.itemImage.sprite = item.itemReference.itemIcon;

            if (item.itemReference.quantity <= 0)
                item.itemButtons.interactable = false;
            else
                item.itemButtons.interactable = true;
        }
    }

    public void OpenCloseModal(bool open)
    {
        if (open)
        {
            cv.DOFade(1, .33f);
            cv.interactable = true;
            cv.blocksRaycasts = true;
            UpdateDescription(null, false);
            UpdateModal();
        }
        else
        {
            cv.DOFade(0, .33f);
            cv.interactable = false;
            cv.blocksRaycasts = false;

        }

        
    }


}

[System.Serializable]
public class RestItem
{
    public Button itemButtons;
    public Image itemImage;
    public TextMeshProUGUI itemQtyText;
    public InventoryItem itemReference;

}
