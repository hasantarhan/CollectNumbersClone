
using Game.Base;
using UnityEngine;

namespace Game.Board
{
    public class NormalItem : Item
    {
        public enum itemType
        {
            TYPE_ONE = 1,
            TYPE_TWO = 2,
            TYPE_THREE = 3,
            TYPE_FOUR = 4
        }

        public itemType ItemType;

        public void SetType(itemType type)
        {
            ItemType = type;
        }

        protected override string GetPrefabName()
        {
            var prefabName = string.Empty;
            switch (ItemType)
            {
                case itemType.TYPE_ONE:
                    prefabName = CONSTANTS.PREFAB_NORMAL_TYPE_ONE;
                    break;
                case itemType.TYPE_TWO:
                    prefabName = CONSTANTS.PREFAB_NORMAL_TYPE_TWO;
                    break;
                case itemType.TYPE_THREE:
                    prefabName = CONSTANTS.PREFAB_NORMAL_TYPE_THREE;
                    break;
                case itemType.TYPE_FOUR:
                    prefabName = CONSTANTS.PREFAB_NORMAL_TYPE_FOUR;
                    break;
            }

            return prefabName;
        }

        internal override bool IsSameType(Item other)
        {
            var itemType = other as NormalItem;

            return itemType != null && itemType.ItemType == ItemType;
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

            Object.Destroy(View.gameObject);
            SetView();
        }
    }
}