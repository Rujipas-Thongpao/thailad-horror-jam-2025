using UnityEngine;

public class DetectableObject : MonoBehaviour
{
    private MeshRenderer[] meshes;

    [SerializeField]
    private RenderingLayerMask outlineLayer, defaultLayer;


    private void Awake()
    {
        meshes = GetComponentsInChildren<MeshRenderer>();
    }

    public void OnCasted()
    {
        foreach (var mesh in meshes)
        {
            mesh.renderingLayerMask = outlineLayer;
        }
    }

    public void OnUnCasted()
    {
        foreach (var mesh in meshes)
        {
            mesh.renderingLayerMask = defaultLayer;
        }

    }
}
