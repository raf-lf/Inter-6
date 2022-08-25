using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp : MonoBehaviour
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
        yield return new WaitForSeconds(GameManager.GameData.iFrames);
        attacksImmuneTo.Remove(id);
    }

    public void HpChange(int value)
    {
        GameManager.GameData.currentHp = Mathf.Clamp(GameManager.GameData.currentHp + value, 0, GameManager.GameData.playerHp);

        if (value < 0)
        {

        }
        else if (value > 0)
        {
        }

        if (GameManager.GameData.currentHp <= 0)
            Death();

    }

    public void Death()
    {
        dead = true;

    }
}
