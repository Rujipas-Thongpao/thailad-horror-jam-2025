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
    private NearByChecker nearByChecker;

    private void Awake()
    {
        meshes = GetComponentsInChildren<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
        nearByChecker = gameObject.GetComponentInChildren<NearByChecker>();

        if (center == null) center = transform;
        Subscribe();
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }

    private void Subscribe()
    {
        if(nearByChecker != null)
            nearByChecker.EventPlayerNearBy += OnPlayerNearBy;
    }

    private void Unsubscribe()
    {
        if(nearByChecker != null)
            nearByChecker.EventPlayerNearBy -= OnPlayerNearBy;
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
    }

    public void OnPlaced(Vector3 pos)
    {
        transform.position = pos;
        rb.isKinematic = false;

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
}

[Serializable]
public class PlaceConfig
{
    public bool Flip;
    public bool PlaceVertical;
}
