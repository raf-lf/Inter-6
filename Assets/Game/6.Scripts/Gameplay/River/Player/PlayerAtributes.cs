using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAtributes : MonoBehaviour
{
    public bool dead;
    private List<int> attacksImmuneTo = new List<int>();

    [SerializeField] private int damageThresholdHigh;

    public void Damage(int value, int attackId)
    {
        if (dead || !GameManager.PlayerControl)
            return;
        
        if (attacksImmuneTo.Contains(attackId) == false)
        {
            if(value >= damageThresholdHigh)
                GameManager.PlayerInstance.playerVfx.VfxDamageHigh();
            else if (value > 0)
                GameManager.PlayerInstance.playerVfx.VfxDamageLow();
            else if (value == 0)
                GameManager.PlayerInstance.playerVfx.VfxInvulnerability();

            HpChange(-value);

            StartCoroutine(Iframes(attackId));
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
        if (value < 0 && Cheats.cheatInvulnerability)
            return;

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
        GameManager.PlayerInstance.playerVfx.VfxDeath();
        dead = true;
        StartCoroutine(DeathSequence());

    }
    public IEnumerator DeathSequence()
    {
        Destroy(GameManager.soundTrackManager);
        GameManager.PlayerControl = false;
        yield return new WaitForSeconds(2);
        GameManager.CanvasManager.AnimateOverlay(OverlayAnimation.Black, 2);
        yield return new WaitForSeconds(2);
        GameManager.PlayerControl = true;
        GameManager.GameData.ResetValues();
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

}
