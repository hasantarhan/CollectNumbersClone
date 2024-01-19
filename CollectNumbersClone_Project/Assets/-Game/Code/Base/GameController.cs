
using EventArch;
using Game.Configs;
using UnityEngine;

namespace Game.Base
{
    public class GameController : MonoBehaviour
    {
        public int loopLevel = 1;
        public LevelController levelController;
        public LevelData currentLevelData;
        private LevelConfig levelConfig;

        private void Awake()
        {
            DataManager.Init();
            levelConfig = Resources.Load<LevelConfig>("Configs/LevelConfig");
        }

        private void Start()
        {
            int level = DataManager.Player.Level;
            int maxLevel = levelConfig.levelData.Length - 1;
            if (level > maxLevel) level = (level - 1) % (maxLevel - loopLevel + 1) + loopLevel;
            currentLevelData = levelConfig.levelData[level];
            levelController.Setup(currentLevelData);
        }

        private void OnDisable()
        {
            EventManager.Clear();
        }
    }
}