using EventArch;
using Game.Board;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Entities
{
    public class Player : MonoBehaviour
    {
        private Camera camera1;
        public UnityAction<Cell> onCellClicked;

        private void Awake()
        {
            camera1 = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var hit = Physics2D.Raycast(
                    camera1.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)),
                    Vector2.zero);
                if (hit.transform != null && hit.transform.TryGetComponent(out Cell cell)) onCellClicked?.Invoke(cell);
            }
        }

        private void OnEnable()
        {
            EventManager.AddListener<OnFinishGame>(Disable);
        }

        private void OnDisable()
        {
            EventManager.RemoveListener<OnFinishGame>(Disable);
        }

        private void Disable(OnFinishGame obj)
        {
            enabled = false;
        }
    }
}