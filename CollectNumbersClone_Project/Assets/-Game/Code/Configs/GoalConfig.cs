using System;
using System.Collections.Generic;
using Game.Board;
using Game.Entities;
using UnityEngine;

namespace Game.Configs
{
    [CreateAssetMenu(fileName = "GoalConfig", menuName = "Game/Goal Config", order = 0)]
    public class GoalConfig : ScriptableObject
    {
        public List<Goal> goals;
        public int moveCount = 10;
        public GoalIndicator goalIndicatorPrefab;
    }

    [Serializable]
    public class Goal
    {
        public NormalItem.itemType type;
        public int amount;
    }

    [Serializable]
    public class GoalParticle
    {
        public NormalItem.itemType type;
        public ParticleSystem ps;

        public GoalParticle(NormalItem.itemType type, ParticleSystem ps)
        {
            this.type = type;
            this.ps = ps;
        }
    }
}