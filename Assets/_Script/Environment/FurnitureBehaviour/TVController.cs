using UnityEngine;

public class TVController : FurnitureObject
{
    [SerializeField] private MeshRenderer screenRenderer;
    [SerializeField] private Material tvOnMaterial;
    [SerializeField] private Material tvOffMaterial;

    public void SetTVState(bool isOn)
    {
        if (isOn)
        {
            screenRenderer.material = tvOnMaterial;
        }
        else
        {
            screenRenderer.material = tvOffMaterial;
        }
    }
}
