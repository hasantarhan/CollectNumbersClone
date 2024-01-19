using System;
using UnityEngine;

namespace Game.Configs
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Game/LevelConfig", order = 0)]
    public class LevelConfig : ScriptableObject
    {
        public LevelData[] levelData;
    }

    [Serializable]
    public class LevelData
    {
        public BoardConfig boardConfig;
        public GoalConfig goalConfig;
    }
}