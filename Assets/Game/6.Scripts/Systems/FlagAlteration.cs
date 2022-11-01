using System;
using UnityEngine;
using UnityEngine.Events;

public class FlagAlteration : MonoBehaviour
{
    [SerializeField] private bool checkOnStart;
    [SerializeField] private FlagLock flagLock;
    [SerializeField] private UnityEvent[] successEvents = Array.Empty<UnityEvent>();
    [SerializeField] private UnityEvent[] failureEvents = Array.Empty<UnityEvent>();


    private void Start()
    {
        if (checkOnStart)
            DoAlterations();
    }

    public void DoAlterations()
    {
        if (flagLock.FlagsCleared())
        {
            if (successEvents.Length <= 0)
                return;
            
            foreach (var item in successEvents)
            {
                item?.Invoke();
            }
        }
        else
        {
            if (failureEvents.Length <= 0)
                return;
            
            foreach (var item in failureEvents)
            {
                item?.Invoke();
            }
        }
    }
}