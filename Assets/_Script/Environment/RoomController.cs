using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using SmoothShakeFree;

public class RoomController : MonoBehaviour
{
    [SerializeField] private LightObject[] lights;
    [SerializeField] private Material lightOn, lightOff;

    [SerializeField] private FurnitureObject[] furniture;

    private readonly List<DetectableObject> sceneObjects = new();
    private readonly List<LightObject> activeLights = new();
    private readonly List<Rigidbody> forceAbleObject = new();

    public static RoomController Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

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
        var abnormalAmount = LogicHelper.GetDistributeArray(marks.Count, furniture.Length);

        for (int i = 0; i < furniture.Length; i++)
        {
            var placedMarks = new List<BaseMark>();

            for (int j = 0; j < abnormalAmount[i]; j++)
            {
                var mark = marks[^1];
                marks.RemoveAt(marks.Count - 1);
                placedMarks.Add(mark);
            }

            var objs = furniture[i].PlaceItems(placedMarks);

            foreach (var obj in objs)
            {
                sceneObjects.Add(obj);
            }
        }
    }

    private void SetUpLight()
    {
        foreach (var light in lights)
        {
            var isOn = Random.Range(0f, 1f) > 0.3f;

            light.Init(lightOn, lightOff);
            light.SetLight(isOn);
            activeLights.Add(light);
        }
    }

    #region environment events
    [ContextMenu("Flicker Light")]
    public void StartFlickerLight()
    {
        StartCoroutine(FlickerLight());
    }

    [ContextMenu("Shake Room")]
    public void ShakeRoom()
    {
        //TODO: implement camera handler and move logic there.
        var allShake = FindObjectsByType<SmoothShake>(FindObjectsSortMode.None);

        foreach (var canShake in allShake)
        {
            canShake.StartShake();
        }
        //

        for (int i = 0; i <= forceAbleObject.Count; i++)
        {
            var randomDirection = Random.onUnitSphere;
            var randomForce = Random.Range(2f, 4f);

            forceAbleObject[i].AddForce(randomDirection * randomForce, ForceMode.Impulse);
        }
    }
    #endregion

    private IEnumerator FlickerLight()
    {
        var randCd = Random.Range(.1f, 2f);
        //yield return new WaitForSeconds(randCd);

        var randObj = Random.Range(activeLights.Count/2, activeLights.Count);
        var randFlickerMode = Random.Range(0, 2);
        randFlickerMode = 1;

        for(var i = 0; i <= activeLights.Count; i++)
        {
            if (i != randObj) continue;

            switch (randFlickerMode)
            {
                case 0:
                    activeLights[i].SetLight(false, true);
                    yield return new WaitForSeconds(randCd / 10f);
                    activeLights[i].SetLight(true, true);
                    break;

                case 1:
                    int r = Random.Range(15, 30);
                    for(int j = 0; j < r; j++)
                    {
                        var w = new WaitForSeconds(Random.Range(0.01f, .2f)); 

                        activeLights[i].SetLight(false,true);
                        yield return w;
                        activeLights[i].SetLight(true,true);
                        yield return w;
                    }
                    break;
            }
        }
    }

    private void SetUpForceObj()
    {
        if (forceAbleObject.Count != 0) forceAbleObject.Clear();

        var forceObject = GameObject.FindGameObjectsWithTag("ForceObj");

        foreach (var obj in forceObject)
        {
            var haveRb = obj.GetComponent<Rigidbody>();
            if(haveRb) forceAbleObject.Add(haveRb);
        }

        StartCoroutine(ForceRecur());
    }

    private void PushObj()
    {
        if (forceAbleObject.Count == 0) return;

        var randObj = Random.Range(0, forceAbleObject.Count);

        for (var i = 0; i <= forceAbleObject.Count; i++)
        {
            if (i != randObj) continue;

            var randomDirection = Random.onUnitSphere;
            var randomForce = Random.Range(2f, 4f);

            forceAbleObject[i].AddForce(randomDirection * randomForce, ForceMode.Impulse);
        }
    }

    IEnumerator ForceRecur()
    {
        var randCd = Random.Range(2f, 5f);
        yield return new WaitForSeconds(randCd);

        PushObj();
        StartCoroutine(ForceRecur());
    }
}
