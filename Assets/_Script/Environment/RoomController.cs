using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    [SerializeField] private LightObject[] lights;
    [SerializeField] private Material lightOn, lightOff;

    private List<DetectableObject> objects;

    [SerializeField]  private List<Rigidbody> forceAbleObject;
    [SerializeField]  private List<LightObject> flickerLight;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        SetUpLight();

        SetUpForceObj();
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
}
