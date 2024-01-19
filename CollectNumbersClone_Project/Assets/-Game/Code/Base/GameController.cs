using System;
using System.Collections;
using System.Collections.Generic;
using _Game.Code;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using _Game.Code.Utils;
using EventArch;

public enum GameState
{
    Boot,
    Ready,
    Game,
    Win,
    Fail
}

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
        var level = DataManager.Player.Level;
        var maxLevel = levelConfig.levelData.Length - 1;
        if (level > maxLevel)
        {
            level = (level - 1) % (maxLevel - loopLevel + 1) + loopLevel;
        }
        currentLevelData = levelConfig.levelData[level];
        levelController.Setup(currentLevelData);
    }

    private void OnDisable()
    {
        EventManager.Clear();
    }
}