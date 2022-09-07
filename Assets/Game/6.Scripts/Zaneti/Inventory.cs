using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory: MonoBehaviour 
{
    public static Inventory Instance;


    internal bool canInteract;
    GoIntoIsland islandToGo;
   

   

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }


    public Inventory GetInstance() 
    {
        if (Instance == null) Instance = this;
        return Instance;
    }




    private void Update()
    {

        if(canInteract && Input.GetButtonDown("Action")) 
        {

            Debug.Log("Entrando na Ilhota");
            SceneTrans.instance.GoTo(islandToGo.islandName);
            //land.GoIntoShore();
        
        }
    }


    private void OnTriggerEnter(Collider collision)
    {

        islandToGo = collision.gameObject.GetComponent<GoIntoIsland>();

        if (islandToGo != null)
        {
            Debug.Log("Aqui a ilha ó");
            canInteract = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {

        if (islandToGo != null)
        {
            canInteract = false;
            islandToGo = null;
        }
    }





}
public enum Items
{
    Coffe,
    Gold,
    Tobacco,
    CheeseB

}