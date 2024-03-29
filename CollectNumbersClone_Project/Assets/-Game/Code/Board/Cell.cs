﻿using UnityEngine;

namespace Game.Board
{
    public class Cell : MonoBehaviour
    {
        public int BoardX { get; private set; }

        public int BoardY { get; private set; }

        public Item Item { get; private set; }

        public Cell NeighbourUp { get; set; }

        public Cell NeighbourRight { get; set; }

        public Cell NeighbourBottom { get; set; }

        public Cell NeighbourLeft { get; set; }

        public bool IsEmpty => Item == null;

        public void Setup(int cellX, int cellY)
        {
            BoardX = cellX;
            BoardY = cellY;
        }

        public void Free()
        {
            Item = null;
        }

        public void Assign(Item item)
        {
            Item = item;
            Item.SetCell(this);
        }

        public void ApplyItemPosition(bool withAppearAnimation)
        {
            Item.SetViewPosition(transform.position);

            if (withAppearAnimation) Item.ShowAppearAnimation();
        }

        internal void Clear()
        {
            if (Item != null)
            {
                Item.Clear();
                Item = null;
            }
        }

        internal bool IsSameType(Cell other)
        {
            return Item != null && other.Item != null && Item.IsSameType(other.Item);
        }

        internal void ExplodeItem()
        {
            if (Item == null) return;

            Item.ExplodeView();
            Item = null;
        }
    }
}