
using System;
using System.Linq;
using Game.Board;
using URandom = UnityEngine.Random;


    public class Match3Utility
    {
        public static NormalItem.itemType GetRandomNormalType()
        {
            var values = Enum.GetValues(typeof(NormalItem.itemType));
            var result = (NormalItem.itemType)values.GetValue(URandom.Range(0, values.Length));

            return result;
        }

        public static NormalItem.itemType GetRandomNormalTypeExcept(NormalItem.itemType[] types)
        {
            var list = Enum.GetValues(typeof(NormalItem.itemType)).Cast<NormalItem.itemType>().Except(types).ToList();

            int rnd = URandom.Range(0, list.Count);
            var result = list[rnd];

            return result;
        }
    }
