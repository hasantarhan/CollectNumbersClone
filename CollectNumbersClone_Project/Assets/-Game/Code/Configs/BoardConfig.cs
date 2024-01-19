using System.Collections.Generic;
using Game.Board;
using UnityEngine;

namespace Game.Configs
{
    [CreateAssetMenu(fileName = "BoardConfig", menuName = "Game/Board Config", order = 0)]
    public class BoardConfig : ScriptableObject
    {
        public int boardSizeX = 5;
        public int boardSizeY = 5;
        public int matchesMin = 3;
        public List<StartItem> startItems;
    }
}