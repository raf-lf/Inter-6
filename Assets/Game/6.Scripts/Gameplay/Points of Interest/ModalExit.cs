using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModalExit : IslandModal
{
    public void LeaveIsland()
    {
        IslandManager.CurrentIslandManager.canvasIslandManager.OpenCloseModal(false, associatedModal);
        IslandManager.CurrentIslandManager.LeaveIsland();
    }

}
