using System;
using UnityEngine;

public class DetectableObject : MonoBehaviour
{
    public event Action EventPlayerNearby;
    public event Action EventPicked;
    public event Action EventImpactHit;

    private static readonly RenderingLayerMask OutlineLayer = 2, DefaultLayer = 1;

    private MeshRenderer[] meshes;
    private Rigidbody rb;
    private bool isCasted;

    private void Awake()
    {
        meshes = GetComponentsInChildren<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
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
        rb.isKinematic = true;
    }

    public void OnPlaced()
    {
        rb.isKinematic = false;
    }

    public void OnDropped()
    {
        rb.isKinematic = false;
    }

    private void SetLayerMask(RenderingLayerMask layer)
    {
        foreach (var mesh in meshes)
        {
            mesh.renderingLayerMask = layer;
        }
    }
}
