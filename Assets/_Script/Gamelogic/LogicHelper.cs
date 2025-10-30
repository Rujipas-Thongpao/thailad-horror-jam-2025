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
        var remainder = amount > 0 ? total % amount : 0;
        var value = amount > 0 ? total/amount : 0;

        for (var i = 0; i < amount; i++)
        {
            result[i] = i < remainder ? value + 1 : value;
        }

        return ShuffleArray(result);
    }

    private static bool IsFlipped(Transform t)
    {
        return t.localToWorldMatrix.determinant < 0;
    }

    public static void ApplyAngle(Transform transform)
    {
        var euler = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0f, euler.y, 0f);
    }

    public static void ApplyAngleCanFlip(Transform transform)
    {
        var euler = transform.rotation.eulerAngles;
        transform.rotation = IsFlipped(transform) ?
                             Quaternion.Euler(euler.x, euler.y, euler.z) :
                             Quaternion.Euler(0f, euler.y, 0f);
    }

    public static void ApplyAngleCanFlipAndPlaceVertical(Transform transform)
    {
        var euler = transform.rotation.eulerAngles;
        var angleFromUp = Vector3.Angle(transform.up, Vector3.up);

        if (angleFromUp > 60f)
        {
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                Quaternion.FromToRotation(transform.up, Vector3.up) * transform.rotation,
                10f
            );
        }

        if (IsFlipped(transform))
        {
            transform.rotation = Quaternion.Euler(euler.x, euler.y, euler.z);
        }
    }

    public static Vector3 GetRandomPositionOnSphere(Vector3 center, float radius = 4f)
    {
        var randomDirection = Random.onUnitSphere;
        return center + randomDirection * radius;
    }
}
