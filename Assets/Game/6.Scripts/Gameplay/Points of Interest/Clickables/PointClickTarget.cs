using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class PointClickTarget : MonoBehaviour
{
    [SerializeField] private bool mouseOver;
    [SerializeField] private ParticleSystem vfxMouseOver;
    [SerializeField] private ParticleSystem vfxMouseClick;
    private ParticleSystem.EmissionModule mouseOverEm;
    [SerializeField] private Renderer renderer;

    [SerializeField] public CinemachineVirtualCameraBase focusCamera;

    [SerializeField] private UnityEvent ClickEvent;

    protected virtual void Start()
    {
        mouseOverEm = vfxMouseOver.emission;
    }

    private void Update()
    {
        mouseOverEm.enabled = mouseOver;
        
        if (mouseOver && GameManager.PlayerControl && Input.GetMouseButtonDown(0))
            Click();
    }

    public virtual void Click()
    {
        mouseOver = false;
        vfxMouseClick.Play();
        ClickEvent?.Invoke();

        if (focusCamera != null)
            GameManager.CameraManager.FocusCamera(focusCamera);
    }
    
    public void ResetCameraFocus()
    {
        if (focusCamera != null)
            GameManager.CameraManager.ReturnCamera();
    }

    private void OnMouseOver()
    {
        MouseOver(true);
    }
    
    private void OnMouseExit()
    {
        MouseOver(false);
    }
    
    private void MouseOver(bool active)
    {
        if (!GameManager.PlayerControl || !GameManager.PlayerClickControl)
            return;
        
        mouseOver = active;
        
        float value = active ? .5f : 0f;
        
        if(renderer)
            renderer.material.SetFloat("_BlinkIntensity", value);
    }
    
}
