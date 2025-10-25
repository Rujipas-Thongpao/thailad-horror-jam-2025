using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using SmoothShakeFree;

public class RoomController : MonoBehaviour
{
    [SerializeField] private LightObject[] lights;
    [SerializeField] private Material lightOn, lightOff;

    [SerializeField] private RoomConfigSO roomConfig;
    [SerializeField] private FurnitureObject[] furnitures;

    private List<DetectableObject> sceneObjects = new();

    [SerializeField]  private List<Rigidbody> forceAbleObject;
    [SerializeField]  private List<LightObject> flickerLight;

    [SerializeField] bool shake;

    public void Init(List<BaseMark> marks)
    {
        SetUpFurniture(marks);
        SetUpLight();
        SetUpForceObj();
    }

    public void Dispose()
    {
        sceneObjects.Clear();
    }

    private void SetUpFurniture(List<BaseMark> marks)
    {
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

    private void SetUpLight()
    {
        for (int i = 0; i < lights.Length; i++)
        {
            var isOn = Random.Range(0f, 1f) > 0.3f;

            lights[i].Init(lightOn, lightOff);
            lights[i].SetLight(isOn);

            if(isOn) flickerLight.Add(lights[i]);
        }
        
        StartCoroutine(FlickerLight());
    }

    IEnumerator FlickerLight()
    {
        float randCd = Random.Range(2f, 5f);
        yield return new WaitForSeconds(randCd);
        
        int randObj = Random.Range(0, flickerLight.Count);

        int randFlickerMode = Random.Range(0, 1 + 1);

        for(int i=0; i<=flickerLight.Count; i++)
        {
            if(i == randObj) 
            {
                if(randFlickerMode == 0)
                {
                    flickerLight[i].SetLight(false);
                    yield return new WaitForSeconds(randCd / 10f);
                    flickerLight[i].SetLight(true);
                }
                else if(randFlickerMode == 1)
                {
                    flickerLight[i].SetLight(false);
                    yield return new WaitForSeconds(0.15f);
                    flickerLight[i].SetLight(true);
                    yield return new WaitForSeconds(0.15f);
                    flickerLight[i].SetLight(false);
                    yield return new WaitForSeconds(0.15f);
                    flickerLight[i].SetLight(true);
                }
            }
        }
        
        StartCoroutine(FlickerLight());
    }

    private void SetUpForceObj()
    {
        GameObject[] forceObject = GameObject.FindGameObjectsWithTag("ForceObj");
        if(forceAbleObject.Count != 0) forceAbleObject.Clear();

        foreach(GameObject obj in forceObject)
        {
            Rigidbody haveRB = obj.GetComponent<Rigidbody>();
            if(haveRB) forceAbleObject.Add(haveRB);
        }

        StartCoroutine(ForceRecur());
    }

    private void PushObj()
    {
        int randObj = Random.Range(0, forceAbleObject.Count);
        for(int i=0; i<=forceAbleObject.Count; i++)
        {
            if(i == randObj) 
            {
                Vector3 randomDirection = Random.onUnitSphere;
                float randomForce = Random.Range(2f, 4f);

                forceAbleObject[i].AddForce(randomDirection * randomForce, ForceMode.Impulse);
            }
        }
    }

    IEnumerator ForceRecur()
    {
        float randCd = Random.Range(2f, 5f);
        yield return new WaitForSeconds(randCd);

        PushObj();

        StartCoroutine(ForceRecur());
    }

    public void ShakeRoom()
    {
        SmoothShake[] allShake = FindObjectsByType<SmoothShake>(FindObjectsSortMode.None);

        foreach(SmoothShake canShake in allShake)
        {
            canShake.StartShake();
        }

        for(int i=0; i<=forceAbleObject.Count; i++)
        {
            Vector3 randomDirection = Random.onUnitSphere;
            float randomForce = Random.Range(2f, 4f);

            forceAbleObject[i].AddForce(randomDirection * randomForce, ForceMode.Impulse);
        }
    }

    void Update()
    {
        if(shake)
        {
            shake = false;
            ShakeRoom();
        }
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
