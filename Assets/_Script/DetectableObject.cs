using UnityEngine;

public class DetectableObject : MonoBehaviour
{
    private MeshRenderer[] meshes;

    [SerializeField]
    private RenderingLayerMask outlineLayer, defaultLayer;

    private bool isCasted = false;

    private void Awake()
    {
        meshes = GetComponentsInChildren<MeshRenderer>();
    }

    public void OnCasted()
    {
        isCasted = true;
        SetLayerMask(outlineLayer);
    }

    public void OnUnCasted()
    {
        isCasted = false;
        SetLayerMask(defaultLayer);
    }

    private void Update()
    {
        // If not casted, ensure the layer mask is set to default
        if (!isCasted)
        {
            SetLayerMask(defaultLayer);
        }
    }

    private void SetLayerMask(RenderingLayerMask layer)
    {
        foreach (var mesh in meshes)
        {
            mesh.renderingLayerMask = layer;
        }
    }
}
