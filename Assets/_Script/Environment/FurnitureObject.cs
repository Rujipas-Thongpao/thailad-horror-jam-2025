using System.Collections.Generic;
using UnityEngine;

public class FurnitureObject : MonoBehaviour
{
    [SerializeField] private Transform[] places;
    [SerializeField] private FurnitureConfigSO config;

    public List<DetectableObject> PlaceItems(List<BaseMark> marks)
    {
        var objs = new List<DetectableObject>();
        var amount = GetAmount(marks.Count);
        var shuffledPlaces = LogicHelper.ShuffleArray(places);
        var isAbnormal = LogicHelper.GetDistributeArray(marks.Count, amount);

        for (int i = 0; i < amount; i++)
        {
            var obj = isAbnormal[i] == 0 ?
                                    SpawnNormalObject() :
                                    SpawnAbnormalObject(ref marks);

            var angleY = Random.Range(0f, 360f);
            var rotation = Quaternion.Euler(new Vector3(0, angleY, 0));
            obj.transform.SetPositionAndRotation(shuffledPlaces[i].position, rotation);

            objs.Add(obj);
        }

        return objs;
    }

    private DetectableObject SpawnNormalObject()
    {
        return Instantiate(config.GetNormalObject());
    }

    private DetectableObject SpawnAbnormalObject(ref List<BaseMark> marks)
    {
        var obj = Instantiate(config.GetAbnormalObject());
        var mark = marks[^1];
        marks.RemoveAt(marks.Count - 1);
        obj.ApplyMark(mark);

        return obj;
    }

    private int GetAmount(int minimum)
    {
        int maximum = places.Length;
        minimum = Mathf.Max(minimum, maximum / 2);
        return Random.Range(minimum, maximum);
    }
}
