using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableOpenModal : PointClickTarget
{
    public IslandModalType associatedModal;

    protected override void Start()
    {
        base.Start();

        switch (associatedModal)
        {
            case IslandModalType.exit: 
                IslandManager.CurrentIslandManager.canvasIslandManager.clickableAnchor = this;
                break;
            case IslandModalType.rest:
                IslandManager.CurrentIslandManager.canvasIslandManager.clickableRestPoint = this;
                break;
        }
    }

    public override void Click()
    {
        base.Click();
        IslandManager.CurrentIslandManager.canvasIslandManager.OpenCloseModal(true, associatedModal);
    }
}
