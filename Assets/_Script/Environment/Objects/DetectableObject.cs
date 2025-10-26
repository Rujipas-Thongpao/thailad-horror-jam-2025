using System;
using UnityEngine;

public class DetectableObject : MonoBehaviour, IDetectable
{
    public event Action EventPlayerNearby;
    public event Action EventPicked;
    public event Action EventImpactHit;

    private static readonly RenderingLayerMask OutlineLayer = 2, DefaultLayer = 1;

    [SerializeField] private PlaceConfig placeConfig;
    [SerializeField] private Transform center;

    public Transform Center => center;

    private MeshRenderer[] meshes;
    private Rigidbody rb;
    private Collider[] cols;

    private void Awake()
    {
        meshes = GetComponentsInChildren<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
        cols = GetComponentsInChildren<Collider>();

        if (center == null) center = transform;
    }

    public void OnHovered()
    {
        SetLayerMask(OutlineLayer);
    }

    public void OnUnhovered()
    {
        SetLayerMask(DefaultLayer);
    }

    public void OnPicked()
    {
        EventPicked?.Invoke();
        rb.isKinematic = true;
        TogglePhysicCollider(false);
    }

    public void OnPlaced(Vector3 pos)
    {
        transform.position = pos;
        rb.isKinematic = false;
        TogglePhysicCollider(true);

        switch (placeConfig.Flip)
        {
            case false when !placeConfig.PlaceVertical:
            {
                LogicHelper.ApplyAngle(transform);
                break;
            }
            case true when !placeConfig.PlaceVertical:
            {
                LogicHelper.ApplyAngleCanFlip(transform);
                break;
            }
            case true when placeConfig.PlaceVertical:
            {
                LogicHelper.ApplyAngleCanFlipAndPlaceVertical(transform);
                break;
            }
        }
    }

    public void OnDropped()
    {
        rb.isKinematic = false;
        TogglePhysicCollider(true);
    }

    private void TogglePhysicCollider(bool isActive)
    {
        if (cols.Length == 0) return;

        foreach (var col in cols)
        {
            col.enabled = isActive;
        }
    }

    private void SetLayerMask(RenderingLayerMask layer)
    {
        foreach (var mesh in meshes)
        {
            mesh.renderingLayerMask = layer;
        }
    }

    public void OnPlayerNearBy()
    {
        EventPlayerNearby?.Invoke();
    }

    public void Addforce(Vector3 force)
    {
        rb.AddForce(force, ForceMode.Impulse);
    }
}

[Serializable]
public class PlaceConfig
{
    public bool Flip;
    public bool PlaceVertical;
}
