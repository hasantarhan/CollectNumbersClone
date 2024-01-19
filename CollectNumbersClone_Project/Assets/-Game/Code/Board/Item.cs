using System;
using DG.Tweening;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Board
{
    [Serializable]
    public class Item
    {
        public Cell Cell { get; private set; }

        public Transform View { get; private set; }

        public virtual void SetView()
        {
            string prefabName = GetPrefabName();

            if (!string.IsNullOrEmpty(prefabName))
            {
                var prefab = Resources.Load<GameObject>(prefabName);
                if (prefab)
                {
                    if (View != null) Object.Destroy(View.gameObject);
                    View = Object.Instantiate(prefab).transform;
                }
            }
        }

        protected virtual string GetPrefabName()
        {
            return string.Empty;
        }

        public virtual void SetCell(Cell cell)
        {
            Cell = cell;
        }

        internal void AnimationMoveToPosition()
        {
            if (View == null) return;

            View.DOMove(Cell.transform.position, 0.2f);
        }

        public void SetViewPosition(Vector3 pos)
        {
            if (View) View.position = pos;
        }

        public void SetViewRoot(Transform root)
        {
            if (View) View.SetParent(root);
        }

        internal void ShowAppearAnimation()
        {
            if (View == null) return;

            var scale = View.localScale;
            View.localScale = Vector3.one * 0.1f;
            View.DOScale(scale, 0.1f);
        }

        internal virtual bool IsSameType(Item other)
        {
            return false;
        }

        internal virtual void ExplodeView()
        {
            if (View)
                View.DOScale(0.1f, 0.1f).OnComplete(
                    () =>
                    {
                        Object.Destroy(View.gameObject);
                        View = null;
                    }
                );
        }

        internal void Clear()
        {
            Cell = null;

            if (View)
            {
                Object.Destroy(View.gameObject);
                View = null;
            }
        }
    }

    [Serializable]
    public class StartItem
    {
        public int x = 5;
        public int y = 5;
        public NormalItem.itemType value;
    }
}