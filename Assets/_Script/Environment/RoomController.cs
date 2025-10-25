using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using SmoothShakeFree;

public class RoomController : MonoBehaviour
{
    [SerializeField] private LightObject[] lights;
    [SerializeField] private Material lightOn, lightOff;

    [SerializeField] private FurnitureObject[] furnitures;

    [SerializeField] bool shake;

    private List<DetectableObject> sceneObjects = new();
    private List<LightObject> activeLights = new();
    private List<Rigidbody> forceAbleObject;

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
        var abnormalAmount = LogicHelper.GetDistributeArray(marks.Count, furnitures.Length);

        for (int i = 0; i < furnitures.Length; i++)
        {
            var placedMarks = new List<BaseMark>();

            for (int j = 0; j < abnormalAmount[i]; j++)
            {
                var mark = marks[^1];
                marks.RemoveAt(marks.Count - 1);
                placedMarks.Add(mark);
            }

            var objs = furnitures[i].PlaceItems(placedMarks);

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

            if (isOn) activeLights.Add(light);
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
        var randCd = Random.Range(2f, 5f);
        yield return new WaitForSeconds(randCd);

        var randObj = Random.Range(0, lights.Length);
        var randFlickerMode = Random.Range(0, 2);

        for(var i = 0; i <= lights.Length; i++)
        {
            if (i != randObj) continue;

            switch (randFlickerMode)
            {
                case 0:
                    activeLights[i].SetLight(false);
                    yield return new WaitForSeconds(randCd / 10f);
                    activeLights[i].SetLight(true);
                    break;

                case 1:
                    var delay = new WaitForSeconds(0.15f);
                    activeLights[i].SetLight(false);
                    yield return delay;
                    activeLights[i].SetLight(true);
                    yield return delay;
                    activeLights[i].SetLight(false);
                    yield return delay;
                    activeLights[i].SetLight(true);
                    break;
            }
        }
    }

    private void SetUpForceObj()
    {
        var forceObject = GameObject.FindGameObjectsWithTag("ForceObj");
        if (forceAbleObject.Count != 0) forceAbleObject.Clear();

        foreach (var obj in forceObject)
        {
            var haveRb = obj.GetComponent<Rigidbody>();
            if(haveRb) forceAbleObject.Add(haveRb);
        }

        StartCoroutine(ForceRecur());
    }

    private void PushObj()
    {
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
