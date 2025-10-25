using System.Collections.Generic;
using UnityEngine;

public static class LogicHelper
{
    public static List<T> ShuffleList<T>(List<T> list)
    {
        for (var i = 0; i < list.Count; i++)
        {
            var rand = Random.Range(i, list.Count);
            (list[i], list[rand]) = (list[rand], list[i]);
        }

        return list;
    }

    public static T[] ShuffleArray<T>(T[] array)
    {
        for (var i = 0; i < array.Length; i++)
        {
            var rand = Random.Range(i, array.Length);
            (array[i], array[rand]) = (array[rand], array[i]);
        }

        return array;
    }

    public static int[] GetDistributeArray(int total, int amount)
    {
        var result = new int[amount];
        var remainder = total % amount;
        var value = total/amount;

        for (var i = 0; i < amount; i++)
        {
            result[i] = i < remainder ? value + 1 : value;
        }

        return ShuffleArray(result);
    }
}
