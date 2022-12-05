using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using DG.Tweening;

public class CombatState : MonoBehaviour
{
    private int combatants;
    [SerializeField] private StudioEventEmitter bgmCombat;
    private float bgmTransition;

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
        DOTween.To(() => bgmTransition, x => bgmTransition = x, combatants > 0 ? 1 : 0, 2);

        bgmCombat.SetParameter("draga", bgmTransition); 

    }
}
