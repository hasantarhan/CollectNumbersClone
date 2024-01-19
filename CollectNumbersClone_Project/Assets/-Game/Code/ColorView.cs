using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Game.Code
{
    public class ColorView : MonoBehaviour
    { 
        [SerializeField] private VisualConfig visualConfig;
        [SerializeField] private NormalItem.itemType itemType;
        private SpriteRenderer spriteRenderer;
        private Image image;
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
            if (spriteRenderer)
            {
                spriteRenderer.color = visualConfig.GetBoyTypeColor(itemType);
            }

            if (image)
            {
                image.color = visualConfig.GetBoyTypeColor(itemType);
            }
          
        }
        
    }
}