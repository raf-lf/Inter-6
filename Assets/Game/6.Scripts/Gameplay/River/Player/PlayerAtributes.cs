using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtributes : MonoBehaviour
{
    public bool dead;
    private List<int> attacksImmuneTo = new List<int>();

    public void Damage(int value, int attackId)
    {
        if (!dead)
        {
            if (attacksImmuneTo.Contains(attackId) == false)
            {
                HpChange(-value);
                StartCoroutine(Iframes(attackId));
            }
        }
    }

    public IEnumerator Iframes(int id)
    {
        attacksImmuneTo.Add(id);
        yield return new WaitForSeconds(GameManager.GameData.invulnerabilitySeconds);
        attacksImmuneTo.Remove(id);
    }

    public void HpChange(float value)
    {
        GameManager.GameData.currentHp = Mathf.Clamp(GameManager.GameData.currentHp + value, 0, GameManager.GameData.maxHp);
        GameManager.Hud.UpdateHp(value);

        if (value < 0)
        {

        }
        else if (value > 0)
        {
        }

        if (GameManager.GameData.currentHp <= 0)
            Death();

    }

    public void EnergyChange(float value)
    {
        GameManager.GameData.currentGas = Mathf.Clamp(GameManager.GameData.currentGas + value, 0, GameManager.GameData.maxGas);
        GameManager.Hud.UpdateEnergy(value);

    }

    public void Death()
    {
        dead = true;

    }
}
