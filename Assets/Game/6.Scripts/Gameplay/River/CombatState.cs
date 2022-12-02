using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class CombatState : MonoBehaviour
{
    private int combatants;
    [SerializeField] private StudioEventEmitter bgmCombat;

    private void Start()
    {
        combatants = 0;
    }

    private void Awake()
    {
        GameManager.CombatState = this;
    }

    public void ChangeCombatants(int value)
    {
        combatants += value;
    }

    private void Update()
    {
        bgmCombat.SetParameter("draga", combatants > 0 ? 1 : 0); 

    }
}
