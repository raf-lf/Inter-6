using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Flag", menuName = "Flag")]
public class Flag : ScriptableObject
{
    public bool flagActive;

    public void ActivateFlag()
    {
        flagActive = true;
    }

}
