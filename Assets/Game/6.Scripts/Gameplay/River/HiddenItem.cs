using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HiddenItem : LanternTarget
{
    [SerializeField] private GameObject itemMesh;
    [SerializeField] private ParticleSystem pfxBubbles;

    public override void LanternEffect()
    {
        base.LanternEffect();
        itemMesh.transform.DOScale(0, .33f).SetEase(Ease.InBounce);
        pfxBubbles.Stop();
    }

    protected override void Update()
    {
        base.Update();

        if (!lanternImmune)
        {
            itemMesh.transform.localPosition = new Vector3(0,  -1 + (lanternProgress / targetThreshold) * 2, 0 );
        }
    }
}
