using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LanternTargetType
{
    soul, dredge, hiddenItem
}

public class LanternTarget : MonoBehaviour
{
    public float lanternProgress;
    public float targetThreshold;
    [SerializeField] private float lanternRecovery;
    [SerializeField] private bool lanternActiveOver;
    [SerializeField] private bool lanternImmune;

    public void LanternGain(float effect)
    {
        if(lanternImmune)
            return;

        lanternActiveOver = true;
        lanternProgress = Mathf.Clamp(lanternProgress + effect * Time.deltaTime, 0, targetThreshold);

        if(lanternProgress >= targetThreshold)
        {
            LanternEffect();
        }
    }

    private void LanternLoss()
    {
        if (lanternActiveOver)
            return;

        if (lanternProgress <= 0)
            return;

        lanternProgress = Mathf.Clamp(lanternProgress - lanternRecovery * Time.deltaTime, 0, targetThreshold);
    }

    public void LanternEffect()
    {
        lanternImmune = true;

    }

    private void Update()
    {
        LanternLoss();
    }
}
