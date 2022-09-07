using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class SceneTrans : MonoBehaviour
{
    Animator animator;

    public static SceneTrans instance;

    

    private void Awake()
    {
        if(instance == null) 
        {
            instance = this;    
        }


    }



    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }
    public void GoTo(string whereTo) 
    {
        StartCoroutine(Transition(whereTo));

    }

    IEnumerator Transition(string goingTo) 
    {


        animator.SetTrigger("_start");

        yield return new WaitForSeconds(1);

        
        EditorSceneManager.LoadScene(goingTo);
    
    }

}
