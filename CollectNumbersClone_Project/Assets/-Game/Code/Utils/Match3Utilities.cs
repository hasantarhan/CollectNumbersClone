using System;
using System.Collections.Generic;
using System.Linq;
using URandom = UnityEngine.Random;
namespace _Game.Code
{
    public class Match3Utilities
    {
        public static NormalItem.itemType GetRandomNormalType()
        {
            Array values = Enum.GetValues(typeof(NormalItem.itemType));
            NormalItem.itemType result = (NormalItem.itemType)values.GetValue(URandom.Range(0, values.Length));

            return result;
        }

        public static NormalItem.itemType GetRandomNormalTypeExcept(NormalItem.itemType[] types)
        {
            List<NormalItem.itemType> list = Enum.GetValues(typeof(NormalItem.itemType)).Cast<NormalItem.itemType>().Except(types).ToList();

            int rnd = URandom.Range(0, list.Count);
            NormalItem.itemType result = list[rnd];

            return result;
        }
    }
}