using UnityEngine;

public class LampController : OnOffDeviceController
{
    [SerializeField] private MeshRenderer renderer;
    [SerializeField] private Light lampLight;
    [SerializeField] private Material OnMaterial;
    [SerializeField] private Material OffMaterial;

    public override void SetState(bool isOn)
    {
        if (isOn)
        {
            renderer.material = OnMaterial;
            lampLight.enabled = true;
        }
        else
        {
            renderer.material = OffMaterial;
            lampLight.enabled = false;
        }
    }
}
