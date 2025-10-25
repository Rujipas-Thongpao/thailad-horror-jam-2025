using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    [SerializeField] private LightObject[] lights;
    [SerializeField] private Material lightOn, lightOff;

    [SerializeField] private RoomConfigSO roomConfig;
    [SerializeField] private FurnitureObject[] furnitures;

    private List<DetectableObject> sceneObjects = new();

    public void Init(List<BaseMark> marks)
    {
        for (int i = 0; i < lights.Length; i++)
        {
            var isOn = Random.Range(0f, 1f) > 0.3f;

            lights[i].Init(lightOn, lightOff);
            lights[i].SetLight(isOn);
        }

        for (int i = 0; i < furnitures.Length; i++)
        {
            var amount = furnitures[i].GetAmount();
            var objs = new DetectableObject[amount];

            for (int j = 0; j < amount; j++)
            {
                objs[j] = Instantiate(roomConfig.GetNonMarkObject());
                sceneObjects.Add(objs[j]);
            }
            
            furnitures[i].PlaceItems(objs);

        }

        ApplyMark(marks);
    }

    public void Dispose()
    {
        sceneObjects.Clear();
    }

    private void ApplyMark(List<BaseMark> marks)
    {
        if (marks == null || marks.Count == 0 || sceneObjects == null || sceneObjects.Count == 0) return;

        var selectedObjects = GetRandomUniqueElements(sceneObjects, marks.Count);

        for (int i = 0; i < selectedObjects.Count; i++)
        {
            selectedObjects[i].ApplyMark(marks[i]);
        }
    }

    private static List<T> GetRandomUniqueElements<T>(IList<T> source, int count)
    {
        count = Mathf.Min(count, source.Count);

        var tempList = new List<T>(source);
        var result = new List<T>();

        for (int i = 0; i < count; i++)
        {
            int index = Random.Range(0, tempList.Count);
            result.Add(tempList[index]);
            tempList.RemoveAt(index);
        }

        return result;
    }
}
