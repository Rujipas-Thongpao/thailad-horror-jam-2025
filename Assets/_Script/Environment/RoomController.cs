using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using SmoothShakeFree;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

public class RoomController : MonoBehaviour
{
    [SerializeField] private LightObject[] lights;
    [SerializeField] private Material lightOn, lightOff;

    [SerializeField] private FurnitureSetup[] setupPool;

    private FurnitureSetup setup;

    private readonly List<DetectableObject> sceneObjects = new();
    private readonly List<LightObject> activeLights = new();

    public static RoomController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void Init(List<BaseMark> marks, FurnitureSetup furnitureSetup)
    {
        setup = Instantiate(furnitureSetup, transform);
        SetUpFurniture(marks);
        SetUpLight();
    }

    public void Dispose()
    {
        for (int i = 0; i < sceneObjects.Count; i++)
        {
            if (sceneObjects[i] == null) continue;
            Destroy(sceneObjects[i].gameObject);
        }

        sceneObjects.Clear();
        Destroy(setup.gameObject);
    }

    private void SetUpFurniture(List<BaseMark> marks)
    {
        var spawners = setup.ObjSpawners;
        var abnormalAmount = LogicHelper.GetDistributeArray(marks.Count, spawners.Length);

        for (int i = 0; i < spawners.Length; i++)
        {
            var placedMarks = new List<BaseMark>();

            for (int j = 0; j < abnormalAmount[i]; j++)
            {
                var mark = marks[^1];
                marks.RemoveAt(marks.Count - 1);
                placedMarks.Add(mark);
            }

            var objs = spawners[i].PlaceItems(placedMarks);

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

        for (int i = 0; i < sceneObjects.Count; i++)
        {
            var randomDirection = Random.onUnitSphere;
            var randomForce = Random.Range(1f, 2f);

            sceneObjects[i]?.Addforce(randomDirection * randomForce);
        }
    }


    [ContextMenu("Test Poltergeist Routine")]
    public async Task TestPoltergeistRoutine()
    {
        await PoltergeistRoutine(2f);
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

    private void PushObj(float intensity)
    {
        var randObj = Random.Range(0, sceneObjects.Count);

        var objAmount = Random.Range(0, sceneObjects.Count * intensity);
        objAmount = Mathf.Clamp(objAmount, 1, sceneObjects.Count);

        for (var i = 0; i < objAmount; i++)
        {
            randObj = Random.Range(0, sceneObjects.Count);
            sceneObjects[randObj]?.Addforce(Random.onUnitSphere * Random.Range(1f, 3f) * intensity);
        }
    }

    public async UniTask PoltergeistRoutine(float intensity)
    {
        var r = Random.Range(2f, intensity * 3f);
        for(int i =0;i< r; i++)
        {
            PushObj(intensity);
            await UniTask.Delay(System.TimeSpan.FromSeconds(Random.Range(2f, 5f)));
        }
        return;
    }

    public void SpawnGhost()
    {
        var spawnPoint = setup.GhostSpawnPoints[Random.Range(0, setup.GhostSpawnPoints.Length)];
    }
}
