using System;
using UnityEngine;

namespace Game.Base
{
    public static class DataManager
    {
        public static PlayerData Player;

        public static void Init()
        {
            Player = new PlayerData();
        }
    }

    [Serializable]
    public class PlayerData
    {
        public int Level
        {
            get => PlayerPrefs.GetInt("Level", 0);
            set => PlayerPrefs.SetInt("Level", value);
        }

        public void Save()
        {
            PlayerPrefs.Save();
        }
    }
}