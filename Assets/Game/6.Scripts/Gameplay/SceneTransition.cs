using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private string sceneDestination;

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            StartSceneTransition();
    }
    */

    public void StartSceneTransition()
    {
        StartCoroutine(Transition());

    }

    IEnumerator Transition() 
    {
        GameManager.CanvasManager.AnimateOverlay(OverlayAnimation.Black, 1);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneDestination,LoadSceneMode.Single);
    
    }

}
