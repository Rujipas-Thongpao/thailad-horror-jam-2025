using UnityEngine;

public class LightObject : MonoBehaviour
{
    [SerializeField] private Light light;

    private MeshRenderer mesh;
    private Material lightOn;
    private Material lightOff;

    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    public void Init(Material _lightOn, Material _lightOff)
    {
        lightOn = _lightOn;
        lightOff = _lightOff;
        mesh.material = lightOn;
    }

    public void SetLight(bool isOn)
    {
        light.enabled = isOn;
        mesh.material = isOn ? lightOn : lightOff;
    }
}
