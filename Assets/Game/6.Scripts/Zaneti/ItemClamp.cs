using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemClamp : MonoBehaviour
{
    Button itemButton;
    
    [SerializeField]
    Items item;


    private void Start()
    {
        itemButton = GetComponent<Button>();
        Debug.Log("FoundIt: " + itemButton);
        //itemButton.onClick.AddListener(IslandManager.Instance.GiveIten(item));
    }

    private void Update()
    {
    }



}
 