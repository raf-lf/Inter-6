using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagLock : MonoBehaviour
{
    public enum CheckType { And, Or}

    public Flag[] flagsToCheck = new Flag[0];
    [SerializeField] private CheckType checkType;

    public bool FlagsCleared()
    {
        switch (checkType)
        {
            default:
                return false;

            case CheckType.Or:
                foreach (var item in flagsToCheck)
                {
                    if (item.flagActive)
                        return true;
                }
                return false; 

            case CheckType.And:
                int clears = flagsToCheck.Length;
                foreach (var item in flagsToCheck)
                {
                    if(item.flagActive)
                        clears--;
                }
                if (clears <= 0)
                    return true;
                else return false;
        }

    }
}
