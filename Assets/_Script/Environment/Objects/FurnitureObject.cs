using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FurnitureObject : MonoBehaviour
{
    [SerializeField] protected Transform[] places;
    [SerializeField] private FurnitureConfigSO config;

    private void Start()
    {
        if (places.Length > 0 && config == null)
        {
            Debug.LogWarning($"Furniture object: {name} has no config");
        }
    }

    public virtual List<DetectableObject> PlaceItems(List<BaseMark> marks)
    {
        if(places.Length == 0 || config == null)
        {
            return new List<DetectableObject>();
        }

        var objs = new List<DetectableObject>();
        var amount = GetAmount(marks.Count);
        var shuffledPlaces = LogicHelper.ShuffleArray(places);
        var isAbnormal = LogicHelper.GetDistributeArray(marks.Count, amount);

        for (int i = 0; i < amount; i++)
        {
            var obj = isAbnormal[i] == 0 ?
                                    SpawnNormalObject() :
                                    SpawnAbnormalObject(ref marks);

            obj.Init();
            var angleY = Random.Range(0f, 360f);
            var rotation = Quaternion.Euler(new Vector3(0, angleY, 0));
            obj.transform.SetPositionAndRotation(shuffledPlaces[i].position, rotation);

            objs.Add(obj);
        }

        return objs;
    }

    protected DetectableObject SpawnNormalObject()
    {
        return Instantiate(config.GetNormalObject());
    }

    protected DetectableObject SpawnAbnormalObject(ref List<BaseMark> marks)
    {
        var obj = Instantiate(config.GetAbnormalObject());
        var mark = marks[^1];
        marks.RemoveAt(marks.Count - 1);
        mark.Init(obj);
        obj.ApplyMark(mark);

        return obj;
    }

    protected virtual int GetAmount(int minimum)
    {
        int maximum = places.Length;
        minimum = Mathf.Max(minimum, maximum / 2);
        return Random.Range(minimum, maximum);
    }
}
