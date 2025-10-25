using UnityEngine;

public class SwitchMaterialController : MonoBehaviour
{
    [SerializeField] private MeshRenderer renderer;
    [SerializeField] private Material OnMaterial;
    [SerializeField] private Material OffMaterial;

    public void SetTVState(bool isOn)
    {
        if (isOn)
        {
            renderer.material = OnMaterial;
        }
        else
        {
            renderer.material = OffMaterial;
        }
    }
}
