using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalItem : Item
{
    public enum itemType
    {
        TYPE_ONE = 1,
        TYPE_TWO = 2,
        TYPE_THREE = 3,
        TYPE_FOUR = 4,
    }

    public itemType ItemType;

    public void SetType(itemType type)
    {
        ItemType = type;
    }

    protected override string GetPrefabName()
    {
        var prefabname = string.Empty;
        switch (ItemType)
        {
            case itemType.TYPE_ONE:
                prefabname = CONSTANTS.PREFAB_NORMAL_TYPE_ONE;
                break;
            case itemType.TYPE_TWO:
                prefabname = CONSTANTS.PREFAB_NORMAL_TYPE_TWO;
                break;
            case itemType.TYPE_THREE:
                prefabname = CONSTANTS.PREFAB_NORMAL_TYPE_THREE;
                break;
            case itemType.TYPE_FOUR:
                prefabname = CONSTANTS.PREFAB_NORMAL_TYPE_FOUR;
                break;
        }

        return prefabname;
    }

    internal override bool IsSameType(Item other)
    {
        var it = other as NormalItem;

        return it != null && it.ItemType == this.ItemType;
    }

    public void Increase()
    {
        switch (ItemType)
        {
            case itemType.TYPE_ONE:
                ItemType = itemType.TYPE_TWO;
                break;
            case itemType.TYPE_TWO:
                ItemType = itemType.TYPE_THREE;
                break;
            case itemType.TYPE_THREE:
                ItemType = itemType.TYPE_FOUR;
                break;
            case itemType.TYPE_FOUR:
                ItemType = itemType.TYPE_ONE;
                break;
        }
        GameObject.Destroy(View.gameObject);
        SetView();
    }
}