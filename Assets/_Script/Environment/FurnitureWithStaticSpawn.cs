using System.Collections.Generic;
using UnityEngine;

public class FurnitureWithStaticSpawn : FurnitureObject
{
     [SerializeField] private Transform[] placesWithParent;

    public override List<DetectableObject> PlaceItems(List<BaseMark> marks)
    {
        var objs = new List<DetectableObject>();

        var minAmountWithParent = Mathf.Min(Random.Range(0, marks.Count + 1), places.Length);
        var minAmount = marks.Count - minAmountWithParent;

        var amount = GetAmount(minAmount, places.Length);
        var amountWithParent = GetAmount(minAmountWithParent, placesWithParent.Length);

        //var shuffledPlaces = LogicHelper.ShuffleArray(places);

        var isAbnormal = LogicHelper.GetDistributeArray(marks.Count, amount + amountWithParent);

        for (int i = 0; i < amount; i++)
        {
            var obj = isAbnormal[i] == 0 ?
                                    SpawnNormalObject() :
                                    SpawnAbnormalObject(ref marks);

            obj.Init(false);
            var angleY = Random.Range(0f, 360f);
            var rotation = Quaternion.Euler(new Vector3(0, angleY, 0));
            obj.transform.SetPositionAndRotation(places[i].position, rotation);

            objs.Add(obj);
        }

        for ( int i = 0; i < placesWithParent.Length; i++)
        {
            var obj = marks.Count > 0 ?
                                    SpawnAbnormalObject(ref marks) :
                                    SpawnNormalObject();
            var place = placesWithParent[i];
            //obj.GetComponent<Rigidbody>().isKinematic = false;
            Debug.Log($"Placing object {obj.name} at place with parent {place.name}");
            obj.Init(true);
            obj.transform.SetPositionAndRotation(place.position, place.rotation);
            obj.transform.SetParent(place);
            objs.Add(obj);
        }

        return objs;
    }

    protected int GetAmount(int minimum, int maximum)
    {
        minimum = Mathf.Max(minimum, maximum / 2);
        return Random.Range(minimum, maximum);
    }
}
