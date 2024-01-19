using System;
using System.Collections.Generic;
using Game.Board;
using UnityEngine;

namespace Game.Configs
{
    [CreateAssetMenu(fileName = "VisualConfig", menuName = "Game/Visual Config", order = 0)]
    public class VisualConfig : ScriptableObject
    {
        [SerializeField] private ColorData[] colors;
        public List<GoalParticle> goalParticles;

        public Color GetBoyTypeColor(NormalItem.itemType type)
        {
            foreach (var colorData in colors)
                if (colorData.Type == type)
                    return colorData.Color;

            return Color.white;
        }
    }

    [Serializable]
    public class ColorData
    {
        public Color Color;
        public NormalItem.itemType Type;
    }
}