using System;
using UnityEngine;

namespace _Game.Code
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
            get
            {
               return PlayerPrefs.GetInt("Level", 0);
            }
            set
            {
                 PlayerPrefs.SetInt("Level", value);
            }
        }
        public void Save()
        {
            PlayerPrefs.Save();
        }
    }
}