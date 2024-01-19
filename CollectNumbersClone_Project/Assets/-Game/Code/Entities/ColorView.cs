using Game.Board;
using Game.Configs;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Entities
{
    public class ColorView : MonoBehaviour
    {
        [SerializeField] private VisualConfig visualConfig;
        [SerializeField] private NormalItem.itemType itemType;
        private Image image;
        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            image = GetComponent<Image>();
        }

        private void Start()
        {
            SetType(itemType);
        }

        public void SetType(NormalItem.itemType type)
        {
            itemType = type;
            if (spriteRenderer) spriteRenderer.color = visualConfig.GetBoyTypeColor(itemType);

            if (image) image.color = visualConfig.GetBoyTypeColor(itemType);
        }
    }
}