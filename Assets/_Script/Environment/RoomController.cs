using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    [SerializeField] private LightObject[] lights;
    [SerializeField] private Material lightOn, lightOff;

    private List<DetectableObject> objects;

    public void Init()
    {
        for (int i = 0; i < lights.Length; i++)
        {
            var isOn = Random.Range(0f, 1f) > 0.3f;

            lights[i].Init(lightOn, lightOff);
            lights[i].SetLight(isOn);
        }
    }
}
