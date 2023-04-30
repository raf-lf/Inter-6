using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using FMODUnity;

public class ModalRest : IslandModal
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
        RuntimeManager.PlayOneShot("event:/UI/click");
    }

    public IEnumerator RestSequence(int restItemIndex)
    {

        IslandManager.CurrentIslandManager.canvasIslandManager.OpenCloseModal(false, associatedModal);

        PlayerData.buffResistance = false;
        PlayerData.buffEfficiency = false;
        PlayerData.buffStealth = false;

        foreach (var item in restItems)
        {
            item.itemButtons.interactable = false;
        }

        GameManager.CanvasManager.AnimateOverlay(OverlayAnimation.Black, 3);
        yield return new WaitForSeconds(3);
        GameManager.CanvasManager.AnimateOverlay(OverlayAnimation.Off, 3);

        PlayerAtributes hp = GameManager.PlayerInstance.atributes;

        if (restItemIndex < 0 || restItemIndex > restItems.Length)
        {
            if (GameManager.GameData.currentHp < GameManager.GameData.maxHp / 2)
                hp.HpChange(GameManager.GameData.maxHp / 2 - GameManager.GameData.currentHp);
        }
        else
        {
            if (GameManager.GameData.currentHp < GameManager.GameData.maxHp)
                hp.HpChange(GameManager.GameData.maxHp - GameManager.GameData.currentHp);

            restItems[restItemIndex].itemReference.quantity--;
            
            switch(restItemIndex)
            {
                case 0:
                    PlayerData.buffResistance = true;
                    break;
                case 1:
                    PlayerData.buffEfficiency = true;
                    break;
                case 2:
                    PlayerData.buffStealth = true;
                    break;

            }

        }

        UpdateModal();
        GameManager.Hud.UpdateBuff();

        GameManager.CameraManager.ReturnCamera();


    }

    public void UpdateDescription(InventoryItem item, bool noItemSelected)
    {
        if(noItemSelected)
        {
            nameText.text = "Sem Item";
            descriptionText.text = "Descansar sem um item de descanso recupera apenas a matade de sua integridade.";
            flavorText.text = "A vida no rio S�o Francisco � dif�cil. Muitas vezes, a escassez � o padr�o.";
        }
        else if(item == null)
        {
            nameText.text = "";
            descriptionText.text = "";
            flavorText.text = "";
        }
        else
        {
            if(GameManager.gameLanguage == Language.portuguese) 
            {
                nameText.text = item.itemPTName;
                descriptionText.text = item.ptDescription;
                flavorText.text = item.ptFlavorText;
            }
            else 
            {
                nameText.text = item.itemENName;
                descriptionText.text = item.itemENDescription;
                flavorText.text = item.itemENFlavorText;
            }
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

    public override void ModalOpened()
    {
        base.ModalOpened();
        UpdateDescription(null, false);
        UpdateModal();
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
