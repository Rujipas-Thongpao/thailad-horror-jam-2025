using System;
using UnityEngine;

public class DetectableObject : MonoBehaviour
{
    public event Action EventPlayerNearby;
    public event Action EventPicked;
    public event Action EventImpactHit;

    private static readonly RenderingLayerMask OutlineLayer = 2, DefaultLayer = 1;

    public BaseMark Mark { get; private set; }

    private MeshRenderer[] meshes;
    private bool isCasted = false;

    private void Awake()
    {
        meshes = GetComponentsInChildren<MeshRenderer>();
    }

    public void OnCasted()
    {
        isCasted = true;
        SetLayerMask(OutlineLayer);
    }

    public void OnUnCasted()
    {
        isCasted = false;
        SetLayerMask(DefaultLayer);
    }

    public void OnPicked()
    {
        EventPicked?.Invoke();
    }

    public void ApplyMark(BaseMark mark)
    {
        Mark = mark;
    }

    private void SetLayerMask(RenderingLayerMask layer)
    {
        foreach (var mesh in meshes)
        {
            mesh.renderingLayerMask = layer;
        }
    }
}
