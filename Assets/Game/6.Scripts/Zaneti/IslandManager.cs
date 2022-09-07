using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IslandManager : MonoBehaviour
{

    [Header("Components")]
     GameObject player;
     public Image BackGroung;
     public Camera Camera;
     public float intoShoreSpeed;

    [Header("Base")]
     Transform deckPosition;
    Transform exitPosition;
    public LayerMask interactionLayer;
    public string SceneToGo;
    bool canInteract;

    public static IslandManager Instance;

   


    private void Start()
    {
        player = FindObjectOfType<PlayerData>().gameObject;
        deckPosition = transform.Find("DeckPosition");
        exitPosition = transform.Find("ExitPosition");
        Debug.Log("Exit:" + exitPosition);
        StartCoroutine(NavigateArround(deckPosition, false));
        
    }
    private void Update()
    {
        if (canInteract) PointClick();


    }



    void PointClick() 
    {

        bool hitted = Physics.Raycast(MouseRay(),out RaycastHit hit, interactionLayer);
        Debug.Log("Ta pointando e clickando");

        if (hitted == true) 
        {

            Debug.Log("HittedObject: " + hit.collider.gameObject);

            if (Input.GetKeyDown(KeyCode.Mouse0)) { Destroy(hit.collider.gameObject);}
        }
        

    
    
    }



    Ray MouseRay() 
    {
        return Camera.ScreenPointToRay(Input.mousePosition);
    }



    public void GoBackToRiver(){        StartCoroutine(NavigateArround(exitPosition, true));        }




    IEnumerator NavigateArround(Transform position, bool goingOut ) 
    {

        canInteract = false;
        while(player.transform.position != position.position) 
        {
            
            player.transform.position = Vector3.MoveTowards(player.transform.position, position.position, intoShoreSpeed * 0.01f);  
            yield return null;  
        
        }
        if(goingOut == false) canInteract = true;

        else if (goingOut) SceneTrans.instance.GoTo(SceneToGo);
            
        
    
    }   


}
