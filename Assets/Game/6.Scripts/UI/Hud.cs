using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Hud : MonoBehaviour
{
    [Header("Atributes")]
    [SerializeField] private TextMeshProUGUI hpTextCurrent ;
    [SerializeField] private TextMeshProUGUI hpTextMax ;
    [SerializeField] private Image hpFill;
    [SerializeField] private Image energyFill;

    [Header("GetItemAnimation")]
    [SerializeField] private RectTransform itemOrigin;
    [SerializeField] private RectTransform itemDestination;
    [SerializeField] private GameObject getItemPrefab;
    [SerializeField] private AnimationCurve getItemAnimationCurve;

    private void Awake()
    {
        GameManager.Hud = this;
    }

    private void Start()
    {
        UpdateHp(0);
        UpdateEnergy(0);
    }

    public void UpdateHp(float value)
    {
        hpTextCurrent.text = GameManager.GameData.currentHp.ToString();
        hpTextMax.text = GameManager.GameData.maxHp.ToString();

        hpFill.DOFillAmount((float)GameManager.GameData.currentHp / (float)GameManager.GameData.maxHp, .15f);
    }

    public void UpdateEnergy(float value)
    {
        energyFill.DOFillAmount((float)GameManager.GameData.currentGas / (float)GameManager.GameData.maxGas, .15f);
    }
    public IEnumerator GetItemSequence(InventoryItem item, int quantity)
    {
        for (int i = quantity; i > 0; i--)
        {
            var newItem = Instantiate(getItemPrefab, itemOrigin);
            newItem.transform.position += new Vector3(Random.Range(-50, 50), Random.Range(-50, 50),0);
            newItem.GetComponent<Image>().sprite = item.itemIcon;
            var newItemRect = newItem.GetComponent<RectTransform>();
    
            Sequence seq = DOTween.Sequence();
            seq.Append(newItemRect.DOScale(0, 0));
            seq.Append(newItemRect.DOScale(1, 1f).SetEase(Ease.OutBack));
            seq.Append(newItemRect.DOMove(itemDestination.position, .5f).SetEase(getItemAnimationCurve));
            seq.Append(newItemRect.DOScale(0, .5f));

            Destroy(newItem, 2);
            yield return new WaitForSeconds(.5f / quantity);
        }
    }

}
