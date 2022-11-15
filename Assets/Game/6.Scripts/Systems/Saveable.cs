using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saveable : MonoBehaviour
{
    [SerializeField] private SaveableDataType dataType;

    public void Start()
    {
        Load();
    }

    public void Save(bool value)
    {
        SaveSystem.Save(dataType, gameObject.name, value);
    }

    public void Load()
    {
        switch (dataType)
        {
            case SaveableDataType.item:
                if (SaveSystem.Load(dataType, gameObject.name))
                    gameObject.SetActive(false);
                break;
            case SaveableDataType.shortcut:
                if (SaveSystem.Load(dataType, gameObject.name))
                    GetComponent<ShortcutCave>().OpenShortcut();
                break;
            case SaveableDataType.spiritLock:
                if (SaveSystem.Load(dataType, gameObject.name))
                    GetComponent<SpiritLock>().OpenLock(true);
                break;
        }
    }
}
